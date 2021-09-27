using UnityEngine;

public class Element_ServerResponse : MonoBehaviour
{
    public Application_ServerResponse app
    {
        get
        {
            return GameObject.FindObjectOfType<Application_ServerResponse>();
        }
    }
}

public class Application_ServerResponse : MonoBehaviour
{
    // Reference to the root instances of the MVC.
    public Model_ServerResponse model;
    public Controller_ServerResponse controller;
    void Start()
    {
        
    }

}
