using System;
using System.Net;
using System.Net.Sockets;

namespace CSharpServer
{
    class Program
    {
        static string IPHost;
        static int port = 1302;
        static void Main(string[] args)
        {
            Console.Title = "ServerApp";
            UseDNS();

            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Server.Start(listener);

            Console.ReadKey();
        }

        #region Check Host ip address
        public static void UseDNS()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry local = Dns.GetHostByName(hostName);

            foreach (IPAddress ipaddress in local.AddressList)
            {
                IPHost = ipaddress.ToString();
            }

            if (isLocal(IPHost))
            {
                Console.WriteLine($"The local ip address is {IPHost} and port is {port}.");
            }
        }

        public static bool isLocal(string host)
        {
            try
            {
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch { }
            return false;
        }
        #endregion Check Host ip address
    }
}
