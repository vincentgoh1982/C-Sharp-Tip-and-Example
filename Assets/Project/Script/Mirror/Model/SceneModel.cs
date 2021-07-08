using System.Collections.Generic;
using UnityEngine;

public class SceneModel : SceneElement
{
    [Tooltip("Scene's name to load on the main scene")]
    public List<string> sceneName = new List<string>();
}
