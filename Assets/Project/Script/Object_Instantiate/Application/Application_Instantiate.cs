using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_Instantiate : MonoBehaviour
{
    // Gives access to the application and all instances.
    public Application_Instantiate app
    {
        get
        {
            return GameObject.FindObjectOfType<Application_Instantiate>();
        }
    }
}
public class Application_Instantiate : MonoBehaviour
{
    // Reference to the root instances of the MVC.
    public Model_Instantiate model;
    public View_Instantiate view;
    public Controller_Instantiate controller;

    // Init things here
    void Start() { }
}
