using NaughtyAttributes;
using UnityEngine;

public class DelegateHandler : MonoBehaviour
{
    public delegate void OnButtonClickDelegate(string Name);
    public static OnButtonClickDelegate buttonClickDelegate;

    public delegate void OnButton2ClickDelegate(string Name);
    public event OnButton2ClickDelegate onButton2ClickDelegate;
    public void Start()
    {
        GameObject HideOnStart = GameObject.Find("SecondCanvas(Clone)");
        HideOnStart.SetActive(false);
    }

    [Button]
    public void PressVincent()
    {
        buttonClickDelegate("Vincent");
    }

    [Button]
    public void PressJason()
    {
        onButton2ClickDelegate("Jason");
    }
}
