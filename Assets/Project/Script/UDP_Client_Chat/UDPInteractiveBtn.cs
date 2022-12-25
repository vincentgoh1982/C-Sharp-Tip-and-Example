using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UDP_Asynchronous_Chat
{
    public class UDPInteractiveBtn : MonoBehaviour
    {
        private UDPAsynchronousChatClient mChatClient;

        public TMP_InputField tbLocalPort;
        public TMP_InputField tbRemotePort;
        public TMP_InputField tbConsole;
        public TMP_InputField tbBroadcastText;
        public TMP_InputField tbMessage;
        private string updateText;

        public void buttonUpdate()
        {
            tbConsole.ForceLabelUpdate();
        }
        public void btnSendBroadcast_Click()
        {
            if (mChatClient == null)
            {
                int.TryParse(tbLocalPort.text, out var nLockPort);
                int.TryParse(tbRemotePort.text, out var nRemotePort);
                mChatClient =
                    new UDPAsynchronousChatClient
                    (nLockPort, nRemotePort);

                mChatClient.RaisePrintStringEvent += chatClient_PrintString;
            }

            string broadcastText = tbBroadcastText.text.ToString().TrimEnd('\r', '\n');

            mChatClient.SendBroadcast(broadcastText);
        }

        public void btnSendMessage_Click()
        {
            string messageText = tbMessage.text;
            mChatClient.SendMessageToKnownServer(messageText);
        }

        private void chatClient_PrintString(object sender, PrintStringEventArgs e)
        {
            updateText += $"{Environment.NewLine}{DateTime.Now} - {e.MessageToPrint}";

        }

        private void Update()
        {
            tbConsole.text = updateText;
        }

    }
}
