using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        // Press the space key to add the Scene additively and move the GameObject to that Scene
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            hideUI();
        }
    }
    private void hideUI()
    {
        GameObject ui2D = GameObject.Find("SecondCanvas(Clone)");
        ui2D.SetActive(false);
    }
}
