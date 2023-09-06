using UnityEngine;

public class BaseScript : MonoBehaviour
{
    // Define a virtual method for starting the scripts
    public virtual void MonoBehaviourVirtualStart()
    {
        Debug.Log("BaseScript: Default MonoBehaviourVirtualStart() called");
    }
}
