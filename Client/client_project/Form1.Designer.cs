namespace client_project
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
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.RichTextBox = new System.Windows.Forms.RichTextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.label_Message = new System.Windows.Forms.Label();
            this.button_Send = new System.Windows.Forms.Button();
            this.label_UserName = new System.Windows.Forms.Label();
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(80, 35);
            this.label_IP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(24, 17);
            this.label_IP.TabIndex = 0;
            this.label_IP.Text = "IP:";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(139, 33);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(68, 22);
            this.textBox_IP.TabIndex = 1;
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(67, 63);
            this.label_Port.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(38, 17);
            this.label_Port.TabIndex = 2;
            this.label_Port.Text = "Port:";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(139, 63);
            this.textBox_Port.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(68, 22);
            this.textBox_Port.TabIndex = 3;
            // 
            // RichTextBox
            // 
            this.RichTextBox.Location = new System.Drawing.Point(314, 35);
            this.RichTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.ReadOnly = true;
            this.RichTextBox.Size = new System.Drawing.Size(337, 281);
            this.RichTextBox.TabIndex = 4;
            this.RichTextBox.Text = "";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(139, 123);
            this.button_Connect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(82, 28);
            this.button_Connect.TabIndex = 5;
            this.button_Connect.Text = "Connect";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // textBox_Message
            // 
            this.textBox_Message.Enabled = false;
            this.textBox_Message.Location = new System.Drawing.Point(139, 292);
            this.textBox_Message.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.Size = new System.Drawing.Size(68, 22);
            this.textBox_Message.TabIndex = 6;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Location = new System.Drawing.Point(34, 294);
            this.label_Message.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(69, 17);
            this.label_Message.TabIndex = 7;
            this.label_Message.Text = "Message:";
            // 
            // button_Send
            // 
            this.button_Send.Enabled = false;
            this.button_Send.Location = new System.Drawing.Point(139, 323);
            this.button_Send.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(82, 28);
            this.button_Send.TabIndex = 8;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(21, 91);
            this.label_UserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(83, 17);
            this.label_UserName.TabIndex = 9;
            this.label_UserName.Text = "User Name:";
            // 
            // textBox_UserName
            // 
            this.textBox_UserName.Location = new System.Drawing.Point(139, 91);
            this.textBox_UserName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_UserName.Name = "textBox_UserName";
            this.textBox_UserName.Size = new System.Drawing.Size(68, 22);
            this.textBox_UserName.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 385);
            this.Controls.Add(this.textBox_UserName);
            this.Controls.Add(this.label_UserName);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.textBox_Message);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.RichTextBox);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label_IP);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.RichTextBox RichTextBox;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.TextBox textBox_UserName;
    }
}

