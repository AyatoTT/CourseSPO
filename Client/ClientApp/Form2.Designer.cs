namespace ClientApp
{
    partial class Form2
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
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.messagesTextBox = new System.Windows.Forms.TextBox();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.labelNotif = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // messageTextBox
            // 
            this.messageTextBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.messageTextBox.Location = new System.Drawing.Point(85, 433);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.messageTextBox.MaxLength = 512;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(432, 20);
            this.messageTextBox.TabIndex = 0;
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(521, 426);
            this.sendBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(112, 32);
            this.sendBtn.TabIndex = 1;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // messagesTextBox
            // 
            this.messagesTextBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.messagesTextBox.Location = new System.Drawing.Point(85, 24);
            this.messagesTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.messagesTextBox.Multiline = true;
            this.messagesTextBox.Name = "messagesTextBox";
            this.messagesTextBox.Size = new System.Drawing.Size(548, 394);
            this.messagesTextBox.TabIndex = 2;
            this.messagesTextBox.TextChanged += new System.EventHandler(this.messagesTextBox_TextChanged);
            // 
            // listUsers
            // 
            this.listUsers.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.listUsers.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listUsers.FormattingEnabled = true;
            this.listUsers.Location = new System.Drawing.Point(2, 24);
            this.listUsers.Margin = new System.Windows.Forms.Padding(2);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(79, 394);
            this.listUsers.TabIndex = 3;
            this.listUsers.SelectedIndexChanged += new System.EventHandler(this.listUsers_SelectedIndexChanged);
            // 
            // labelNotif
            // 
            this.labelNotif.AutoSize = true;
            this.labelNotif.Location = new System.Drawing.Point(85, 9);
            this.labelNotif.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNotif.Name = "labelNotif";
            this.labelNotif.Size = new System.Drawing.Size(77, 13);
            this.labelNotif.TabIndex = 5;
            this.labelNotif.Text = "Уведомления";
            this.labelNotif.Click += new System.EventHandler(this.labelNotif_Click);
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(23, 440);
            this.loginLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(35, 13);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = "label1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(644, 493);
            this.Controls.Add(this.labelNotif);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.listUsers);
            this.Controls.Add(this.messagesTextBox);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.messageTextBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.TextBox messagesTextBox;
        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Label labelNotif;
        private System.Windows.Forms.Label loginLabel;
    }
}