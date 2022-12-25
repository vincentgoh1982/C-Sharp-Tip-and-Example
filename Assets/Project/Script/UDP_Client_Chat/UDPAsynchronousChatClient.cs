using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UDP_Asynchronous_Chat
{
    public class UDPAsynchronousChatClient : UDPChatGeneral
    {
        Socket mSockBroadCastSender;
        IPEndPoint mIPEPBroadcast;
        IPEndPoint mIPEPLocal;
        private EndPoint mChatServerEP;

        //Added
        Thread receiveThread;

        public UDPAsynchronousChatClient(int _localPort, int _remotePort)
        {
            mIPEPBroadcast = new IPEndPoint(IPAddress.Broadcast, _remotePort);
            mIPEPLocal = new IPEndPoint(IPAddress.Any, _localPort);

            mSockBroadCastSender = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);

            mSockBroadCastSender.EnableBroadcast = true;
        }

        //Looping for receiving information from server
        private void ReceiveUpdateTextFromServer()
        {
            while(true)
            {
                try
                {
                    SocketAsyncEventArgs saeaSendConfirmation = new SocketAsyncEventArgs();
                    saeaSendConfirmation.SetBuffer(new byte[1024], 0, 1024);
                    saeaSendConfirmation.RemoteEndPoint = mIPEPLocal;

                    saeaSendConfirmation.UserToken = "<CONFIRM>";

                    saeaSendConfirmation.Completed += ReceiveComfirmationCompleted;

                    mSockBroadCastSender.ReceiveFromAsync(saeaSendConfirmation);
                }

                catch(Exception err)
                {
                    Debug.Log(err.ToString());
                }
            }
        }

        public void SendBroadcast(string strDataForBroadcast)
        {
            if (string.IsNullOrEmpty(strDataForBroadcast))
            {
                return;
            }
            try
            {
                if (!mSockBroadCastSender.IsBound)
                {
                    mSockBroadCastSender.Bind(mIPEPLocal);
                }

                ChatPacket objChatPacket = new ChatPacket();
                objChatPacket.Message = strDataForBroadcast;
                objChatPacket.PacketType = PACKET_TYPE.DISCOVERY;

                string strJSONDiscovery = JsonConvert.SerializeObject(objChatPacket);

                var dataBytes = Encoding.ASCII.GetBytes(strJSONDiscovery);

                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(dataBytes, 0, dataBytes.Length);
                saea.RemoteEndPoint = mIPEPBroadcast;

                saea.UserToken = objChatPacket;
                saea.Completed += SendCompletedCallback;

                mSockBroadCastSender.SendToAsync(saea);
            }
            catch (Exception exc)
            {
                Debug.Log(exc.ToString());
                throw;
            }
        }

        private void SendCompletedCallback(object sender, SocketAsyncEventArgs e)
        {
            Debug.Log($"SendCompletedCallback Data sent successfully to: {e.RemoteEndPoint}");

            if ((e.UserToken as ChatPacket).PacketType == PACKET_TYPE.DISCOVERY)
            {
                ReceiveTextFromServer(expectedValue: "<CONFIRM>", IPEPReceiverLocal: mIPEPLocal);
            }
        }

        private void ReceiveTextFromServer(string expectedValue, IPEndPoint IPEPReceiverLocal)
        {
            if (IPEPReceiverLocal == null)
            {
                Debug.Log("No IPEndpoint specified");
                return;
            }

            SocketAsyncEventArgs saeaSendConfirmation = new SocketAsyncEventArgs();
            saeaSendConfirmation.SetBuffer(new byte[1024], 0, 1024);
            saeaSendConfirmation.RemoteEndPoint = IPEPReceiverLocal;

            saeaSendConfirmation.UserToken = expectedValue;

            saeaSendConfirmation.Completed += ReceiveComfirmationCompleted;

            mSockBroadCastSender.ReceiveFromAsync(saeaSendConfirmation);
        }

        private void ReceiveComfirmationCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0)
            {
                Debug.Log($"Zero bytes transferred, socket error: {e.SocketError}");
                return;
            }

            var receivedText = Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred);

            var expectedText = Convert.ToString(e.UserToken);

            ChatPacket objPacketConfirmation = JsonConvert.DeserializeObject<ChatPacket>(receivedText);

            if (objPacketConfirmation.PacketType == PACKET_TYPE.CONFIRMATION && objPacketConfirmation.Message.Equals(expectedText))
            {
                mChatServerEP = e.RemoteEndPoint;
                ReceiveTextFromServer(string.Empty, mChatServerEP as IPEndPoint);
                Debug.Log($"Received confirmation from server. {e.RemoteEndPoint}");
                OnRaisePrintStringEvent(new PrintStringEventArgs($"Received confirmation from server. {e.RemoteEndPoint}"));

            }
            else if (objPacketConfirmation.PacketType == PACKET_TYPE.TEXT &&
                !string.IsNullOrEmpty(objPacketConfirmation.Message))
            {
                Debug.Log($"Text received: {objPacketConfirmation.Message}");
                OnRaisePrintStringEvent(new PrintStringEventArgs($"Text received: {objPacketConfirmation.Message}"));

                ReceiveTextFromServer(string.Empty, mChatServerEP as IPEndPoint);
            }
            else if (objPacketConfirmation.PacketType == PACKET_TYPE.CONFIRMATION && !string.IsNullOrEmpty(expectedText) && !objPacketConfirmation.Message.Equals(expectedText))
            {
                Debug.Log($"Expected token not returned by the server.");
                OnRaisePrintStringEvent(new PrintStringEventArgs($"Expected token not returned by the server."));
            }
        }

        public void SendMessageToKnownServer(string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    return;
                }

                ChatPacket objMessagePacket = new ChatPacket();
                objMessagePacket.Message = message;
                objMessagePacket.PacketType = PACKET_TYPE.TEXT;

                var bytesToSend = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(objMessagePacket));

                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(bytesToSend, 0, bytesToSend.Length);

                saea.RemoteEndPoint = mChatServerEP;

                saea.UserToken = message;

                saea.Completed += SendMessageToKnownServerCompletedCallback;

                mSockBroadCastSender.SendToAsync(saea);
            }
            catch (Exception excp)
            {

                Debug.Log(excp.ToString());
            }
        }

        private void SendMessageToKnownServerCompletedCallback(object sender, SocketAsyncEventArgs e)
        {
            Debug.Log($"Sent: {e.UserToken}{Environment.NewLine}Server: {e.RemoteEndPoint}");
        }
    }
}
