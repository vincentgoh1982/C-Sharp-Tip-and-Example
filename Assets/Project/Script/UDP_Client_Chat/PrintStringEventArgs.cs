using System;

namespace UDP_Asynchronous_Chat
{
    public class PrintStringEventArgs : EventArgs
    {
        public string MessageToPrint { get; set; }

        public PrintStringEventArgs(string _message) => MessageToPrint = _message;
    }
}