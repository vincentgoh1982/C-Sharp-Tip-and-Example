using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_Pooling : MonoBehaviour
{
    // Gives access to the application and all instances.
    public Application_Pooling app
    {
        get
        {
            return GameObject.FindObjectOfType<Application_Pooling>();
        }
    }
}

public class Application_Pooling : MonoBehaviour
{
    // Reference to the root instances of the MVC.
    public Model_Pooling model;
    public View_Pooling view;
    public Controller_Pooling controller;

    // Init things here
    void Start() { }
}
