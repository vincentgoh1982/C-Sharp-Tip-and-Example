using UnityEngine;

// Base class for all elements in this application.
public class BounceElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public BounceApplication app { 
        get 
        { 
            return GameObject.FindObjectOfType<BounceApplication>(); 
        } 
    }
}

// 10 Bounces Entry Point.
public class BounceApplication : MonoBehaviour
{
    // Reference to the root instances of the MVC.
    public BounceModel model;
    public BounceView view;
    public BounceController controller;

    // Init things here
    void Start() { }
}

