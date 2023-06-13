using System;
using System.IO.Ports;
using System.Text;

namespace RS_485
{

    public class Frame
    {
        public char addres { get; set; }
        public char function { get; set; }
        public string args { get; set; }
        public char Lrc { get; set; }

        string byteArray;

        public Frame(char addres, char function, string args)
        {
            this.addres = addres;
            this.function = function;
            this.args = args;
            byteArray = ((byte)this.addres).ToString("x2") + ((byte)this.function).ToString("x2") + this.args;
            this.Lrc = (char)Crc.ComputeChecksum(Encoding.ASCII.GetBytes(byteArray));
            byteArray += ((byte)this.Lrc).ToString("x2");
        }
        public Frame(string hexdata)
        {
            if (hexdata.Length < 6)
                return;
            this.addres = (char)Convert.ToByte(hexdata.Substring(0, 2), 16);
            this.function = (char)Convert.ToByte(hexdata.Substring(2, 2), 16);
            this.args = hexdata.Substring(4, hexdata.Length - 2 - 4);
            this.Lrc = (char)Convert.ToByte(hexdata.Substring(hexdata.Length - 2, 2), 16);

            byteArray = hexdata;
        }
        public bool CheckLRC()
        {
            return Lrc == Crc.ComputeChecksum(Encoding.ASCII.GetBytes(byteArray.Remove(byteArray.Length - 2)));
        }

        public string ToWrite()
        {
            return ":" + byteArray + "\r\n";
        }

        public string ToString()
        {

            return ":" + byteArray + "\\r\\n";
        }

        internal void setWrongCrc()
        {
            this.Lrc = 'b';
            byteArray = ((byte)this.addres).ToString("x2") + ((byte)this.function).ToString("x2") + this.args + ((byte)this.Lrc).ToString("x2");
        }
    }
}