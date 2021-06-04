using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    private Image image;
    private bool switches = false;
    // Use this for initialization
    void Start()
    {
        DelegateHandler.buttonClickDelegate += ChangeColor;
        image = this.gameObject.GetComponent<Image>();
    }

    private void ChangeColor()
    {
        switches = !switches;
        if (switches)
        {
            image.color = new Color32(0, 0, 225, 100);
        }
        else
        {
            image.color = new Color32(255, 0, 0, 100);
        }
    }

    // Unsubscribing Delegate
    private void OnDisable()
    {
        DelegateHandler.buttonClickDelegate -= ChangeColor;
    }

}
