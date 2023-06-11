using System;

namespace RS_485
{
    class SerialInterfacePEvent : EventArgs
    {
        public bool DSR { get; }
        public bool CTS { get; }

        public SerialInterfacePEvent(bool dSR, bool cTS)
        {
            DSR = dSR;
            CTS = cTS;
        }
    }
}
