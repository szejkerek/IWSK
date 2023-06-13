using System;

namespace RS_485
{
    class SerialInterfaceEvent : EventArgs
    {
        public string data { get; }

        public SerialInterfaceEvent(string data)
        {
            this.data = data;
        }
    }
}
