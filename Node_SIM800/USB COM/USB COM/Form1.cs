using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USB_COM
{
    public partial class Form1 : Form
    {
        string textTemp = "";
        bool ReceiveDone = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            cboPort.SelectedIndex = 0;
            btnClose.Enabled = false;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try
            {
                serialPort2.PortName = cboPort.Text;
                serialPort2.Open();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {            
            try
            {
                if (serialPort2.IsOpen)
                {
                    serialPort2.Write(txtMessenger.Text + Environment.NewLine);
                    txtMessenger.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try
            {                
                serialPort2.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort2.IsOpen)
                {
                    txtReceive.Text = serialPort2.ReadExisting();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                serialPort2.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(btOpen.Text == "Open")
            {

                try
                {
                    serialPort2.PortName = cboxPort.Text;
                    serialPort2.BaudRate = Int32.Parse(cboxBaurate.Text);
                    serialPort2.ReadTimeout = 1000;
                    serialPort2.Open();
                    //Exception here
                    btOpen.Text = "Close";
                    btOpen.BackColor = Color.FromArgb(174, 255, 179);     //green
                    btnWrite.Enabled = true;
                    btnRead.Enabled = true;
                    btnRefresh.Enabled = false;

                    tboxConsole.Enabled = true;
                    tboxSTD1.Enabled = true;
                    tboxSTD2.Enabled = true;
                    tboxSTD3.Enabled = true;
                    tboxSTD4.Enabled = true;
                    tboxSTD5.Enabled = true;
                    tboxSTD6.Enabled = true;
                    tboxSTD7.Enabled = true;
                    tboxSTD8.Enabled = true;
                    tboxSTD9.Enabled = true;
                    tboxSTD10.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }                


            } else
            {
                btOpen.Text = "Open";
                btOpen.BackColor = Color.FromArgb(248, 179, 171);      //red
                btnWrite.Enabled = false;
                btnRead.Enabled = false;
                btnRefresh.Enabled = true;

                tboxConsole.Enabled = false;
                tboxSTD1.Enabled = false;
                tboxSTD2.Enabled = false;
                tboxSTD3.Enabled = false;
                tboxSTD4.Enabled = false;
                tboxSTD5.Enabled = false;
                tboxSTD6.Enabled = false;
                tboxSTD7.Enabled = false;
                tboxSTD8.Enabled = false;
                tboxSTD9.Enabled = false;
                tboxSTD10.Enabled = false;

                try
                {                    
                    serialPort2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cboxPort.Items.AddRange(ports);
            cboxPort.SelectedIndex = 1;            

            string[] baurate = { "300", "600", "1200", "2400", "4800", "9600", "14400",
                "19200", "28800", "38400", "57600", "115200" };            
            cboxBaurate.Items.AddRange(baurate);
            cboxBaurate.SelectedIndex = 5;

            btOpen.Text = "Open";
            btOpen.BackColor = Color.FromArgb(248, 179, 171);      //red
            btnWrite.Enabled = false;
            btnRead.Enabled = false;

            tboxConsole.Enabled = false;
            tboxSTD1.Enabled = false;
            tboxSTD2.Enabled = false;
            tboxSTD3.Enabled = false;
            tboxSTD4.Enabled = false;
            tboxSTD5.Enabled = false;
            tboxSTD6.Enabled = false;
            tboxSTD7.Enabled = false;
            tboxSTD8.Enabled = false;
            tboxSTD9.Enabled = false;
            tboxSTD10.Enabled = false;


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void btWrite_Click(object sender, EventArgs e)
        {
            
        }

        private void cboxPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cboxPort.Items.Clear();
            cboxPort.Items.AddRange(ports);
            cboxPort.SelectedIndex = 0;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                serialPort2.WriteLine("see");
                txt = "";
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                serialPort2.Close(); 
            }
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                tboxConsole.Text = serialPort2.ReadExisting();
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.tboxConsole.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {                                
                textTemp = text;
                this.tboxConsole.Text = textTemp;

                ////////////////////////////////////////////
                Console.WriteLine("---->tboxSTD1");
                getNumber(ref textTemp, tboxSTD1);
                Console.WriteLine("<----tboxSTD1");

                Console.WriteLine("---->tboxSTD2");
                getNumber(ref textTemp, tboxSTD2);
                Console.WriteLine("<----tboxSTD2");
                /*                getNumber(ref textTemp, tboxSTD3);
                                getNumber(ref textTemp, tboxSTD4);
                                getNumber(ref textTemp, tboxSTD5);
                                getNumber(ref textTemp, tboxSTD6);
                                getNumber(ref textTemp, tboxSTD7);
                                getNumber(ref textTemp, tboxSTD8);
                                getNumber(ref textTemp, tboxSTD9);
                                getNumber(ref textTemp, tboxSTD10);*/
            }
        }
        private string txt;
        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string rec = serialPort2.ReadTo("9: \r\n");
            Console.WriteLine("value: {0}", rec);
            SetText(rec);

        }

        private void getNumber(ref string textTemp, TextBox tbox)
        {
            int pStart = textTemp.IndexOf(": ");
            int pEnd = textTemp.IndexOf("\r\n");
            Console.WriteLine("---->pStart is : {0}", pStart.ToString());
            Console.WriteLine("---->pEnd is : {0}", pEnd.ToString());
            if ((pEnd != -1) && (pStart != -1))
            {
                Console.WriteLine("---->textTemp before cut is : {0}", textTemp);
                string number = textTemp.Substring(pStart+2, pEnd - (pStart+1));
                int len = textTemp.Length;
                tbox.Text = number;
                textTemp = textTemp.Substring(pEnd, len - pEnd);
                Console.WriteLine("---->textTemp after cut is : {0}", textTemp);                Console.WriteLine("<----number is : {0}", number);
            }
        }
    }
}
