using UnityEngine;

namespace QuickStart
{
    public class SceneReference : MonoBehaviour
    {
        public SceneScript sceneScript;// GameObject.Find() get the non-networked scene object, which will have those Network Identity scene object as pre-set variables.
    }
}
