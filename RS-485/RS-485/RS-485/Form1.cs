using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;

namespace RS_485
{
    public partial class Form1 : Form
    {
        // Serial port configuration
        SerialInterfaceServer SerialService = new SerialInterfaceServer();
        Master modbusMaster = new Master();
        Slave modbusSlave = new Slave();
        public Form1()
        {
            InitializeComponent();
            InitializeModbusSystem();
        }
        private void InitializeModbusSystem()
        {
            baud.SelectedIndex = 0;
            port.Items.Clear();
            port.Items.AddRange(modbusMaster.GetAvaliablePorts());
            modbusSlave.CheckSumErrorHandler += ModbusSlave_CheckSumErrorHandler;
            modbusSlave.FirstCommandExecutionHandler += ModbusSlave_FirstCommandExecutionHandler;
            modbusSlave.ReceivedFrameHandler += ModbusSlave_ReceivedFrameHandler;
            modbusSlave.SendFrameHandler += ModbusSlave_SendFrameHandler;
            modbusSlave.StatusHandler += ModbusSlave_StatusHandler;

            modbusMaster.StatusHandler += ModbusMaster_StatusHandler;
            modbusMaster.FunctionTwoHandler += ModbusMaster_FunctionTwoHandler;
            modbusMaster.CheckSumErrorHandler += ModbusSlave_CheckSumErrorHandler;
            modbusMaster.ReceivedFrameHandler += ModbusMaster_ReceivedFrameHandler;
            modbusMaster.SendFrameHandler += ModbusMaster_SendFrameHandler;
        }


        private void portCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (portCheckBox.Checked)
            {
                portCheckBox.Text = "Zamknij";
                SerialService.Close();
                string serialPortName = port.Text;
                int baudRate = int.Parse(baud.Text);
                if (station.Text.Contains("Master"))
                {
                    modbusMaster.Open(serialPortName, baudRate);
                    if (!modbusMaster.IsOpen())
                    {
                        portCheckBox.Checked = !portCheckBox.Checked;
                        portCheckBox.Text = "Otwórz";
                        return;
                    }
                    master.Enabled = true;
                    slave.Enabled = false;
                }
                else
                {
                    modbusSlave.Open(serialPortName, baudRate);
                    if (!modbusSlave.IsOpen())
                    {
                        portCheckBox.Checked = !portCheckBox.Checked;
                        portCheckBox.Text = "Otwórz";
                        return;
                    }
                    master.Enabled = false;
                    slave.Enabled = true;
                }
            }
            else
            {
                modbusMaster.Close();
                modbusSlave.Close();
                master.Enabled = false;
                slave.Enabled = false;
                portCheckBox.Text = "Otwórz";
            }
        }
        //Master
        private void masterSendedHex_Click(object sender, EventArgs e)
        {
            string temp = masterSendedFrame.Text;
            temp = temp.Replace("\\n", "\n").Replace("\\r", "\r");
            string hexDump = "";
            foreach (char c in temp)
            {
                hexDump += "0x" + Convert.ToByte(c).ToString("x2") + " ";
            }
            MessageBox.Show(hexDump, "Konwersja do hex");
        }

        private void masterRecivedHex_Click(object sender, EventArgs e)
        {
            string temp = masterRecivedFrame.Text;
            temp = temp.Replace("\\n", "\n").Replace("\\r", "\r");
            string hexDump = "";
            foreach (char c in temp)
            {
                hexDump += "0x" + Convert.ToByte(c).ToString("x2") + " ";
            }
            MessageBox.Show(hexDump, "Konwersja do hex");
        }

        private void masterCRC_CheckedChanged(object sender, EventArgs e)
        {
            modbusMaster.BadCrcChecksum = !masterCRC.Checked;
        }
        private void masterTransactionButton_Click(object sender, EventArgs e)
        {
            int slaveAddress = decimal.ToInt32(masterAddres.Value);
            int functionCode = decimal.ToInt32(masterFunction.Value);
            string functionArguments = masterFunctionArguments.Text;
            int chars = decimal.ToInt32(masterT.Value); //T (single char) timeout
            int retransmissionsCount = decimal.ToInt32(masterRetransmission.Value);
            int transactionTimeout = decimal.ToInt32(masterTransactionTime.Value);

            masterSendedFrame.Text = "";
            masterRecivedFrame.Text = "";
            masterRecivedData.Text = "";
            masterStatus.Text = "";

            modbusMaster.RunTransaction(slaveAddress, functionCode, functionArguments, chars, transactionTimeout, retransmissionsCount);
            Application.DoEvents();
        }
        //Slave
        private void slaveSendedHex_Click(object sender, EventArgs e)
        {
            string temp = slaveSendedFrame.Text;
            temp = temp.Replace("\\n", "\n").Replace("\\r", "\r");
            string hexDump = "";
            foreach (char c in temp)
            {
                hexDump += "0x" + Convert.ToByte(c).ToString("x2") + " ";
            }
            MessageBox.Show(hexDump, "Konwersja do hex");
        }

        private void slaveRecivedHex_Click(object sender, EventArgs e)
        {
            string temp = slaveRecivedFrame.Text;
            temp = temp.Replace("\\n", "\n").Replace("\\r", "\r");
            string hexDump = "";
            foreach (char c in temp)
            {
                hexDump += "0x" + Convert.ToByte(c).ToString("x2") + " ";
            }
            MessageBox.Show(hexDump, "Konwersja do hex");
        }

        private void slaveCrc_CheckedChanged(object sender, EventArgs e)
        {
            modbusSlave.BadCrcChecksum = !slaveCrc.Checked;
        }
        private void slaveStartButton_CheckedChanged(object sender, EventArgs e)
        {
            if (slaveStartButton.Checked)
            {
                int address = decimal.ToInt32(slaveAddres.Value);
                int characterTimeout = decimal.ToInt32(slaveT.Value);
                modbusSlave.secondCommand = slaveData.Text;
                modbusSlave.Run(address, characterTimeout);
                slaveAddres.Enabled = false;
                slaveT.Enabled = false;
                slaveStartButton.Text = "Wyłącz";
            }
            else
            {
                modbusSlave.Stop();
                slaveAddres.Enabled = true;
                slaveT.Enabled = true;
                slaveStartButton.Text = "Uruchom";
            }
            slaveSendedFrame.Text = "";
            slaveRecivedFrame.Text = "";
            slaveRecivedData.Text = "";
            slaveStatus.Text = "";
        }

        //Events
        private void ModbusSlave_ReceivedFrameHandler(object sender, ModbusMsg e)
        {
            slaveRecivedFrame.Invoke(new Action(() =>
            {
                slaveRecivedFrame.Text = e.message;
            }));
        }
        private void ModbusSlave_FirstCommandExecutionHandler(object sender, ModbusMsg e)
        {
            slaveRecivedFrame.Invoke(new Action(() =>
            {
                slaveRecivedData.Text = e.message;
            }));
        }
        private void ModbusSlave_SendFrameHandler(object sender, ModbusMsg e)
        {
            slaveRecivedFrame.Invoke(new Action(() =>
            {
                slaveSendedFrame.Text = e.message;
            }));
        }
        private void ModbusSlave_CheckSumErrorHandler(object sender, ModbusMsg e)
        {
            MessageBox.Show(e.message, "Bad Checksum");
        }
        private void ModbusSlave_StatusHandler(object sender, ModbusMsg e)
        {
            slaveStatus.Invoke(new Action(() =>
            {
                slaveStatus.Text = e.message;
            }));
        }
        private void ModbusMaster_StatusHandler(object sender, ModbusMsg e)
        {
            masterStatus.Invoke(new Action(() =>
            {
                masterStatus.Text = e.message;
            }));
        }
        private void ModbusMaster_FunctionTwoHandler(object sender, ModbusMsg e)
        {
            masterRecivedData.Invoke(new Action(() =>
            {
                masterRecivedData.Text = e.message;
            }));

        }
        private void ModbusMaster_ReceivedFrameHandler(object sender, ModbusMsg e)
        {
            masterRecivedFrame.Invoke(new Action(() =>
            {
                masterRecivedFrame.Text = e.message;
            }));
        }
        private void ModbusMaster_SendFrameHandler(object sender, ModbusMsg e)
        {
            masterSendedFrame.Invoke(new Action(() =>
            {
                masterSendedFrame.Text = e.message;
            }));
        }
        //created by accident
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
