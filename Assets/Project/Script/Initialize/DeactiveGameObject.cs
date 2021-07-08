using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveGameObject : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(OffUI());
    }
    [System.Obsolete]
    IEnumerator OffUI()
    {
        yield return new WaitForSecondsRealtime(5);

        this.gameObject.active = false;
    }
}
