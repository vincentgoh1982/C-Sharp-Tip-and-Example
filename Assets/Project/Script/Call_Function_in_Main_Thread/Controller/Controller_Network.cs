using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Controller_Network : Element_MainThread
{
    private Thread clientReceiveThread;
    private TcpClient socketConnection;

    private string ipAddress = string.Empty;
    private int portNumber;

    private string preMessage = string.Empty;

    private OnlineMaps map;

    public delegate void LonLatZoomDelegate(string lon_Lat_Zoom);
    public LonLatZoomDelegate LonLatZoomEvent;

    private void Awake()
    {
        map = app.view.map;
        ipAddress = app.model.ipAddress;
        portNumber = app.model.portNumber;
    }

    public void ButtonPress()=>GetPositionMap();


    private void GetPositionMap()
    {
        map.GetPosition(out double lng, out double lat);
        string lonLatZoom = "lon:" + (float)lng + "_lat:" + (float)lat + "_zoom:" + map.floatZoom;
        SendMessage(lonLatZoom);

    }

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
            socketConnection = new TcpClient(ipAddress, portNumber);
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
                        
                        string[] textSplit = serverMessage.Split(char.Parse("|")); //Separate the ip address and value
                        if (textSplit[1] != preMessage && textSplit.Length == 2) //Compare previous value
                        {
                            LonLatZoomEvent(textSplit[1]);
                            preMessage = textSplit[1];
                        }
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
