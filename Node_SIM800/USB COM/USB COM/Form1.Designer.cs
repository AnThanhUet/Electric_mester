namespace USB_COM
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btOpen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxPort = new System.Windows.Forms.ComboBox();
            this.tboxConsole = new System.Windows.Forms.TextBox();
            this.tboxSTD1 = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.tboxSTD2 = new System.Windows.Forms.TextBox();
            this.tboxSTD4 = new System.Windows.Forms.TextBox();
            this.tboxSTD3 = new System.Windows.Forms.TextBox();
            this.tboxSTD6 = new System.Windows.Forms.TextBox();
            this.tboxSTD5 = new System.Windows.Forms.TextBox();
            this.tboxSTD8 = new System.Windows.Forms.TextBox();
            this.tboxSTD7 = new System.Windows.Forms.TextBox();
            this.tboxSTD10 = new System.Windows.Forms.TextBox();
            this.tboxSTD9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cboxBaurate = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btOpen
            // 
            this.btOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btOpen.Location = new System.Drawing.Point(242, 49);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(67, 25);
            this.btOpen.TabIndex = 0;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = false;
            this.btOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "PORT";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cboxPort
            // 
            this.cboxPort.FormattingEnabled = true;
            this.cboxPort.Location = new System.Drawing.Point(92, 50);
            this.cboxPort.Name = "cboxPort";
            this.cboxPort.Size = new System.Drawing.Size(108, 24);
            this.cboxPort.TabIndex = 3;
            this.cboxPort.SelectedIndexChanged += new System.EventHandler(this.cboxPort_SelectedIndexChanged);
            // 
            // tboxConsole
            // 
            this.tboxConsole.Location = new System.Drawing.Point(339, 225);
            this.tboxConsole.Multiline = true;
            this.tboxConsole.Name = "tboxConsole";
            this.tboxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tboxConsole.Size = new System.Drawing.Size(289, 213);
            this.tboxConsole.TabIndex = 4;
            this.tboxConsole.WordWrap = false;
            // 
            // tboxSTD1
            // 
            this.tboxSTD1.Location = new System.Drawing.Point(92, 182);
            this.tboxSTD1.Name = "tboxSTD1";
            this.tboxSTD1.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD1.TabIndex = 5;
            // 
            // btnWrite
            // 
            this.btnWrite.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnWrite.BackColor = System.Drawing.Color.Transparent;
            this.btnWrite.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnWrite.Location = new System.Drawing.Point(496, 147);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(77, 45);
            this.btnWrite.TabIndex = 6;
            this.btnWrite.Text = "Write Number";
            this.btnWrite.UseVisualStyleBackColor = false;
            this.btnWrite.Click += new System.EventHandler(this.btWrite_Click);
            // 
            // tboxSTD2
            // 
            this.tboxSTD2.Location = new System.Drawing.Point(92, 208);
            this.tboxSTD2.Name = "tboxSTD2";
            this.tboxSTD2.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD2.TabIndex = 7;
            // 
            // tboxSTD4
            // 
            this.tboxSTD4.Location = new System.Drawing.Point(92, 260);
            this.tboxSTD4.Name = "tboxSTD4";
            this.tboxSTD4.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD4.TabIndex = 9;
            // 
            // tboxSTD3
            // 
            this.tboxSTD3.Location = new System.Drawing.Point(92, 234);
            this.tboxSTD3.Name = "tboxSTD3";
            this.tboxSTD3.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD3.TabIndex = 8;
            // 
            // tboxSTD6
            // 
            this.tboxSTD6.Location = new System.Drawing.Point(92, 312);
            this.tboxSTD6.Name = "tboxSTD6";
            this.tboxSTD6.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD6.TabIndex = 11;
            // 
            // tboxSTD5
            // 
            this.tboxSTD5.Location = new System.Drawing.Point(92, 286);
            this.tboxSTD5.Name = "tboxSTD5";
            this.tboxSTD5.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD5.TabIndex = 10;
            // 
            // tboxSTD8
            // 
            this.tboxSTD8.Location = new System.Drawing.Point(92, 364);
            this.tboxSTD8.Name = "tboxSTD8";
            this.tboxSTD8.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD8.TabIndex = 13;
            // 
            // tboxSTD7
            // 
            this.tboxSTD7.Location = new System.Drawing.Point(92, 338);
            this.tboxSTD7.Name = "tboxSTD7";
            this.tboxSTD7.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD7.TabIndex = 12;
            // 
            // tboxSTD10
            // 
            this.tboxSTD10.Location = new System.Drawing.Point(92, 416);
            this.tboxSTD10.Name = "tboxSTD10";
            this.tboxSTD10.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD10.TabIndex = 15;
            // 
            // tboxSTD9
            // 
            this.tboxSTD9.Location = new System.Drawing.Point(92, 390);
            this.tboxSTD9.Name = "tboxSTD9";
            this.tboxSTD9.Size = new System.Drawing.Size(181, 22);
            this.tboxSTD9.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "SDT1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "SDT2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(384, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(189, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "SDT3";
            this.label7.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 27;
            this.label8.Text = "SDT4";
            this.label8.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 289);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 27;
            this.label9.Text = "SDT5";
            this.label9.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 315);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "SDT6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 341);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "SDT7";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(30, 367);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "SDT8";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 393);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 17);
            this.label13.TabIndex = 31;
            this.label13.Text = "SDT9";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 419);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 17);
            this.label14.TabIndex = 32;
            this.label14.Text = "SDT10";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(125, 157);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "Number Admin";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(336, 205);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "Console";
            // 
            // btnRead
            // 
            this.btnRead.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnRead.BackColor = System.Drawing.Color.Transparent;
            this.btnRead.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRead.Location = new System.Drawing.Point(384, 147);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(77, 45);
            this.btnRead.TabIndex = 35;
            this.btnRead.Text = "Read Number";
            this.btnRead.UseVisualStyleBackColor = false;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // serialPort2
            // 
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(238, 85);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 36;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 88);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 17);
            this.label17.TabIndex = 37;
            this.label17.Text = "BAURATE";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // cboxBaurate
            // 
            this.cboxBaurate.FormattingEnabled = true;
            this.cboxBaurate.Location = new System.Drawing.Point(92, 85);
            this.cboxBaurate.Name = "cboxBaurate";
            this.cboxBaurate.Size = new System.Drawing.Size(108, 24);
            this.cboxBaurate.TabIndex = 38;
            this.cboxBaurate.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            // 
            // Form1
            // 
            this.AccessibleName = "";
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(193)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(668, 733);
            this.Controls.Add(this.cboxBaurate);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tboxSTD10);
            this.Controls.Add(this.tboxSTD9);
            this.Controls.Add(this.tboxSTD8);
            this.Controls.Add(this.tboxSTD7);
            this.Controls.Add(this.tboxSTD6);
            this.Controls.Add(this.tboxSTD5);
            this.Controls.Add(this.tboxSTD4);
            this.Controls.Add(this.tboxSTD3);
            this.Controls.Add(this.tboxSTD2);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.tboxSTD1);
            this.Controls.Add(this.tboxConsole);
            this.Controls.Add(this.cboxPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btOpen);
            this.Name = "Form1";
            this.Text = "Phần mềm cài đặt số điện thoại";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtMessenger;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxPort;
        private System.Windows.Forms.TextBox tboxConsole;
        private System.Windows.Forms.TextBox tboxSTD1;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox tboxSTD2;
        private System.Windows.Forms.TextBox tboxSTD4;
        private System.Windows.Forms.TextBox tboxSTD3;
        private System.Windows.Forms.TextBox tboxSTD6;
        private System.Windows.Forms.TextBox tboxSTD5;
        private System.Windows.Forms.TextBox tboxSTD8;
        private System.Windows.Forms.TextBox tboxSTD7;
        private System.Windows.Forms.TextBox tboxSTD10;
        private System.Windows.Forms.TextBox tboxSTD9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnRead;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboxBaurate;
        public System.Windows.Forms.Timer timer1;
    }
}

