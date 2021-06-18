using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Normal_Encoder
{
    public class NormalEncoder : MonoBehaviour
    {
        public Text textAppear;

        private void OnDestroy()
        {
            Mail.mailDelegate -= ChangeBytes;
            Video.videoDelegate -= ChangeTitle;
            MailService.mailServiceDelegate -= ChangeEmail;
        }
        private void Start()
        {
            Mail.mailDelegate += ChangeBytes;
            Video.videoDelegate += ChangeTitle;
            MailService.mailServiceDelegate += ChangeEmail;
        }
        private void ChangeBytes(float answer)
        {
            textAppear.text = textAppear.text + "Bytes: "  + answer.ToString() + System.Environment.NewLine;
        }

        private void ChangeTitle(string answer)
        {
            textAppear.text = textAppear.text + "Video title: " +  answer.ToString() + System.Environment.NewLine;
        }

        private void ChangeEmail(string answer)
        {
            textAppear.text = textAppear.text + answer.ToString() + System.Environment.NewLine;
        }

        [Button]
        public void Press()
        {
            var encoder = new VideoEncoder();
            encoder.Encode(new Video("Hello World"));

            var encoder2 = new VideoEncoder();
            encoder2.Encode(new Video("Bye World"));
        }
    }
}
