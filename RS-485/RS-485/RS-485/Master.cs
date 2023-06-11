using System.Diagnostics;
using System.Threading;
using System;

namespace RS_485
{
    class Master : Modbus
    {
        private enum MasterState { Idle, Receiver, Processing, CharacterTimeout }
        MasterState state = MasterState.Idle;
        bool waitForFrame = false;

        string inputBuffer;
        int CharTimeout;
        int TransactionTimeout;
        Stopwatch CharacterStopwatch = new Stopwatch();

        Frame ReceivedFrame;

        public event EventHandler<ModbusMsg> ReceivedFrameHandler;
        public event EventHandler<ModbusMsg> SendFrameHandler;
        public event EventHandler<ModbusMsg> FunctionTwoHandler;
        public event EventHandler<ModbusMsg> CheckSumErrorHandler;
        public event EventHandler<ModbusMsg> StatusHandler;

        public override void Receive(char character)
        {
            if (waitForFrame)
            {
                if (character == ':')
                {
                    state = MasterState.Receiver;
                    CharacterStopwatch = new Stopwatch();
                    CharacterStopwatch.Start();
                }
                else if (state == MasterState.Receiver)
                {
                    Thread.Sleep(20);
                    if (CharacterStopwatch.ElapsedMilliseconds > CharTimeout)
                    {
                        CharacterStopwatch.Stop();
                        inputBuffer = "";
                        state = MasterState.CharacterTimeout;
                        return;
                    }

                    inputBuffer += character.ToString();
                    if (inputBuffer.Length > 2)
                    {
                        if (inputBuffer.Substring(inputBuffer.Length - 2) == "\r\n")
                        {
                            ReceivedFrame = new Frame(inputBuffer.Remove(inputBuffer.Length - 2));
                            state = MasterState.Processing;
                            inputBuffer = "";
                            serialPort.ReadExisting();
                        }
                    }
                }
            }
            CharacterStopwatch.Restart();
        }

        private bool WaitForResponse(Frame sendFrame)
        {
            waitForFrame = true;
            state = MasterState.Idle;
            serialPort.ReadExisting();
            inputBuffer = "";
            Write(sendFrame);
            Stopwatch TTimeout = new Stopwatch();
            TTimeout.Start();
            while (state != MasterState.Processing)
            {
                if (state == MasterState.CharacterTimeout)
                {
                    waitForFrame = false;
                    inputBuffer = "";
                    serialPort.ReadExisting();
                    return false;
                }

                if (TTimeout.ElapsedMilliseconds > TransactionTimeout)
                {
                    waitForFrame = false;
                    inputBuffer = "";
                    state = MasterState.Idle;
                    serialPort.ReadExisting();
                    return false;
                }
            }
            TTimeout.Stop();
            waitForFrame = false;

            if (ReceivedFrame != null)
            {
                if (ReceivedFrame.CheckLRC())
                {
                    ReceivedFrameHandler(this, new ModbusMsg(ReceivedFrame.ToString()));
                    ExecuteFrame(ReceivedFrame);
                }
                else
                    CheckSumErrorHandler(this, new ModbusMsg("LRC Error"));
            }
            state = MasterState.Idle;
            return true;
        }

        private void ExecuteFrame(Frame recivedFrame)
        {
            if (recivedFrame.function == 2)
            {
                FunctionTwoHandler(this, new ModbusMsg(recivedFrame.args));
            }
        }

        public bool makeTransaction(int slaveAdres, int function, string args)
        {
            Frame sendFrame = new Frame((char)slaveAdres, (char)function, args);
            SendFrameHandler(this, new ModbusMsg(sendFrame.ToString()));
            if (slaveAdres == 0)
            {
                Write(sendFrame);
                return true;
            }
            else
            {
                return WaitForResponse(sendFrame);
            }
        }

        public void RunTransaction(int slaveAdres, int function, string args, int charTimeout, int transactionTimeout, int retransmission)
        {
            this.CharTimeout = charTimeout;
            this.TransactionTimeout = transactionTimeout;

            if (function != 1 && function != 2)
            {
                StatusHandler(this, new ModbusMsg("Nieprawidłowa funkcja"));
                return;
            }
            if (function == 2 && slaveAdres == 0)
            {
                StatusHandler(this, new ModbusMsg("Błąd: Nie można rozesłać przez broadcast"));
                return;
            }

            for (int i = 1; i <= retransmission + 1; i++)
            {
                if (makeTransaction(slaveAdres, function, args))
                    return;
                StatusHandler(this,
                    state == MasterState.CharacterTimeout
                        ? new ModbusMsg("Character Timeout")
                        : new ModbusMsg(string.Format("Timeout Error {0}", i)));
            }
        }
    }
}