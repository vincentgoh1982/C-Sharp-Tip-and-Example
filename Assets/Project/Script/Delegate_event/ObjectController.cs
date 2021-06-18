using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{
    private Image image;
    private bool switches = false;
    public Text UserName;
    // Use this for initialization
    void Start()
    {
        DelegateHandler.buttonClickDelegate += ChangeColor;
        image = this.gameObject.GetComponent<Image>();
    }

    private void ChangeColor(string Name)
    {
        switches = !switches;
        if (switches)
        {
            UserName.text = Name;
            image.color = new Color32(0, 0, 225, 100);
        }
        else
        {
            UserName.text = "Hello World";
            image.color = new Color32(255, 0, 0, 100);
        }
    }

    // Unsubscribing Delegate
    private void OnDisable()
    {
        DelegateHandler.buttonClickDelegate -= ChangeColor;
    }

}
