using UnityEngine;

public class CatScript : BaseScript
{
    public override void MonoBehaviourVirtualStart()
    {
        base.MonoBehaviourVirtualStart();
        Debug.Log("CatScript: MonoBehaviourVirtualStart() called");
    }
}