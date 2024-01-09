using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private static Form1 instance;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // Устанавливаем центральное положение формы
            instance = this;
            this.FormClosing += Form1Closing;
        }

        public static Form1 Instance
        {
            get { return instance; }
        }

        public Form2 form2;

        public string login;

        private void loginBtn_Click(object sender, EventArgs e)
        {
            login = loginBox.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Please enter both login.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (login.Contains("|"))
            {
                MessageBox.Show("The login contains a forbidden character '|'.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Program.SendMessage("login|" + login);
        }

        public void createForm2()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                form2 = new Form2();
                form2.Form2Closed += Form2_FormClosed;
                this.Hide();
                form2.Show();
            });
        }

        private void Form1Closing(object sender, FormClosingEventArgs e)
        {
            Program.SendMessage("disconnect|");
            if (Program.receiveThread != null && Program.receiveThread.IsAlive)
            {
                Program.receiveThread.Abort();
            }

            if (Program.networkStream != null)
            {
                Program.networkStream.Close();
            }

            if (Program.tcpClient != null)
            {
                Program.tcpClient.Close();
            }
        }

        private void Form2_FormClosed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
