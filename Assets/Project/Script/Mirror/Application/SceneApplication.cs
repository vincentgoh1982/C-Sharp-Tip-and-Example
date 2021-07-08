using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public SceneApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<SceneApplication>();
        }
    }
}

public class SceneApplication : MonoBehaviour
{
    public SceneController controller;
    public SceneModel model;
    public SceneView view;
}
