using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public struct UserMessage
    {
        public string Login { get; set; }
        public string Message { get; set; }
    }

    public partial class Form2 : Form
    {
        Form1 form1Instace = Form1.Instance;
        public List<UserMessage> messages = new List<UserMessage>();
        public string generalChat = "";
        public Form2()
        {
            InitializeComponent();
            listUsers.Items.Add("Флудилка");
            listUsers.SelectedIndex = 0;
            loginLabel.Text = form1Instace.login;
            this.FormClosing += Form2Closing;
            Program.SendMessage("getusers|");
        }

        public event EventHandler Form2Closed;

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string message = messageTextBox.Text;

            if (!string.IsNullOrWhiteSpace(message))
            {
                if (listUsers.SelectedIndex != 0)
                {
                    string selectedUser = listUsers.SelectedItem.ToString();
                    Program.SendMessage("messageto|" + selectedUser + "|" + form1Instace.login + "|" + form1Instace.login + ": " + message);
                    messageTextBox.Text = string.Empty;
                }
                else
                {
                    Program.SendMessage("message|" + form1Instace.login + ": " + message);
                    messageTextBox.Text = string.Empty;
                }
            }
        }

        public void UpdateMessage(string login, string message)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Login == login)
                {
                    var tempUserMessage = messages[i];
                    tempUserMessage.Message = messages[i].Message + (message + Environment.NewLine);
                    messages[i] = tempUserMessage;
                    if (listUsers.InvokeRequired)
                    {
                        listUsers.Invoke((MethodInvoker)delegate
                        {
                            if (listUsers.SelectedIndex != 0)
                            {
                                string selectedUser = listUsers.SelectedItem.ToString();
                                if (login != selectedUser)
                                {
                                    labelNotif.Text = message;
                                }
                            }
                            else
                            {
                                labelNotif.Text = message;
                            }
                        });
                    }
                    else
                    {
                        if (listUsers.SelectedIndex != 0)
                        {
                            string selectedUser = listUsers.SelectedItem.ToString();
                            if (login != selectedUser)
                            {
                                labelNotif.Text = message;
                            }
                        }
                        else
                        {
                            labelNotif.Text = message;
                        }
                    }
                    break;
                }
            }
        }

        public void UpdateMessages()
        {
            if (listUsers.InvokeRequired)
            {
                listUsers.Invoke((MethodInvoker)delegate
                {
                    if (listUsers.SelectedIndex != 0)
                    {
                        string selectedUser = listUsers.SelectedItem.ToString();
                        foreach (UserMessage message in messages)
                        {
                            if (message.Login == selectedUser)
                            {
                                DisplayMessage(message.Message);
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage(generalChat);
                    }
                });
            }
            else
            {
                if (listUsers.SelectedIndex != 0)
                {
                    string selectedUser = listUsers.SelectedItem.ToString();
                    foreach (UserMessage message in messages)
                    {
                        if (message.Login == selectedUser)
                        {
                            DisplayMessage(message.Message);
                        }
                    }
                }
                else
                {
                    DisplayMessage(generalChat);
                }
            }
        }

        public void DisplayMessage(string message)
        {
            if(messagesTextBox.InvokeRequired)
            {
                messagesTextBox.Invoke((MethodInvoker)delegate
                {
                    messagesTextBox.Text = message;
                });
            }
            else
            {
                messagesTextBox.Text = message;
            }
        }

        public void DisplayUser(string login)
        {
            if (listUsers.InvokeRequired)
            {
                listUsers.Invoke((MethodInvoker)delegate
                {
                    listUsers.Items.Add(login);
                });
            }
            else
            {
                listUsers.Items.Add(login);
            }
        }

        public void RemoveUser(string login)
        {
            if (listUsers.InvokeRequired)
            {
                listUsers.Invoke((MethodInvoker)delegate
                {
                    string selectedUser = listUsers.SelectedItem.ToString();

                    if (selectedUser == login)
                    {
                        listUsers.SelectedIndex = 0;
                    }
                    listUsers.Items.Remove(login);
                });
            }
            else
            {
                string selectedUser = listUsers.SelectedItem.ToString();

                if (selectedUser == login)
                {
                    listUsers.SelectedIndex = 0;
                }
                listUsers.Items.Remove(login);
            }
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Login == login)
                {
                    messages.RemoveAt(i);
                    break;
                }
            }
        }

        private void listUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMessages();
        }

        private void Form2Closing(object sender, FormClosingEventArgs e)
        {
            Form2Closed?.Invoke(this, EventArgs.Empty);
        }

        private void labelNotif_Click(object sender, EventArgs e)
        {

        }

        private void messagesTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
