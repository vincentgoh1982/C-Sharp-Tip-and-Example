using UnityEngine;

public class DogScript : BaseScript
{
    public override void MonoBehaviourVirtualStart()
    {
        base.MonoBehaviourVirtualStart();
        Debug.Log("DogScript: MonoBehaviourVirtualStart() called");
    }
}
