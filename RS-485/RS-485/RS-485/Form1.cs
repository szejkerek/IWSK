using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace RS_485
{
    public partial class Form1 : Form
    {
        // Serial port configuration
        private const string SerialPortName = "COM1"; // Update with your serial port name
        private const int BaudRate = 9600; // Update with your baud rate
        private const Parity Parity = Parity.None;
        private const int DataBits = 8;
        private const StopBits StopBits = StopBits.One;
        private SerialPort serialPort;


        public Form1()
        {
            InitializeComponent();
        }
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort(SerialPortName, BaudRate, Parity, DataBits, StopBits);
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.Open();
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Handle received data from the serial port (Slave station)
            // Implement your logic for processing Modbus frames and generating responses
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
