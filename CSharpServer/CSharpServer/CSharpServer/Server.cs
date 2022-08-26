using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace CSharpServer
{
    class Server
    {
        public static void Start(TcpListener listener)
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"Client ({client.ToString()}) accepted.");
                NetworkStream stream = client.GetStream();
                StreamReader streamReader = new StreamReader(client.GetStream());
                StreamWriter streamWriter = new StreamWriter(client.GetStream());

                try
                {
                    byte[] buffer = new byte[1024];//Created 1024 empty bytes
                    stream.Read(buffer, 0, buffer.Length);

                    int sizeMagic = sizeof(int); //returns the number of bytes used to store an integer 
                    byte[] magicData = new byte[sizeMagic];//Create new btyes to store

                    for (int i = 0; i < sizeMagic; i++)
                    {
                        magicData[i] = buffer[i];//Transfer the bytes to magicData
                    }

                    int magic = BitConverter.ToInt32(magicData);//Convert into int to find the string length

                    //retrieve real data
                    byte[] data = new byte[magic];
                    Buffer.BlockCopy(buffer, sizeof(int), data, 0, magic);
                    //To get the string actual bytes so that the remaining bytes to be remove
                    //The source buffer, The zero-based byte offset into , The destination buffer, The zero-based byte offset into, The number of bytes to copy.

                    string message = Encoding.UTF8.GetString(data);

                    Console.WriteLine("request received by: " + message);

                    if (message == "Apple")
                    {
                        Console.WriteLine("Correct.");
                        streamWriter.WriteLine($"You have received a message from server: {message}");
                        streamWriter.Flush();
                    }
                    else if (message == "Orange")
                    {
                        Console.WriteLine("Correct.");
                        streamWriter.WriteLine($"You have received a message from server: {message}");
                        streamWriter.Flush();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect.");
                        streamWriter.WriteLine($"This is not the correct answer: {message}");
                        streamWriter.Flush();
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to send.");
                    streamWriter.WriteLine(ex.ToString());
                }
            }
        }

    }
}