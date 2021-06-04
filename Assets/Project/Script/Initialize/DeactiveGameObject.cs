using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveGameObject : MonoBehaviour
{
    [System.Obsolete]
    void Awake()
    {
        this.gameObject.active=false;
    }

}
