using System;

namespace UDP_Asynchronous_Chat
{
    public class UDPChatGeneral
    {
        public EventHandler<PrintStringEventArgs> RaisePrintStringEvent;

        protected virtual void OnRaisePrintStringEvent(PrintStringEventArgs e)
        {
            EventHandler<PrintStringEventArgs> handler = RaisePrintStringEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
