using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    private Image image;
    public Text UserName;
    public DelegateHandler delegateHandler;

    // Use this for initialization
    void Start()
    {
        DelegateHandler.buttonClickDelegate += ChangeColor;
        delegateHandler.onButton2ClickDelegate += Call;
        image = this.gameObject.GetComponent<Image>();
    }

    private void ChangeColor(string Name)
    {
        UserName.text = Name;
        image.color = new Color32(225, 0, 0, 100);
    }

    private void Call(string Name)
    {
        UserName.text = Name;
        image.color = new Color32(0, 0, 225, 100);
    }

    // Unsubscribing Delegate
    private void OnDisable()
    {
        DelegateHandler.buttonClickDelegate -= ChangeColor;
        delegateHandler.onButton2ClickDelegate -= Call;
    }

}
