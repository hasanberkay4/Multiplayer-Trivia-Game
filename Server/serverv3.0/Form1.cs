using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serverv3._0
{
    public partial class Form1 : Form
    {
        bool listening = false;
        bool terminating = false;
        bool isFirst = true;
        bool acceptDirectly = true;
        bool questionAsked = false;
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        class Questions
        {
            List<KeyValuePair<string, double>> questions;
            int questionIndex;

            public static Questions initializeQuestions(string path)
            {
                Questions newQuestions = new Questions();
                newQuestions.questionIndex = -1;
                newQuestions.questions = new List<KeyValuePair<string, double>>();

                string line;
                double answer;

                StreamReader sr = null;
                sr = new StreamReader(path);

                while (true)
                {
                    line = sr.ReadLine();

                    if (line == null)
                        break;
                    else
                    {
                        answer = double.Parse(sr.ReadLine());
                    }

                    newQuestions.addQuestion(line, answer);
                }
                sr.Close();

                return newQuestions;
            }

            public void addQuestion(string question, double answer)
            {
                KeyValuePair<string, double> newQuestion = new KeyValuePair<string, double>(question, answer);
                questions.Add(newQuestion);
            }

            public string getQuestion()
            {
                questionIndex = (++questionIndex == questions.Count) ? 0 : questionIndex;

                return questions[questionIndex].Key;
            }

            public double getAnswer()
            {
                return questions.ElementAt(questionIndex).Value;
            }
        };

        class Client
        {
            public Socket socket;
            public string name;
            public double score;
            public double answer;
            public bool answered;
            public bool inGame;

            public Client(ref Socket socket, string name, bool inGame = false)
            {
                this.socket = socket;
                this.name = name;
                this.inGame = inGame;
                score = 0;
                answer = 0;
                answered = false;
            }

            public void answerQuestion(double answer)
            {
                this.answer = answer;
                answered = true;
            }

            public void incrementScore(double score)
            {
                this.score += score;
            }
        };

        class Clients
        {
            public List<Client> clients;
            public int totalAnswers;
            public bool moreThan1Player;

            public static Clients initializeClients()
            {
                Clients newList = new Clients();
                newList.clients = new List<Client>();
                newList.totalAnswers = 0;
                newList.moreThan1Player = false;

                return newList;
            }

            public bool containsName(string name)
            {
                foreach (Client currClient in clients)
                {
                    if (currClient.name == name)
                        return true;
                }

                return false;
            }

            public void sortClients()
            {
                clients.Sort((c1, c2) =>
                {
                    return c2.score.CompareTo(c1.score);
                });
            }

            public List<string> scoreboardText()
            {
                List<string> scoreboard = new List<string>();
                sortClients();
                for (int index = 0; index < clients.Count; index++)
                {
                    string message = (index + 1) + "- " + clients.ElementAt(index).name + " Score: " + clients.ElementAt(index).score;
                    scoreboard.Add(message);
                }

                return scoreboard;
            }

            public void addClient(ref Client newClient)
            {
                clients.Add(newClient);
            }

            public int getCount()
            {
                return clients.Count;
            }

            public void readyClientsToQuestion()
            {
                for (int index = 0; index < clients.Count; index++)
                {
                    Client currClient = clients.ElementAt(index);
                    currClient.answered = false;
                }
            }
        };

        Questions questions;
        Clients clients = Clients.initializeClients();
        Clients waitingLobby = Clients.initializeClients();

        List<string> disconnectedPlayers = new List<string>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private async void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (textBox_port.Text != "")
            {
                if (Int32.TryParse(textBox_port.Text, out serverPort))
                {

                    button_listen.Enabled = false;

                    if (isFirst)
                    {
                        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                        serverSocket.Bind(endPoint);
                        serverSocket.Listen(0);
                        isFirst = false;
                    }

                    listening = true;
                    await Task.Run(() =>
                    {
                        while (listening)
                        {
                            try
                            {
                                Socket newClient = serverSocket.Accept();

                                try
                                {
                                    Byte[] buffer = new Byte[256];
                                    newClient.Receive(buffer);

                                    string username = Encoding.Default.GetString(buffer);
                                    username = username.Substring(0, username.IndexOf("\0"));

                                    if (clients.containsName(username) || waitingLobby.containsName(username))
                                    {
                                        string warningUsername = "Please use unique username! Closing the connection";
                                        Byte[] sendBuffer = Encoding.Default.GetBytes(warningUsername);

                                        newClient.Send(sendBuffer);
                                        newClient.Close();
                                    }
                                    else
                                    {
                                        string acceptMessage = "You have connected to server successfully!";
                                        Byte[] acceptBuffer = Encoding.Default.GetBytes(acceptMessage);
                                        newClient.Send(acceptBuffer);

                                        Client addClient = new Client(ref newClient, username);
                                        if (acceptDirectly)
                                        {
                                            addClient.inGame = true;
                                            acceptMessage = "You entered the game!";
                                            clients.addClient(ref addClient);
                                            richTextBox_log.AppendText("User " + username + " is connected as user number " + clients.getCount() + ".\n");
                                            Thread receiveThread = new Thread(() => Receive(ref addClient)); //start receiving from the client
                                            receiveThread.Start();
                                        }
                                        else
                                        {
                                            acceptMessage = "You are waiting for new game!";
                                            richTextBox_log.AppendText(username + " is in waiting queue!\n");
                                            waitingLobby.addClient(ref addClient);
                                            Thread waitingThread = new Thread(() => WaitingThread(ref addClient)); //to understand when someone leaves waiting list
                                            waitingThread.Start();
                                        }

                                        acceptBuffer = Encoding.Default.GetBytes(acceptMessage);
                                        newClient.Send(acceptBuffer);


                                        if (clients.getCount() >= 2)
                                        {
                                            button_start.Enabled = true;
                                            clients.moreThan1Player = true;
                                        }                                                

                                        
                                    }
                                }
                                catch
                                {
                                    richTextBox_log.AppendText("Couldn't get username from new user. Socket will be closed.\n");
                                    newClient.Close();
                                }
                            }
                            catch
                            {
                                if (terminating)
                                {
                                    listening = false;
                                }
                                else
                                {
                                    richTextBox_log.AppendText("User socket stopped working. \n");
                                }
                            }
                        }
                    });                    

                }
                else
                {
                    richTextBox_log.AppendText("Please enter a valid port number!\n");
                }
            }
            else
            {
                richTextBox_log.AppendText("Please fill port number!\n");
            }
        }

        private void Receive(ref Client thisClient)
        {
            bool connected = true;
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    thisClient.socket.Receive(buffer);
                    if (!thisClient.answered && questionAsked)
                    {
                        string message = Encoding.Default.GetString(buffer);
                        double answer = double.Parse(message.Substring(0, message.IndexOf("\0")));
                        thisClient.answerQuestion(answer);
                        clients.totalAnswers++;

                        richTextBox_log.AppendText(thisClient.name + " answered as " + answer + "\n");
                        string ackMessage = "Your answer has been received!";
                        Byte[] sendBuffer = Encoding.Default.GetBytes(ackMessage);
                        thisClient.socket.Send(sendBuffer);
                    }
                }
                catch
                {

                    if (!terminating)
                    {
                        richTextBox_log.AppendText("User " + thisClient.name + " has been disconnected!\n");

                        if (questionAsked && thisClient.answered)
                            clients.totalAnswers--;

                        string message = thisClient.name + " has been disconnected from the game.";
                        disconnectedPlayers.Add(thisClient.name);
                        clients.clients.Remove(thisClient);

                        if (clients.clients.Count == 1)
                        {
                            clients.moreThan1Player = false;
                            button_start.Enabled = false;
                        }

                        connected = false;
                    }
                }
            }
        }

        private void Send(string message)
        {
            if (message != "")
            {
                foreach (Client client in clients.clients)
                {
                    string copyMessage = message;
                    Thread.Sleep(100);
                    try
                    {
                        if (copyMessage.Length > 255) //if message is longer than server can send parse it and then send otherwise directly send
                        {
                            while (copyMessage.Length > 255)
                            {
                                Byte[] buffer = Encoding.Default.GetBytes(copyMessage.Substring(0, 255));
                                client.socket.Send(buffer);
                                copyMessage = copyMessage.Substring(255);
                                Thread.Sleep(200);
                            }
                            if (copyMessage != "")
                            {
                                Byte[] bufferEnd = Encoding.Default.GetBytes(copyMessage);
                                client.socket.Send(bufferEnd);
                            }
                        }
                        else
                        {
                            Byte[] buffer = Encoding.Default.GetBytes(copyMessage);
                            client.socket.Send(buffer);
                        }
                    }
                    catch
                    {
                        richTextBox_log.AppendText("Please chech server connection and start over. Cannot send message to clients.\n");
                        terminating = true;
                        button_listen.Enabled = true;
                        textBox_numq.Enabled = true;
                        textBox_port.Enabled = true;
                        serverSocket.Close();
                    }
                }
            }
        }

        private void WaitingThread(ref Client thisClient)
        {
            bool connected = true;
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    thisClient.socket.Receive(buffer);
                }
                catch
                {

                    if (!terminating)
                    {
                        richTextBox_log.AppendText("User " + thisClient.name + " left waiting queue!\n");
                        waitingLobby.clients.Remove(thisClient);

                        connected = false;
                    }
                }
            }
        }

        private void button_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); //open a screen to chose a file
            ofd.InitialDirectory = @"./";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox_file.Text = Path.GetFullPath(ofd.FileName); //show path to a textbox
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private async void button_start_Click(object sender, EventArgs e)
        {
            int numberOfQuestions;
            acceptDirectly = false;
            disconnectedPlayers.Clear();

            if (textBox_file.Text != "" && textBox_numq.Text != "")
            {
                if (Int32.TryParse(textBox_numq.Text, out numberOfQuestions))
                {
                    button_start.Enabled = false;
                    textBox_numq.Enabled = false;
                    textBox_port.Enabled = false;
                    button_file.Enabled = false;

                    richTextBox_log.AppendText("Number of users: " + clients.getCount() + " Users are:\n");

                    foreach (Client client in clients.clients)
                        richTextBox_log.AppendText("-> " + client.name + "\n");

                    for (int index = 0; index < clients.getCount(); index++)
                        clients.clients.ElementAt(index).score = 0;

                    questions = Questions.initializeQuestions(textBox_file.Text);
                    richTextBox_log.AppendText("Questions are ready!\n");

                    await Task.Run(() =>
                    {
                        for (int qNum = 0; qNum < numberOfQuestions && clients.moreThan1Player; qNum++)
                        {
                            questionAsked = true;
                            clients.totalAnswers = 0;
                            clients.readyClientsToQuestion();
                            richTextBox_log.AppendText("Question Number: " + (qNum + 1) + "\n");

                            Send("Question Number " + (qNum + 1));
                            Send(questions.getQuestion());

                            while (clients.totalAnswers < clients.getCount() && clients.moreThan1Player)
                                Thread.Sleep(300);

                            double closest = double.MaxValue;
                            List<Client> closestClients = new List<Client>();

                            foreach (Client client in clients.clients)
                            {
                                if (!clients.moreThan1Player)
                                    break;

                                if (Math.Abs(client.answer - questions.getAnswer()) < closest)
                                {
                                    closest = Math.Abs(client.answer - questions.getAnswer());
                                    closestClients.Clear();
                                    closestClients.Add(client);
                                }
                                else if (Math.Abs(client.answer - questions.getAnswer()) == closest)
                                    closestClients.Add(client);
                            }

                            double score = (double)1 / closestClients.Count;

                            foreach (Client client in closestClients)
                                client.incrementScore(score);

                            if (clients.moreThan1Player)
                            {
                                Thread.Sleep(100);
                                Send("Correct answer was " + (questions.getAnswer()));

                                string message = "";
                                foreach (Client client in closestClients)
                                    message += client.name + ", ";
                                message = message.Remove(message.Length - 2);
                                message += " got " + score + " points.";

                                richTextBox_log.AppendText(message + "\n");
                                Send(message);

                                Thread.Sleep(100);
                                Send("Score Table");
                            }

                            if (clients.moreThan1Player)
                            {
                                foreach (string line in clients.scoreboardText())
                                {
                                    Send(line);
                                    Thread.Sleep(100);
                                }

                                int order = clients.getCount() + 1;
                                string ghostScore = "";

                                foreach(string name in disconnectedPlayers)
                                {
                                    ghostScore = order + "- " + name + " Score: 0";
                                    Send(ghostScore);
                                    order++;
                                }
                            }
                        }
                        questionAsked = false;
                    });

                    if (clients.moreThan1Player)
                    {
                        clients.sortClients();
                        richTextBox_log.AppendText("Game ended! Restart for a new game.\n");
                        Send("Final Score Board");

                        foreach (string line in clients.scoreboardText())
                        {
                            Send(line);
                            Thread.Sleep(100);
                        }

                        int order = clients.getCount() + 1;
                        string ghostScore = "";

                        foreach (string name in disconnectedPlayers)
                        {
                            ghostScore = order + "- " + name + " Score: 0";
                            Send(ghostScore);
                            order++;
                        }

                        string winners = "";
                        double maxScore = clients.clients.ElementAt(0).score;

                        foreach (Client client in clients.clients)
                        {
                            if (client.score != maxScore)
                                break;
                            winners += client.name + ", ";
                        }
                        winners = winners.Remove(winners.Length - 2);

                        Send(winners + " won the game with score of " + maxScore + ".");
                    }
                    else
                    {
                        Send("All players except you have been disconnected. You won the game!");
                    }

                    textBox_numq.Enabled = true;
                    button_file.Enabled = true;
                }
                else
                {
                    richTextBox_log.AppendText("Invalid number of questions.\n");
                }
            }
            else
            {
                richTextBox_log.AppendText("Please fill file path and number of questions!\n");
            }

            button_start.Enabled = true;

            if (clients.getCount() < 2)
                button_start.Enabled = false;

            if(waitingLobby.getCount() != 0)
            {
                while(waitingLobby.getCount() > 0)
                {
                    Client newClient = waitingLobby.clients.ElementAt(0);
                    clients.addClient(ref newClient);
                    Thread receiveThread = new Thread(() => Receive(ref newClient));
                    receiveThread.Start();
                    waitingLobby.clients.RemoveAt(0);

                    string acceptMessage = "You entered the game!";
                    richTextBox_log.AppendText("User " + newClient.name + " is connected as user number " + clients.getCount() + ".\n");
                    Byte[] acceptBuffer = new Byte[256];

                    acceptBuffer = Encoding.Default.GetBytes(acceptMessage);
                    newClient.socket.Send(acceptBuffer);
                }
            }
            Thread.Sleep(100);
            acceptDirectly = true;
        }
    }
}
