using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_project
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        Socket ClientSocket;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_IP.Text;
            int portNum;
            if(Int32.TryParse(textBox_Port.Text, out portNum))
            {
                try
                {
                    ClientSocket.Connect(IP, portNum);
                    button_Connect.Enabled = false;
                    textBox_Message.Enabled = true;
                    button_Send.Enabled = true;
                    connected = true;
                   
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                    string username = textBox_UserName.Text;
                    if (username != "" && username.Length <= 64)
                    {
                        Byte[] buffer = Encoding.Default.GetBytes(username);
                        ClientSocket.Send(buffer);
                    }

                }
                catch
                {
                    RichTextBox.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                RichTextBox.AppendText("Check the port\n");
            }
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    ClientSocket.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if(incomingMessage == "")
                    {
                        ClientSocket.Close();
                        connected = false;
                        button_Connect.Enabled = true;
                        textBox_Message.Enabled = false;
                        button_Send.Enabled = false;
                        break;
                    }

                    RichTextBox.AppendText("Server: " + incomingMessage + "\n");
                }
                catch{
                    if(!terminating)
                    {
                        RichTextBox.AppendText("The server has disconnected\n");
                        //button_Connect.Enabled = true;
                        textBox_Message.Enabled = false;
                        button_Send.Enabled = false;
                        connected = false;

                        string IP = textBox_IP.Text;
                        int portNum = Int32.Parse(textBox_Port.Text);

                        RichTextBox.AppendText("Trying to connect.\n");
                        ClientSocket.Close();
                        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                        while (!connected)
                        {
                            try
                            {
                                ClientSocket.Connect(IP, portNum);
                                textBox_Message.Enabled = true;
                                button_Send.Enabled = true;
                                connected = true;
                                RichTextBox.AppendText("Connected again to server.\n");

                                string username = textBox_UserName.Text;
                                if (username != "" && username.Length <= 64)
                                {
                                    Byte[] buffer = Encoding.Default.GetBytes(username);
                                    ClientSocket.Send(buffer);
                                }
                            }
                            catch
                            {
                                Thread.Sleep(100);
                            }
                        }

                    }
                }
            }
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            string message = textBox_Message.Text;
            if (message != "" && message.Length <=255)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                ClientSocket.Send(buffer);
            }
        }
    }
}
