using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuickStart
{
    public class SceneScript : NetworkBehaviour
    {
        public Text canvasStatusText;
        public PlayerController playerScript;
        public SceneReference sceneReference;
        public Text canvasAmmoText;
        public GameObject cameraObject;

        [SyncVar(hook = nameof(OnStatusTextChanged))]//fires for changed values
        public string statusText;

        public void UIAmmo(int _value)
        {
            canvasAmmoText.text = "Ammo: " + _value;
        }

        void OnStatusTextChanged(string _Old, string _New)
        {
            //called from sync var hook, to update info on screen for all players
            canvasStatusText.text = statusText;
        }

        public void ButtonSendMessage()
        {
            if (playerScript != null)
            {
                playerScript.CmdSendPlayerMessage();
            }
        }

        public void ButtonChangeScene()
        {
            if (isServer)
            {
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "SecondScene") { NetworkManager.singleton.ServerChangeScene("MyOtherScene"); }

                else { NetworkManager.singleton.ServerChangeScene("SecondScene"); }
            }
            else { Debug.Log("You are not Host."); }
        }
    }
}
