
namespace UDP_Chat_Client_Form
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.tbRemotePort = new System.Windows.Forms.TextBox();
            this.tbBroadcastText = new System.Windows.Forms.TextBox();
            this.btnSendBroadcast = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Remote Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Broadcast Text ";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Location = new System.Drawing.Point(100, 12);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(100, 20);
            this.tbLocalPort.TabIndex = 3;
            this.tbLocalPort.Text = "23001";
            // 
            // tbRemotePort
            // 
            this.tbRemotePort.Location = new System.Drawing.Point(100, 50);
            this.tbRemotePort.Name = "tbRemotePort";
            this.tbRemotePort.Size = new System.Drawing.Size(100, 20);
            this.tbRemotePort.TabIndex = 4;
            this.tbRemotePort.Text = "23000";
            // 
            // tbBroadcastText
            // 
            this.tbBroadcastText.Location = new System.Drawing.Point(100, 88);
            this.tbBroadcastText.Name = "tbBroadcastText";
            this.tbBroadcastText.Size = new System.Drawing.Size(100, 20);
            this.tbBroadcastText.TabIndex = 5;
            this.tbBroadcastText.Text = "<DISCOVER>";
            // 
            // btnSendBroadcast
            // 
            this.btnSendBroadcast.Location = new System.Drawing.Point(468, 10);
            this.btnSendBroadcast.Name = "btnSendBroadcast";
            this.btnSendBroadcast.Size = new System.Drawing.Size(84, 40);
            this.btnSendBroadcast.TabIndex = 6;
            this.btnSendBroadcast.Text = "Broadcast";
            this.btnSendBroadcast.UseVisualStyleBackColor = true;
            this.btnSendBroadcast.Click += new System.EventHandler(this.btnSendBroadcast_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Message Text:";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(231, 31);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(231, 89);
            this.tbMessage.TabIndex = 8;
            this.tbMessage.Text = "Hello World!!";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Console:";
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(12, 141);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.Size = new System.Drawing.Size(531, 297);
            this.tbConsole.TabIndex = 10;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(468, 56);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(84, 41);
            this.btnSendMessage.TabIndex = 11;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSendBroadcast);
            this.Controls.Add(this.tbBroadcastText);
            this.Controls.Add(this.tbRemotePort);
            this.Controls.Add(this.tbLocalPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Client Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.TextBox tbRemotePort;
        private System.Windows.Forms.TextBox tbBroadcastText;
        private System.Windows.Forms.Button btnSendBroadcast;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.Button btnSendMessage;
    }
}

