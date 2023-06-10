namespace RS_485
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.portGroup = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openButton = new System.Windows.Forms.Button();
            this.station = new System.Windows.Forms.ComboBox();
            this.baud = new System.Windows.Forms.ComboBox();
            this.port = new System.Windows.Forms.ComboBox();
            this.master = new System.Windows.Forms.GroupBox();
            this.masterCRC = new System.Windows.Forms.CheckBox();
            this.masterTransactionButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.masterStatus = new System.Windows.Forms.TextBox();
            this.masterRecivedHex = new System.Windows.Forms.Button();
            this.masterSendedHex = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.masterRecivedData = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.masterRecivedFrame = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.masterSendedFrame = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.masterFunctionArguments = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.masterT = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.masterTransactionTime = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.masterFunction = new System.Windows.Forms.NumericUpDown();
            this.masterRetransmission = new System.Windows.Forms.NumericUpDown();
            this.masterAddres = new System.Windows.Forms.NumericUpDown();
            this.slave = new System.Windows.Forms.GroupBox();
            this.slaveStartButton = new System.Windows.Forms.Button();
            this.slaveRecivedHex = new System.Windows.Forms.Button();
            this.slaveSendedHex = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.slaveRecivedData = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.slaveRecivedFrame = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.slaveSendedFrame = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.slaveData = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.slaveStatus = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.slaveT = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.slaveAddres = new System.Windows.Forms.NumericUpDown();
            this.portGroup.SuspendLayout();
            this.master.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.masterT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterTransactionTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterRetransmission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterAddres)).BeginInit();
            this.slave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slaveT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slaveAddres)).BeginInit();
            this.SuspendLayout();
            // 
            // portGroup
            // 
            this.portGroup.Controls.Add(this.label3);
            this.portGroup.Controls.Add(this.label2);
            this.portGroup.Controls.Add(this.label1);
            this.portGroup.Controls.Add(this.openButton);
            this.portGroup.Controls.Add(this.station);
            this.portGroup.Controls.Add(this.baud);
            this.portGroup.Controls.Add(this.port);
            this.portGroup.Location = new System.Drawing.Point(325, 12);
            this.portGroup.Name = "portGroup";
            this.portGroup.Size = new System.Drawing.Size(263, 142);
            this.portGroup.TabIndex = 0;
            this.portGroup.TabStop = false;
            this.portGroup.Text = "Port";
            this.portGroup.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Rodzaj stacji";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(92, 113);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 3;
            this.openButton.Text = "Otwórz";
            this.openButton.UseVisualStyleBackColor = true;
            // 
            // station
            // 
            this.station.FormattingEnabled = true;
            this.station.Location = new System.Drawing.Point(65, 86);
            this.station.Name = "station";
            this.station.Size = new System.Drawing.Size(121, 21);
            this.station.TabIndex = 2;
            // 
            // baud
            // 
            this.baud.FormattingEnabled = true;
            this.baud.Location = new System.Drawing.Point(133, 30);
            this.baud.Name = "baud";
            this.baud.Size = new System.Drawing.Size(121, 21);
            this.baud.TabIndex = 1;
            this.baud.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // port
            // 
            this.port.FormattingEnabled = true;
            this.port.Location = new System.Drawing.Point(6, 30);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(121, 21);
            this.port.TabIndex = 0;
            // 
            // master
            // 
            this.master.Controls.Add(this.masterCRC);
            this.master.Controls.Add(this.masterTransactionButton);
            this.master.Controls.Add(this.label13);
            this.master.Controls.Add(this.masterStatus);
            this.master.Controls.Add(this.masterRecivedHex);
            this.master.Controls.Add(this.masterSendedHex);
            this.master.Controls.Add(this.label12);
            this.master.Controls.Add(this.masterRecivedData);
            this.master.Controls.Add(this.label11);
            this.master.Controls.Add(this.masterRecivedFrame);
            this.master.Controls.Add(this.label10);
            this.master.Controls.Add(this.masterSendedFrame);
            this.master.Controls.Add(this.label9);
            this.master.Controls.Add(this.masterFunctionArguments);
            this.master.Controls.Add(this.label8);
            this.master.Controls.Add(this.masterT);
            this.master.Controls.Add(this.label7);
            this.master.Controls.Add(this.masterTransactionTime);
            this.master.Controls.Add(this.label6);
            this.master.Controls.Add(this.label5);
            this.master.Controls.Add(this.label4);
            this.master.Controls.Add(this.masterFunction);
            this.master.Controls.Add(this.masterRetransmission);
            this.master.Controls.Add(this.masterAddres);
            this.master.Location = new System.Drawing.Point(12, 160);
            this.master.Name = "master";
            this.master.Size = new System.Drawing.Size(440, 376);
            this.master.TabIndex = 1;
            this.master.TabStop = false;
            this.master.Text = "Master";
            // 
            // masterCRC
            // 
            this.masterCRC.AutoSize = true;
            this.masterCRC.Location = new System.Drawing.Point(287, 293);
            this.masterCRC.Name = "masterCRC";
            this.masterCRC.Size = new System.Drawing.Size(48, 17);
            this.masterCRC.TabIndex = 25;
            this.masterCRC.Text = "CRC";
            this.masterCRC.UseVisualStyleBackColor = true;
            // 
            // masterTransactionButton
            // 
            this.masterTransactionButton.Location = new System.Drawing.Point(137, 268);
            this.masterTransactionButton.Name = "masterTransactionButton";
            this.masterTransactionButton.Size = new System.Drawing.Size(118, 64);
            this.masterTransactionButton.TabIndex = 24;
            this.masterTransactionButton.Text = "Transakcja";
            this.masterTransactionButton.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(135, 226);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Status";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // masterStatus
            // 
            this.masterStatus.Location = new System.Drawing.Point(138, 242);
            this.masterStatus.Name = "masterStatus";
            this.masterStatus.ReadOnly = true;
            this.masterStatus.Size = new System.Drawing.Size(117, 20);
            this.masterStatus.TabIndex = 22;
            // 
            // masterRecivedHex
            // 
            this.masterRecivedHex.Location = new System.Drawing.Point(134, 142);
            this.masterRecivedHex.Name = "masterRecivedHex";
            this.masterRecivedHex.Size = new System.Drawing.Size(75, 23);
            this.masterRecivedHex.TabIndex = 21;
            this.masterRecivedHex.Text = "Hex";
            this.masterRecivedHex.UseVisualStyleBackColor = true;
            // 
            // masterSendedHex
            // 
            this.masterSendedHex.Location = new System.Drawing.Point(10, 142);
            this.masterSendedHex.Name = "masterSendedHex";
            this.masterSendedHex.Size = new System.Drawing.Size(75, 23);
            this.masterSendedHex.TabIndex = 20;
            this.masterSendedHex.Text = "Hex";
            this.masterSendedHex.UseVisualStyleBackColor = true;
            this.masterSendedHex.Click += new System.EventHandler(this.masterSendedHex_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(257, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Odebrane dane";
            // 
            // masterRecivedData
            // 
            this.masterRecivedData.Location = new System.Drawing.Point(260, 115);
            this.masterRecivedData.Name = "masterRecivedData";
            this.masterRecivedData.ReadOnly = true;
            this.masterRecivedData.Size = new System.Drawing.Size(117, 20);
            this.masterRecivedData.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(135, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Odebrana ramka";
            // 
            // masterRecivedFrame
            // 
            this.masterRecivedFrame.Location = new System.Drawing.Point(134, 115);
            this.masterRecivedFrame.Name = "masterRecivedFrame";
            this.masterRecivedFrame.ReadOnly = true;
            this.masterRecivedFrame.Size = new System.Drawing.Size(117, 20);
            this.masterRecivedFrame.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Wysłana ramka";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // masterSendedFrame
            // 
            this.masterSendedFrame.Location = new System.Drawing.Point(8, 115);
            this.masterSendedFrame.Name = "masterSendedFrame";
            this.masterSendedFrame.ReadOnly = true;
            this.masterSendedFrame.Size = new System.Drawing.Size(117, 20);
            this.masterSendedFrame.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Argumenty funkcji";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // masterFunctionArguments
            // 
            this.masterFunctionArguments.Location = new System.Drawing.Point(260, 72);
            this.masterFunctionArguments.Name = "masterFunctionArguments";
            this.masterFunctionArguments.Size = new System.Drawing.Size(117, 20);
            this.masterFunctionArguments.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(135, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Czas znaku T (ms)";
            // 
            // masterT
            // 
            this.masterT.Location = new System.Drawing.Point(134, 72);
            this.masterT.Name = "masterT";
            this.masterT.Size = new System.Drawing.Size(120, 20);
            this.masterT.TabIndex = 10;
            this.masterT.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Czas transakcji (ms)";
            // 
            // masterTransactionTime
            // 
            this.masterTransactionTime.Location = new System.Drawing.Point(9, 72);
            this.masterTransactionTime.Name = "masterTransactionTime";
            this.masterTransactionTime.Size = new System.Drawing.Size(120, 20);
            this.masterTransactionTime.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(257, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Funkcja";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Retransmisje";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Adres";
            // 
            // masterFunction
            // 
            this.masterFunction.Location = new System.Drawing.Point(260, 32);
            this.masterFunction.Name = "masterFunction";
            this.masterFunction.Size = new System.Drawing.Size(120, 20);
            this.masterFunction.TabIndex = 2;
            // 
            // masterRetransmission
            // 
            this.masterRetransmission.Location = new System.Drawing.Point(134, 32);
            this.masterRetransmission.Name = "masterRetransmission";
            this.masterRetransmission.Size = new System.Drawing.Size(120, 20);
            this.masterRetransmission.TabIndex = 1;
            // 
            // masterAddres
            // 
            this.masterAddres.Location = new System.Drawing.Point(8, 32);
            this.masterAddres.Name = "masterAddres";
            this.masterAddres.Size = new System.Drawing.Size(120, 20);
            this.masterAddres.TabIndex = 0;
            // 
            // slave
            // 
            this.slave.Controls.Add(this.slaveStartButton);
            this.slave.Controls.Add(this.slaveRecivedHex);
            this.slave.Controls.Add(this.slaveSendedHex);
            this.slave.Controls.Add(this.label18);
            this.slave.Controls.Add(this.slaveRecivedData);
            this.slave.Controls.Add(this.label19);
            this.slave.Controls.Add(this.slaveRecivedFrame);
            this.slave.Controls.Add(this.label20);
            this.slave.Controls.Add(this.slaveSendedFrame);
            this.slave.Controls.Add(this.label17);
            this.slave.Controls.Add(this.slaveData);
            this.slave.Controls.Add(this.label16);
            this.slave.Controls.Add(this.slaveStatus);
            this.slave.Controls.Add(this.label15);
            this.slave.Controls.Add(this.slaveT);
            this.slave.Controls.Add(this.label14);
            this.slave.Controls.Add(this.slaveAddres);
            this.slave.Location = new System.Drawing.Point(461, 160);
            this.slave.Name = "slave";
            this.slave.Size = new System.Drawing.Size(443, 376);
            this.slave.TabIndex = 2;
            this.slave.TabStop = false;
            this.slave.Text = "Slave";
            // 
            // slaveStartButton
            // 
            this.slaveStartButton.Location = new System.Drawing.Point(155, 268);
            this.slaveStartButton.Name = "slaveStartButton";
            this.slaveStartButton.Size = new System.Drawing.Size(118, 64);
            this.slaveStartButton.TabIndex = 36;
            this.slaveStartButton.Text = "Uruchom";
            this.slaveStartButton.UseVisualStyleBackColor = true;
            // 
            // slaveRecivedHex
            // 
            this.slaveRecivedHex.Location = new System.Drawing.Point(133, 99);
            this.slaveRecivedHex.Name = "slaveRecivedHex";
            this.slaveRecivedHex.Size = new System.Drawing.Size(75, 23);
            this.slaveRecivedHex.TabIndex = 35;
            this.slaveRecivedHex.Text = "Hex";
            this.slaveRecivedHex.UseVisualStyleBackColor = true;
            // 
            // slaveSendedHex
            // 
            this.slaveSendedHex.Location = new System.Drawing.Point(9, 99);
            this.slaveSendedHex.Name = "slaveSendedHex";
            this.slaveSendedHex.Size = new System.Drawing.Size(75, 23);
            this.slaveSendedHex.TabIndex = 34;
            this.slaveSendedHex.Text = "Hex";
            this.slaveSendedHex.UseVisualStyleBackColor = true;
            this.slaveSendedHex.Click += new System.EventHandler(this.button6_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(256, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 33;
            this.label18.Text = "Odebrane dane";
            // 
            // slaveRecivedData
            // 
            this.slaveRecivedData.Location = new System.Drawing.Point(259, 72);
            this.slaveRecivedData.Name = "slaveRecivedData";
            this.slaveRecivedData.ReadOnly = true;
            this.slaveRecivedData.Size = new System.Drawing.Size(117, 20);
            this.slaveRecivedData.TabIndex = 32;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(134, 56);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 13);
            this.label19.TabIndex = 31;
            this.label19.Text = "Odebrana ramka";
            // 
            // slaveRecivedFrame
            // 
            this.slaveRecivedFrame.Location = new System.Drawing.Point(133, 72);
            this.slaveRecivedFrame.Name = "slaveRecivedFrame";
            this.slaveRecivedFrame.ReadOnly = true;
            this.slaveRecivedFrame.Size = new System.Drawing.Size(117, 20);
            this.slaveRecivedFrame.TabIndex = 30;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 56);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 29;
            this.label20.Text = "Wysłana ramka";
            // 
            // slaveSendedFrame
            // 
            this.slaveSendedFrame.Location = new System.Drawing.Point(7, 72);
            this.slaveSendedFrame.Name = "slaveSendedFrame";
            this.slaveSendedFrame.ReadOnly = true;
            this.slaveSendedFrame.Size = new System.Drawing.Size(117, 20);
            this.slaveSendedFrame.TabIndex = 28;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(259, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "Dane do wysyłki";
            // 
            // slaveData
            // 
            this.slaveData.Location = new System.Drawing.Point(259, 32);
            this.slaveData.Name = "slaveData";
            this.slaveData.Size = new System.Drawing.Size(117, 20);
            this.slaveData.TabIndex = 26;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(153, 226);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "Status";
            // 
            // slaveStatus
            // 
            this.slaveStatus.Location = new System.Drawing.Point(156, 242);
            this.slaveStatus.Name = "slaveStatus";
            this.slaveStatus.ReadOnly = true;
            this.slaveStatus.Size = new System.Drawing.Size(117, 20);
            this.slaveStatus.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(134, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Czas znaku T (ms)";
            // 
            // slaveT
            // 
            this.slaveT.Location = new System.Drawing.Point(133, 32);
            this.slaveT.Name = "slaveT";
            this.slaveT.Size = new System.Drawing.Size(120, 20);
            this.slaveT.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Adres";
            // 
            // slaveAddres
            // 
            this.slaveAddres.Location = new System.Drawing.Point(7, 32);
            this.slaveAddres.Name = "slaveAddres";
            this.slaveAddres.Size = new System.Drawing.Size(120, 20);
            this.slaveAddres.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 548);
            this.Controls.Add(this.slave);
            this.Controls.Add(this.master);
            this.Controls.Add(this.portGroup);
            this.Name = "Form1";
            this.Text = "Form1";
            this.portGroup.ResumeLayout(false);
            this.portGroup.PerformLayout();
            this.master.ResumeLayout(false);
            this.master.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.masterT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterTransactionTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterRetransmission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterAddres)).EndInit();
            this.slave.ResumeLayout(false);
            this.slave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slaveT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slaveAddres)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox portGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.ComboBox station;
        private System.Windows.Forms.ComboBox baud;
        private System.Windows.Forms.ComboBox port;
        private System.Windows.Forms.GroupBox master;
        private System.Windows.Forms.GroupBox slave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown masterFunction;
        private System.Windows.Forms.NumericUpDown masterRetransmission;
        private System.Windows.Forms.NumericUpDown masterAddres;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown masterTransactionTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox masterFunctionArguments;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown masterT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox masterSendedFrame;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox masterRecivedData;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox masterRecivedFrame;
        private System.Windows.Forms.Button masterSendedHex;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox masterStatus;
        private System.Windows.Forms.Button masterRecivedHex;
        private System.Windows.Forms.Button masterTransactionButton;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox slaveData;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox slaveStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown slaveT;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown slaveAddres;
        private System.Windows.Forms.Button slaveStartButton;
        private System.Windows.Forms.Button slaveRecivedHex;
        private System.Windows.Forms.Button slaveSendedHex;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox slaveRecivedData;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox slaveRecivedFrame;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox slaveSendedFrame;
        private System.Windows.Forms.CheckBox masterCRC;
    }
}

