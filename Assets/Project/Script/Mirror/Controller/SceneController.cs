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
        if (app.model.sceneName[app.model.num] != null)
        {
            SceneManager.LoadSceneAsync(app.model.sceneName[app.model.num], LoadSceneMode.Additive);
            app.model.num += 1;
        }
    }
}
