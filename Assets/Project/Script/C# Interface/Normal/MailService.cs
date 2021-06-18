using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Normal_Encoder
{
    public class MailService
    {
        public delegate void MailServiceDelegate(string Title);
        public static MailServiceDelegate mailServiceDelegate;
        public void Send(Mail mail)
        {
            Debug.Log($"Send: {mail.fileSize}");
            mailServiceDelegate("Email send:" + mail.fileSize.ToString() + "mb files");
        }
    }
}
