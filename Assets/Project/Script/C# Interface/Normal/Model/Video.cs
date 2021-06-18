using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video
{
    public delegate void VideoDelegate(string Title);
    public static VideoDelegate videoDelegate;

    // Constructor that takes no arguments:
    public Video()
    {
        Title = "unknown";
    }

    // Constructor that takes one argument:
    public Video(string title)
    {
        Title = title;
        videoDelegate(Title);
    }

    // Auto-implemented readonly property:
    public string Title { get; }

    // Method that overrides the base class (System.Object) implementation.
    public override string ToString()
    {
        return Title;
    }
}
