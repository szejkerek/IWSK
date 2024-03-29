﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RS_232
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RS232Params _config;

        private RS232Port _port;

        private enum TerminatorType
        {
            None,
            CR,
            LF,
            CRLF,
            Custom
        }
        private static Dictionary<TerminatorType, string> Terminator = new Dictionary<TerminatorType, string>()
        {
            { TerminatorType.None, ""},
            { TerminatorType.CR, "\r"},
            { TerminatorType.LF, "\n"},
            { TerminatorType.CRLF, "\r\n"},
            { TerminatorType.Custom, ""},
        };
        private TerminatorType _terminator;

        public delegate void NewMessageEventHandle(string message);
        public event NewMessageEventHandle NewMessageEvent;
        private SynchronizationContext synchronizationContext = SynchronizationContext.Current;
        private Stopwatch _cmdStopwatch = new Stopwatch();

        public MainWindow()
        {
            InitDefaultConfig();
            _port = new RS232Port(config: _config, SerialDataReceivedEvent);
            NewMessageEvent += OnMessageReceived;
            
            InitializeComponent();
            AddPorts();
            PopulateMembers();
            ChangeStateOfInputs(true);
        }

        private void InitDefaultConfig()
        {
            _config.Port = RS232Port.GetAvailablePorts().ElementAt(0);
            _config.DataBitsCount = 7;
            _config.StopBits = StopBits.One;
            _config.Parity = Parity.Odd;
            _config.BaudRateInBps = 9600;
            _config.Handshake = Handshake.XOnXOff;
        }

        private void AddPorts()
        {
            PortDropdown.ItemsSource = RS232Port.GetAvailablePorts();
            PortDropdown.SelectedIndex = 0;
        }

        void ShowPortInfo()
        {
            TerminalMsg("==== Open port ====");
            TerminalMsg($"Port name: {_config.Port}");
            TerminalMsg($"Baud rate: {_config.BaudRateInBps}bits/s");
            TerminalMsg($"Data bits: {_config.DataBitsCount}");
            TerminalMsg($"Stop bits: {_config.StopBits}");
            TerminalMsg($"Handshake: {_config.Handshake}");
            TerminalMsg($"Parity: {_config.Parity}");
            TerminalMsg("===================");
        }

        void TerminalMsg(string msg, bool addTimestamp = false)
        {
            if (addTimestamp)
            {
                string timestamp = DateTime.Now.ToString("<HH:mm:ss> ");
                TerminalTextbox.Text += timestamp;
            }

            TerminalTextbox.Text += msg;

            if(msg.ElementAt(msg.Length - 1) != '\n')
            {
                TerminalTextbox.Text += "\n";
            }
        }
        void ChangeStateOfInputs(bool enable)
        {
            PortDropdown.IsEnabled = enable;
            BitNumberDropdown.IsEnabled = enable;
            StopBitDropdown.IsEnabled = enable;
            ParityDropdown.IsEnabled = enable;
            BaudRateDropdown.IsEnabled = enable;
            FlowControlDropdown.IsEnabled = enable;
            SendCommandButton.IsEnabled = !enable;
            CommandLine.IsEnabled = !enable;
            ScanPortButton.IsEnabled = enable;
        }

        void UpdatePort()
        {
           _port.ConfigSerialPort(config: _config);
        }

        #region UI

        void SerialDataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {           
            if(_port.ReadData(out string data))
            {
                synchronizationContext.Post(_ => NewMessageEvent?.Invoke(data), null);
            }
        }

        private void OnMessageReceived(string message)
        {
            TerminalMsg($"Received message: {message}", addTimestamp: true);

            if (message == $"PONG{Terminator[_terminator]}")
            {
                _cmdStopwatch.Stop();
                TerminalMsg($"PONG received after {_cmdStopwatch.ElapsedMilliseconds}ms");

                return;
            }

            if(message == $"PING{Terminator[_terminator]}")
            {
                _port.SendData($"PONG{Terminator[_terminator]}");
            }
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            if(CommandLine.Text.Equals(string.Empty))
                return;

            string data = CommandLine.Text;
            if (!_port.SendData(data + Terminator[_terminator]))
            {
                TerminalMsg("Failed to send message", addTimestamp: true);
                return;
            }

            if(data.Equals("PING"))
            {
                _cmdStopwatch.Restart();
            }

            TerminalMsg($"Message sent: {data + Terminator[_terminator]}", addTimestamp: true);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            TerminalMsg($"Opening....");
            if (!_port.OpenPort())
            {
                TerminalMsg("ERROR OPENING PORT");
                return;
            }

            CloseButton.Visibility = Visibility.Visible;
            OpenButton.Visibility = Visibility.Hidden;
            ChangeStateOfInputs(enable: false);
            ShowPortInfo();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            TerminalMsg($"Closing....");
            if (!_port.ClosePort())
            {
                TerminalMsg("ERROR CLOSING PORT");
                return;
            }

            OpenButton.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Hidden;
            ChangeStateOfInputs(enable: true);
            TerminalMsg($"Closed port {_config.Port}");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TerminalTextbox.Clear();
        }

        private void PortDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? port = PortDropdown.SelectedItem.ToString();
            if (port is null)
                return;
            _config.Port = port;
            UpdatePort();
        }

        private void BitNumberDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? bitNumber = ((ComboBoxItem)BitNumberDropdown.SelectedItem)?.Content?.ToString();
            if (bitNumber is null)
                return;
            _config.DataBitsCount = int.Parse(bitNumber);
            UpdatePort();
        }

        private void StopBitDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedStopBits = ((ComboBoxItem)StopBitDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedStopBits) 
            { 
                case "1":
                    _config.StopBits = StopBits.One;
                    break;
                    
                case "2":
                    _config.StopBits = StopBits.Two;
                    break;
                    
                case "1.5":
                    _config.StopBits = StopBits.OnePointFive;
                    break;

                default:
                    _config.StopBits = StopBits.One;
                    break;
            }
            UpdatePort();
        }

        private void ParityDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedParity = ((ComboBoxItem)ParityDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedParity)
            {
                case "None":
                    _config.Parity = Parity.None;
                    break;

                case "Odd":
                    _config.Parity = Parity.Odd;
                    break;

                case "Even":
                    _config.Parity = Parity.Even;
                    break;

                default:
                    _config.Parity = Parity.Odd;
                    break;
            }
            UpdatePort();
        }

        private void BaudRateDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? baundRate = ((ComboBoxItem)BaudRateDropdown.SelectedItem)?.Content?.ToString();
            if(baundRate is null)
                return;
            _config.BaudRateInBps = int.Parse(baundRate.Split(" ").ElementAt(0));
            UpdatePort();
        }

        private void FlowControlDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedFlow = ((ComboBoxItem)FlowControlDropdown.SelectedItem)?.Content?.ToString();
            switch (selectedFlow)
            {
                case "None":
                    _config.Handshake = Handshake.None;
                    break;

                case "XOnXOff":
                    _config.Handshake = Handshake.XOnXOff;
                    break;

                case "RequestToSend":
                    _config.Handshake = Handshake.RequestToSend;
                    break;

                case "RequestToSendXOnXOff":
                    _config.Handshake = Handshake.RequestToSendXOnXOff;
                    break;

                default:
                    _config.Handshake = Handshake.XOnXOff;
                    break;
            }
            UpdatePort();
        }

        private void TerminatorDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedTerminator = ((ComboBoxItem)TerminatorDropdown.SelectedItem)?.Content?.ToString();
            if(selectedTerminator is null) return;
            switch (selectedTerminator)
            {
                case "None":
                    _terminator = TerminatorType.None;
                    break;

                case "CR":
                    _terminator= TerminatorType.CR;
                    break;

                case "LF":
                    _terminator = TerminatorType.LF;
                    break;

                case "CRLF":
                    _terminator = TerminatorType.CRLF;
                    break;

                case "Custom":
                    _terminator = TerminatorType.Custom;
                    TerminatorTextblock.Visibility = Visibility.Visible;
                    TerminatorTextbox.Visibility = Visibility.Visible;
                    return;
            }
            TerminatorTextblock.Visibility = Visibility.Hidden;
            TerminatorTextbox.Visibility = Visibility.Hidden;
        }

        private void TerminatorTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TerminatorTextbox.Text.Length > 2)
            {
                int caretIndex = TerminatorTextbox.CaretIndex;
                TerminatorTextbox.Text = TerminatorTextbox.Text.Substring(0, 2);
                TerminatorTextbox.CaretIndex = caretIndex > 2 ? 2 : caretIndex;
            }
            Terminator[TerminatorType.Custom] = TerminatorTextbox.Text;
        }



        private void PopulateMembers()
        {
            PortDropdown_SelectionChanged(null, null);
            BitNumberDropdown_SelectionChanged(null, null);
            StopBitDropdown_SelectionChanged(null, null);
            ParityDropdown_SelectionChanged(null, null);
            TerminatorDropdown_SelectionChanged(null, null);
            BaudRateDropdown_SelectionChanged(null, null);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendCommandButton_Click(null, null);
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush terminalImageBrush = null;

            // Find the TerminalImage element within the TextBox's visual tree
            Border border = (Border)TerminalTextbox.Template.FindName("border", TerminalTextbox);
            if (border != null)
            {
                terminalImageBrush = (ImageBrush)border.Background;
            }

            // Now you can use the terminalImageBrush for further operations
            if (terminalImageBrush != null)
            {
                // Example: Set the image source of the ImageBrush
                terminalImageBrush.ImageSource = new BitmapImage(new Uri("../../../Resources/juda.jpg", UriKind.RelativeOrAbsolute));
            }
        }


        private void ScanPortButton_Click(object sender, RoutedEventArgs e)
        {
            AddPorts();
        }

        private void PART_ContentHost_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.ExtentHeightChange != 0)
            {
                scrollViewer.ScrollToEnd();
            }
        }
    }
}
