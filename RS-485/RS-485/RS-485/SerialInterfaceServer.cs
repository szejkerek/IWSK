using System;
using System.IO.Ports;

namespace RS_485
{
    class SerialInterfaceServer
    {
        SerialPort serialDevice = new SerialPort();
        public event EventHandler<SerialInterfaceEvent> RxDataEvent;
        public event EventHandler<SerialInterfacePEvent> PinChangeEvent;
        public event EventHandler<SerialInterfaceEvent> PingEvent;

        public class Settings
        {
            public enum Handshake { None, XONXOFF, RTS, DTR };
            public enum Terminator { None, CR, LF, CRLF, Custom };
            public int Baudrate { get; set; }
            public int BitCount { get; set; }
            public Parity parity { get; set; }
            public StopBits stopBits { get; set; }
            public Terminator terminator { get; set; }
            public String CustomTerminator { get; set; }
            public Handshake handshake { get; set; }
        }
        string buffer = "";
        string terminatedBuffer = "";

        bool readyState = false;
        Settings.Terminator terminator = Settings.Terminator.None;
        String customTerminator = "";

        bool ping = false;
        public SerialInterfaceServer()
        {
            serialDevice.DataReceived += new SerialDataReceivedEventHandler(receiver);
            serialDevice.PinChanged += new SerialPinChangedEventHandler(pinChangeHandler);
            serialDevice.ErrorReceived += dataLinkErrorHandler;
        }

        private void dataLinkErrorHandler(object sender, SerialErrorReceivedEventArgs error)
        {
            if (error.EventType == SerialError.RXParity)
            {
                serialDevice.ReadExisting(); // read/clear device buffer
                RxDataEvent(this, new SerialInterfaceEvent("Parity Error\n"));
            }
            else if (error.EventType == SerialError.Frame)
            {
                serialDevice.ReadExisting(); // read/clear device buffer
                RxDataEvent(this, new SerialInterfaceEvent("Malformed frame\n"));
            }
            else
            {
                serialDevice.ReadExisting();
                RxDataEvent(this, new SerialInterfaceEvent("General error\n"));
            }
        }
        void pinChangeHandler(object sender, SerialPinChangedEventArgs ev) 
        {
            if (ev.EventType == SerialPinChange.CtsChanged)
            {
                PinChangeEvent(this, new SerialInterfacePEvent(serialDevice.DsrHolding, serialDevice.CtsHolding));
            }
            else if (ev.EventType == SerialPinChange.DsrChanged)
            {
                if (readyState)
                {
                    Send(buffer);
                    buffer = "";
                }
                PinChangeEvent(this, new SerialInterfacePEvent(serialDevice.DsrHolding, serialDevice.CtsHolding));
            }
        }

        private void receiver(object sender, SerialDataReceivedEventArgs ev)
        {
            while (serialDevice.BytesToRead > 0)
            {
                if (ping)
                {
                    PingUtility((char)serialDevice.ReadChar());
                }
                else
                {
                    if (terminator == Settings.Terminator.None)
                    {
                        RxDataEvent(this, new SerialInterfaceEvent(serialDevice.ReadExisting()));
                    }
                    else
                    {
                        TerminatorUtility((char)serialDevice.ReadChar());
                    }
                }
            }
        }

        private void PingUtility(char magicChar)
        {
            if (magicChar == '^')
            {
                PingEvent(this, new SerialInterfaceEvent("Ping OK!"));
            }
            else if (magicChar == '`')
            {
                serialDevice.Write("^");
            }
        }

        private void TerminatorUtility(char oneChar)
        {
            switch (terminator)
            {
                case Settings.Terminator.LF:
                    if (oneChar == '\n')
                    {
                        RxDataEvent(this, new SerialInterfaceEvent(terminatedBuffer));
                        terminatedBuffer = "";
                    }
                    else
                    {
                        terminatedBuffer += oneChar.ToString();
                    }
                    break;
                case Settings.Terminator.CR:
                    if (oneChar == '\r')
                    {
                        RxDataEvent(this, new SerialInterfaceEvent(terminatedBuffer));
                        terminatedBuffer = "";
                    }
                    else
                    {
                        terminatedBuffer += oneChar.ToString();
                    }
                    break;
                case Settings.Terminator.CRLF:
                    terminatedBuffer += oneChar.ToString();
                    if (terminatedBuffer.Contains("\r\n"))
                    {
                        RxDataEvent(this, new SerialInterfaceEvent(terminatedBuffer.Remove(terminatedBuffer.Length-2,2)));
                        terminatedBuffer = "";
                    }
                    break;
                case Settings.Terminator.Custom:
                    terminatedBuffer += oneChar.ToString();
                    if (terminatedBuffer.Contains(customTerminator))
                    {
                        RxDataEvent(this, new SerialInterfaceEvent(terminatedBuffer.Replace(customTerminator, "")));
                        terminatedBuffer = "";
                    }
                    break;
            }
        }

        internal void Close()
        {
            if (serialDevice.IsOpen)
            {
                serialDevice.ReadExisting();
                if (serialDevice.Handshake != Handshake.RequestToSend)
                {
                    SetDTR(false);
                    SetRTS(false);
                }
                serialDevice.Close();
                // buffer cleanup
                buffer = "";
                terminatedBuffer = "";
            }
        }

        internal bool Open(string SerialDeviceName)
        {
            if (!serialDevice.IsOpen && SerialDeviceName != "")
            {
                serialDevice.PortName = SerialDeviceName;
                serialDevice.Open();
                if (readyState)
                {
                    SetDTR(true);
                }
                PinChangeEvent(this, new SerialInterfacePEvent(serialDevice.DsrHolding, serialDevice.CtsHolding));
                return true;
            }
            return false;
        }

        internal void UpdateSettings(Settings settings)
        {
            serialDevice.BaudRate = settings.Baudrate;
            serialDevice.DataBits = settings.BitCount;
            serialDevice.StopBits = settings.stopBits;
            serialDevice.Parity = settings.parity;
            if (settings.handshake == Settings.Handshake.DTR)
            {
                this.readyState = true;
                serialDevice.Handshake = Handshake.None;
            }
            else
            {
                this.readyState = false;
                serialDevice.Handshake = (Handshake)settings.handshake;
            }
            this.terminator = settings.terminator;
            this.customTerminator = settings.CustomTerminator;
        }

        internal void Send(string dataBuffer)
        {
            if (serialDevice.IsOpen && dataBuffer != null)
            {
                switch(terminator) {
                    case Settings.Terminator.CR:
                        dataBuffer += '\r';
                        break;
                    case Settings.Terminator.LF:
                        dataBuffer += '\n';
                        break;
                    case Settings.Terminator.CRLF:
                        dataBuffer += "\r\n";
                        break;
                    case Settings.Terminator.Custom:
                        dataBuffer += customTerminator;
                        break;
                }
                if (readyState)
                {
                    if (serialDevice.DsrHolding)
                    {
                        serialDevice.Write(dataBuffer);
                    }
                    else
                    {
                        buffer += dataBuffer;
                    }
                } else
                {
                    serialDevice.Write(dataBuffer);
                }
            }
        }
        internal void SendXON()
        {
            if (serialDevice.IsOpen)
            {
                Send(((char)17).ToString());
            }
        }
        internal void SendXOFF()
        {
            if (serialDevice.IsOpen)
            {
                Send(((char)19).ToString());
            }
        }
        internal void SendPing()
        {
            if (serialDevice.IsOpen)
            {
                Send('`'.ToString());
            }
        }
        internal void SetRTS(bool val)
        {
            if (serialDevice.IsOpen)
            {
                serialDevice.RtsEnable = val;
            }
        }
        internal void SetDTR(bool val)
        {
            if (serialDevice.IsOpen)
            {
                serialDevice.DtrEnable = val;
            }
        }
        internal string[] GetPossibleCOMDevices()
        {
            return SerialPort.GetPortNames();
        }

        internal void SetPing(bool val)
        {
            ping = val;
        }
    }
}
