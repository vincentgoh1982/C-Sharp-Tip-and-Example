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

namespace UDPChatServerForm
{
    public partial class Form1 : Form
    {
        UDP_Asynchronous_Chat.UDPAsynchronousChatServer mUDPChatServer;
        public Form1()
        {
            mUDPChatServer = new UDP_Asynchronous_Chat.UDPAsynchronousChatServer();
            mUDPChatServer.RaisePrintStringEvent += chatClient_PrintString;
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            mUDPChatServer.StartReceivingData();
        }

        private void tbConsole_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
