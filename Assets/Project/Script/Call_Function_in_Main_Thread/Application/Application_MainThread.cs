using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_MainThread : MonoBehaviour
{
    public Application_MainThread app
    {
        get
        {
            return GameObject.FindObjectOfType<Application_MainThread>();
        }
    }
}


public class Application_MainThread : MonoBehaviour
{
    public Controller_Network controller;
    public Model_Network model;
    public View_MainThread view;
    public Controller_OtherToMainThread controllerMainThread;
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
