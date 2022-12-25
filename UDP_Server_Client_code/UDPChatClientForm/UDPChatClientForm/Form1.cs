using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDP_Asynchronous_Chat;

namespace UDP_Chat_Client_Form
{
    public partial class Form1 : Form
    {
        UDP_Asynchronous_Chat.UDPAsynchronousChatClient mChatClient;
        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSendBroadcast_Click(object sender, EventArgs e)
        {
            if(mChatClient == null)
            {
                int.TryParse(tbLocalPort.Text, out var nLockPort);
                int.TryParse(tbRemotePort.Text, out var nRemotePort);
                mChatClient = 
                    new UDP_Asynchronous_Chat.UDPAsynchronousChatClient
                    (nLockPort, nRemotePort);

                mChatClient.RaisePrintStringEvent += chatClient_PrintString;
            }

            mChatClient.SendBroadcast(tbBroadcastText.Text);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            mChatClient.SendMessageToKnownServer(tbMessage.Text);
        }

        private void chatClient_PrintString(object sender, PrintStringEventArgs e)
        {
            Action<string> print = PrintToTextBox;
            tbConsole.Invoke(print, new string[] { e.MessageToPrint });
            //tbConsole.Text +=$"{Environment.NewLine}{DateTime.Now} - {e.MessageToPrint}"
        }

        private void PrintToTextBox(string stringToPrint)
        {
            tbConsole.Text += $"{Environment.NewLine}{DateTime.Now} - {stringToPrint}";
        }
    }
}
