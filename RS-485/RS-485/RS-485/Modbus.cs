using System.IO.Ports;

namespace RS_485
{
    class Modbus
    {
        protected SerialPort serialPort;

        protected Frame receivedFrame;

        public bool BadCrcChecksum { get; set; } = false;

        public Modbus()
        {
            this.serialPort = new SerialPort();
            this.serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPort.BytesToRead > 0)
            {
                Receive((char)serialPort.ReadByte());
            }
        }

        public virtual void Receive(char character) { }

        public void Open(string portName, int baudRate)
        {
            if (portName == "") return;
            this.serialPort.PortName = portName;
            this.serialPort.BaudRate = baudRate;
            try
            {
                serialPort.Open();
            }
            catch
            {
                // ignored
            }
        }

        public bool IsOpen()
        {
            return this.serialPort.IsOpen;
        }
        public void Close()
        {
            this.serialPort.Close();
        }
        public void Write(Frame frame)
        {
            if (this.serialPort.IsOpen)
            {
                if (BadCrcChecksum)
                    frame.setWrongCrc();
                this.serialPort.Write(frame.ToWrite());
            }
        }
        public string[] GetAvaliablePorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}