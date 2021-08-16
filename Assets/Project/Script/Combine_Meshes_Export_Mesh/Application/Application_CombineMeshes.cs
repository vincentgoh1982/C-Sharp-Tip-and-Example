using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshesElements : MonoBehaviour
{
    public Application_CombineMeshes app
    {
        get
        {
            return GameObject.FindObjectOfType<Application_CombineMeshes>();
        }
    }
}
public class Application_CombineMeshes : MonoBehaviour
{
    public Model_CombineMeshes model;
    public View_CombineMeshes view;
    public Controller_CombineMeshes controller;

    // Init things here
    void Start() { }
}
