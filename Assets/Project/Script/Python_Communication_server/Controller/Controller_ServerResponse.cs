using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Controller_ServerResponse : Element_ServerResponse
{
    private Thread clientReceiveThread;
    private TcpClient socketConnection;
    private string updateText;


    // Initialization  
    private void Start()
    {
        ConnectToTcpServer();
    }

    private void OnDestroy()
    {
        EndThread();
    }

    /// <summary>   
    /// End socket connection.    
    /// </summary>  
    void EndThread()
    {
        try
        {
            if (clientReceiveThread.IsAlive)
            {
                clientReceiveThread.Abort();
                Debug.Log("Trying to abort");
            }
        }
        catch
        {
            Debug.Log("Aborting thread failed");
        }
    }

    /// <summary>   
    /// Setup socket connection.    
    /// </summary>  
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    /// <summary>   
    /// Runs in background clientReceiveThread; Listens for incomming data.     
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient("172.16.1.6", 1234);
            Debug.Log("Connection successful");
            Byte[] bytes = new Byte[1024];

            while (true)
            {
                // Get a stream object for reading              
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary.                  
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message.                        
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("server message received as: " + serverMessage);
                        updateText = serverMessage;
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    /// <summary>
    /// Quick testing for sending
    /// </summary>
    public void SendMessageButton()
    {
       SendMessage("Test Message from the Hololens");
    }

    /// <summary>   
    /// Send message to server using socket connection.     
    /// </summary>  
    public void SendMessage(string clientMessage)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing.             
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                //string clientMessage = "This is a message from one of your clients.";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent message: " + clientMessage);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}
