using NaughtyAttributes;
using UnityEngine;

public class DelegateHandler : MonoBehaviour
{
    public delegate void OnButtonClickDelegate();
    public static OnButtonClickDelegate buttonClickDelegate;

    public void Start()
    {
        GameObject HideOnStart = GameObject.Find("SecondCanvas(Clone)");
        HideOnStart.SetActive(false);
    }

    [Button]
    public void Press()
    {
        buttonClickDelegate();
    }
}
