
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace PolymorphismInterface
{

    public class InterfaceEncoder : MonoBehaviour
    {
        public Text textAppear;

        private void OnDestroy()
        {
            MailNotificationChannel.interfaceMailServiceDelegate -= ChangeEmail;
            SmsNotificationChannel.interfaceSmsServiceDelegate -= ChangeEmail;
            DVD.dvdDelegate -= ChangeTitle;
            Message.messageDelegate -= ChangeBytes;
        }
        private void Start()
        {
            MailNotificationChannel.interfaceMailServiceDelegate += ChangeEmail;
            SmsNotificationChannel.interfaceSmsServiceDelegate += ChangeEmail;
            DVD.dvdDelegate += ChangeTitle;
            Message.messageDelegate += ChangeBytes;
        }

        [Button]
        public void Press()
        {
            var encoder = new VideoEncoder();
            encoder.RegisterNotificationChannel(new MailNotificationChannel());
            encoder.RegisterNotificationChannel(new SmsNotificationChannel());
            encoder.Encode(new DVD("Interface World"));
        }
        private void ChangeEmail(string answer)
        {
            textAppear.text = textAppear.text + answer.ToString() + System.Environment.NewLine;
        }

        private void ChangeTitle(string answer)
        {
            textAppear.text = textAppear.text + "Video title: " + answer.ToString() + System.Environment.NewLine;
        }
        private void ChangeBytes(float answer)
        {
            textAppear.text = textAppear.text + "Bytes: " + answer.ToString() + System.Environment.NewLine;
        }
    }

}
