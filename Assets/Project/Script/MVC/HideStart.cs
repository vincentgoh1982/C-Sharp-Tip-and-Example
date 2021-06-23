using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hideUI());
    }
     IEnumerator hideUI()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject ui2D = GameObject.Find("SecondCanvas(Clone)");
        ui2D.SetActive(false);
    }
}
