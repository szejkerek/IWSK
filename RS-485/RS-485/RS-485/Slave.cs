using System.Diagnostics;
using System.Threading;
using System;

namespace RS_485
{
    class Slave : Modbus
    {
        private enum SlaveState { Off, Idle, Reciver, Processing }
        SlaveState state = SlaveState.Off;
        string inputBuffer;
        int slaveAdres;
        int charTimeout;
        Stopwatch characterStopwatch;


        public string secondCommand = "";

        public event EventHandler<ModbusMsg> ReceivedFrameHandler;
        public event EventHandler<ModbusMsg> SendFrameHandler;
        public event EventHandler<ModbusMsg> FirstCommandExecutionHandler;
        public event EventHandler<ModbusMsg> CheckSumErrorHandler;
        public event EventHandler<ModbusMsg> StatusHandler;

        public override void Receive(char character)
        {
            if (state != SlaveState.Off)
            {
                if (state == SlaveState.Idle)
                {
                    if (character == ':')
                    {
                        state = SlaveState.Reciver;
                        characterStopwatch = new Stopwatch();
                        characterStopwatch.Start();
                    }
                }
                else if (state == SlaveState.Reciver)
                {
                    Thread.Sleep(20);
                    if (characterStopwatch.ElapsedMilliseconds > charTimeout)
                    {
                        characterStopwatch.Stop();
                        StatusHandler(this, new ModbusMsg("Character Timeout"));
                        inputBuffer = "";
                        state = SlaveState.Idle;
                        return;
                    }
                    StatusHandler(this, new ModbusMsg(characterStopwatch.ElapsedMilliseconds.ToString()));
                    inputBuffer += character.ToString();
                    if (inputBuffer.Length > 2)
                    {
                        if (inputBuffer.Substring(inputBuffer.Length - 2) == "\r\n")
                        {
                            Frame frame = new Frame(inputBuffer.Remove(inputBuffer.Length - 2));
                            ExecuteCommand(frame);
                            state = SlaveState.Idle;
                            inputBuffer = "";
                            serialPort.ReadExisting();
                        }
                    }
                }
            }
            characterStopwatch.Restart();
        }

        private void ExecuteCommand(Frame frame)
        {
            if (frame.CheckLRC())
            {
                ReceivedFrameHandler(this, new ModbusMsg(frame.ToString()));
                if (frame.addres == slaveAdres)
                {
                    //rozgłoszeniowo
                    if (frame.function == 1)
                    {
                        ExecuteFirstCommand(frame.args);
                        Write(frame);
                        SendFrameHandler(this, new ModbusMsg(frame.ToString()));
                    }
                    //adresowany
                    else if (frame.function == 2)
                    {
                        ExecuteSecondCommand(frame);
                    }
                }
                else if (frame.addres == 0)
                {
                    
                    if (frame.function == 1)
                    {
                        ExecuteFirstCommand(frame.args);
                    }
                }
            }
            else
            {
                CheckSumErrorHandler(this, new ModbusMsg("LRC Error"));
            }
        }


        private void ExecuteFirstCommand(String data)
        {
            FirstCommandExecutionHandler(this, new ModbusMsg(data));
        }

        private void ExecuteSecondCommand(Frame data)
        {
            Frame response = new Frame(data.addres, data.function, secondCommand);
            Write(response);
            SendFrameHandler(this, new ModbusMsg(response.ToString()));
        }

        internal void Run(int adres, int charTimeout)
        {
            state = SlaveState.Idle;
            slaveAdres = adres;
            this.charTimeout = charTimeout;
        }

        internal void Stop()
        {
            state = SlaveState.Off;
        }
    }
}