using UnityEngine;

namespace PolymorphismInterface
{
    public class MailNotificationChannel : INotificationChannel
    {
        public delegate void InterfaceMailServiceDelegate(string Title);
        public static InterfaceMailServiceDelegate interfaceMailServiceDelegate;
        public void Send(Message message, DVD dvd)
        {
            Debug.Log($"Sending mail....Btyes: {message.fileSize}, Title: {dvd.Title}");
            interfaceMailServiceDelegate("Email send:" + message.fileSize.ToString() + "mb files");
        }

    }
}