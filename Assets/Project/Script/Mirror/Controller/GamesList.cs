using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuickStart
{
    public class GamesList : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
