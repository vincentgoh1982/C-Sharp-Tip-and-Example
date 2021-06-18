using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Normal_Encoder
{
    public class VideoEncoder
    {
        private readonly MailService mailService;

        public VideoEncoder()
        {
            mailService = new MailService();
        }

        public void Encode(Video video)
        {
            Debug.Log($"Encode: {video.Title}");
            mailService.Send(new Mail(1.0f));
        }

    }
}
