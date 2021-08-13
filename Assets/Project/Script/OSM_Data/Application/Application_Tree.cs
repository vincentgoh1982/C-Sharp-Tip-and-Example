using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public Application_Tree app
    {
        get
        {
            return GameObject.FindObjectOfType<Application_Tree>();
        }
    }
}
public class Application_Tree : MonoBehaviour
{
    // Reference to the root instances of the MVC.
    public Model_Tree model;
    public View_Tree view;
    public Controller_OSMRequestArea controller;

    // Init things here
    void Start() { }
}
