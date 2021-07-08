using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SceneElement
{
    void Update()
    {
        // Press the space key to add the Scene additively and move the GameObject to that Scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadYourAsyncScene();
        }
    }

    private void LoadYourAsyncScene()
    {
        foreach(string sceneName in app.model.sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}
