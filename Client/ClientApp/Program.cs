using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    internal static class Program
    {
        public static TcpClient tcpClient;
        public static NetworkStream networkStream;
        public static Thread receiveThread;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            tcpClient = new TcpClient("127.0.0.1", 5400);
            networkStream = tcpClient.GetStream();

            receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start(networkStream);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void ReceiveMessages(object obj)
        {
            networkStream = (NetworkStream)obj;
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                if (receivedMessage.StartsWith("message|"))
                {
                    string message = receivedMessage.Substring(8);
                    Form1 form1Instace = Form1.Instance;
                    if (form1Instace != null)
                    {
                        form1Instace.form2.generalChat = form1Instace.form2.generalChat + (message + Environment.NewLine);
                        form1Instace.form2.UpdateMessages();
                    }
                }
                else if (receivedMessage.StartsWith("messageto|"))
                {
                    int firstPipeIndex = receivedMessage.IndexOf('|');
                    int secondPipeIndex = receivedMessage.IndexOf('|', firstPipeIndex + 1);
                    int thirdPipeIndex = receivedMessage.IndexOf('|', secondPipeIndex + 1);
                    string login = receivedMessage.Substring(secondPipeIndex + 1, thirdPipeIndex - secondPipeIndex - 1);
                    string message = receivedMessage.Substring(thirdPipeIndex + 1);
                    Form1 form1Instace = Form1.Instance;
                    if (form1Instace != null)
                    {
                        form1Instace.form2.UpdateMessage(login, message);
                        form1Instace.form2.UpdateMessages();
                    }
                }
                else if (receivedMessage.StartsWith("messagefr|"))
                {
                    int firstPipeIndex = receivedMessage.IndexOf('|');
                    int secondPipeIndex = receivedMessage.IndexOf('|', firstPipeIndex + 1);
                    int thirdPipeIndex = receivedMessage.IndexOf('|', secondPipeIndex + 1);
                    string login = receivedMessage.Substring(firstPipeIndex + 1, secondPipeIndex - firstPipeIndex - 1);
                    string message = receivedMessage.Substring(thirdPipeIndex + 1);
                    Form1 form1Instace = Form1.Instance;
                    if (form1Instace != null)
                    {
                        form1Instace.form2.UpdateMessage(login, message);
                        form1Instace.form2.UpdateMessages();
                    }
                }
                else if (receivedMessage.StartsWith("getusers|"))
                {
                    string login = receivedMessage.Substring(9);
                    Form1 form1Instace = Form1.Instance;
                    if (form1Instace != null)
                    {
                        form1Instace.form2.DisplayUser(login);
                        form1Instace.form2.messages.Add(new UserMessage { Login = login, Message = "" });
                    }
                }
                else if (receivedMessage.StartsWith("disconnect|"))
                {
                    string login = receivedMessage.Substring(11);
                    Form1 form1Instace = Form1.Instance;
                    if (form1Instace != null)
                    {
                        form1Instace.form2.RemoveUser(login);
                    }
                }
                else if (receivedMessage.StartsWith("login_error|"))
                {
                    MessageBox.Show($"Login already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (receivedMessage.StartsWith("login_success|"))
                {
                    Form1 form1Instace = Form1.Instance;
                    if (form1Instace != null)
                    {
                        form1Instace.createForm2();
                    }
                }
            }
        }

        public static void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                networkStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
