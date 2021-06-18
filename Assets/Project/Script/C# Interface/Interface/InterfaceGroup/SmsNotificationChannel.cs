using UnityEngine;

namespace PolymorphismInterface
{
    public class SmsNotificationChannel : INotificationChannel
    {
        public delegate void InterfaceSmsServiceDelegate(string Title);
        public static InterfaceSmsServiceDelegate interfaceSmsServiceDelegate;
        public void Send(Message message, DVD dvd)
        {
            Debug.Log($"Sending sms.... Btyes: {message.fileSize}, Title: {dvd.Title}");
            interfaceSmsServiceDelegate("SMS send:" + message.fileSize.ToString() + "mb files");
        }
    }
}
