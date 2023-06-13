using System;

namespace RS_485
{
    public class ModbusMsg : EventArgs
    {
        public string message { get; set; }
        public ModbusMsg(string message)
        {
            this.message = message;
        }
    }
}