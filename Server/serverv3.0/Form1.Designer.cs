namespace serverv3._0
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
            this.label_port = new System.Windows.Forms.Label();
            this.label_num = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_numq = new System.Windows.Forms.TextBox();
            this.button_listen = new System.Windows.Forms.Button();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.label_file = new System.Windows.Forms.Label();
            this.button_file = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(120, 46);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(38, 17);
            this.label_port.TabIndex = 0;
            this.label_port.Text = "Port:";
            // 
            // label_num
            // 
            this.label_num.AutoSize = true;
            this.label_num.Location = new System.Drawing.Point(12, 98);
            this.label_num.Name = "label_num";
            this.label_num.Size = new System.Drawing.Size(146, 17);
            this.label_num.TabIndex = 2;
            this.label_num.Text = "Number of Questions:";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(164, 46);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 22);
            this.textBox_port.TabIndex = 3;
            // 
            // textBox_numq
            // 
            this.textBox_numq.Location = new System.Drawing.Point(164, 93);
            this.textBox_numq.Name = "textBox_numq";
            this.textBox_numq.Size = new System.Drawing.Size(100, 22);
            this.textBox_numq.TabIndex = 5;
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(367, 80);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(75, 26);
            this.button_listen.TabIndex = 6;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.Location = new System.Drawing.Point(15, 213);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.ReadOnly = true;
            this.richTextBox_log.Size = new System.Drawing.Size(486, 221);
            this.richTextBox_log.TabIndex = 7;
            this.richTextBox_log.Text = "";
            // 
            // textBox_file
            // 
            this.textBox_file.Location = new System.Drawing.Point(84, 148);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.ReadOnly = true;
            this.textBox_file.Size = new System.Drawing.Size(209, 22);
            this.textBox_file.TabIndex = 8;
            // 
            // label_file
            // 
            this.label_file.AutoSize = true;
            this.label_file.Location = new System.Drawing.Point(12, 148);
            this.label_file.Name = "label_file";
            this.label_file.Size = new System.Drawing.Size(66, 17);
            this.label_file.TabIndex = 9;
            this.label_file.Text = "File path:";
            // 
            // button_file
            // 
            this.button_file.Location = new System.Drawing.Point(123, 176);
            this.button_file.Name = "button_file";
            this.button_file.Size = new System.Drawing.Size(124, 23);
            this.button_file.TabIndex = 10;
            this.button_file.Text = "Choose File";
            this.button_file.UseVisualStyleBackColor = true;
            this.button_file.Click += new System.EventHandler(this.button_file_Click);
            // 
            // button_start
            // 
            this.button_start.Enabled = false;
            this.button_start.Location = new System.Drawing.Point(367, 141);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 29);
            this.button_start.TabIndex = 11;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 446);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_file);
            this.Controls.Add(this.label_file);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.richTextBox_log);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.textBox_numq);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label_num);
            this.Controls.Add(this.label_port);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Label label_num;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_numq;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.Label label_file;
        private System.Windows.Forms.Button button_file;
        private System.Windows.Forms.Button button_start;
    }
}

