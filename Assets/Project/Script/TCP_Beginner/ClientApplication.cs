using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class ClientElement : MonoBehaviour
{
    // Gives access to the application and all instances.
    public ClientApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<ClientApplication>();
        }
    }
}

public class ClientApplication : MonoBehaviour
{
    public TcpClient client;
    public ClientModel model;
    public ClientController controller;

    private void Start()
    {
        string path = Application.dataPath + "/StreamingAssets";

        if (Directory.Exists(path))
        {
            Debug.Log($"It exists inside this directory: {path}");
        }
        else
        {
            Directory.CreateDirectory(path);
        }
    }
}
