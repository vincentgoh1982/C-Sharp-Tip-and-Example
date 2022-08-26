using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClientController: ClientElement
{
    public void SendApple()
    {
        Send("Apple");
    }

    public void SendOrange()
    {
        Send("Orange");
    }

    private void Send(string fruit)
    {
        try
        {
            app.client = new TcpClient(app.model.serverIp, app.model.port); //Connect to server

            string messageToSend = fruit;
            int length = messageToSend.Length;

            ConvertToBytes(length, messageToSend);
        }
        catch (Exception ex)
        {
            Debug.Log($"Failed to connect to server. {ex}");
        }
    }
    //Copies a specified number of bytes from a source array starting at a particular offset to a destination array starting at a particular offset.
    public static byte[] Combine(byte[] first, byte[] second)
    {
        byte[] bytes = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
        Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
        return bytes;
    }

    //Convert strinng to Bytes
    private void ConvertToBytes(int length, string messageToSend)
    {
        byte[] data = Combine(BitConverter.GetBytes(length), Encoding.UTF8.GetBytes(messageToSend));//the length of the string, message to send

        NetworkStream stream = app.client.GetStream();//Send the buffer.
        stream.Write(data, 0, data.Length);
        Debug.Log("sending data to server.");

        StreamReader streamReader = new StreamReader(stream);//read reply from server
        string response = streamReader.ReadLine();
        Debug.Log(response);

        stream.Close();
        app.client.Close();
    }
}