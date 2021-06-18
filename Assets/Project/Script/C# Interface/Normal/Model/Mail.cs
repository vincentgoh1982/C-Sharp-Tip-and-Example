using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail
{
    public delegate void MailDelegate(float Btyes);
    public static MailDelegate mailDelegate;

    // Constructor that takes no arguments:
    public Mail()
    {
        fileSize = 0.0f;
    }

    // Constructor that takes one argument:
    public Mail(float videobtyes)
    {
        fileSize = videobtyes;
        mailDelegate(videobtyes);
    }

    // Auto-implemented readonly property:
    public float fileSize { get; }

}
