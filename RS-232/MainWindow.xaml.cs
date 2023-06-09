﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
        

        public MainWindow()
        {
            InitializeComponent();
            AddPorts();
            PopulateMembers();
            ChangeStateOfInputs(true);

           _port = new RS232Port(config: _config, SerialDataReceivedEvent);

        }

        private void AddPorts()
        {
            PortDropdown.ItemsSource = RS232Port.GetAvailablePorts();
            PortDropdown.SelectedIndex = 0;
        }

        void ShowPortInfo()
        {
            TerminalMsg("==== Open port ====\n");
            TerminalMsg($"Port name: {_config.Port}\n");
            TerminalMsg($"Baud rate: {_config.BaudRateInBps}\n");
            TerminalMsg($"Data bits: {_config.DataBitsCount}\n");
            TerminalMsg($"Stop bits: {_config.StopBits}\n");
            TerminalMsg($"Handshake: {_config.Handshake}\n");
            TerminalMsg($"Parity: {_config.Parity}\n");
            TerminalMsg("===================\n");
        }

        void TerminalMsg(string msg, bool addTimestamp = false)
        {
            if (addTimestamp)
            {
                string timestamp = DateTime.Now.ToString("<HH:mm:ss> ");
                TerminalTextbox.Text += timestamp;
            }
            TerminalTextbox.Text += msg;
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
        }

        #region UI

        void SerialDataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {           
            if(_port.ReadData(out string data))
            {
                TerminalMsg(data);
            }
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            if(CommandLine.Text.Equals(string.Empty))
                return;

            string data = CommandLine.Text;
            if (!_port.SendData(data))
            {
                TerminalMsg("Failed to send message\n", addTimestamp: true);
                return;
            }    
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            CloseButton.Visibility = Visibility.Visible;
            OpenButton.Visibility = Visibility.Hidden;
            ChangeStateOfInputs(enable: false);

            TerminalMsg($"Opening....\n");
            if (!_port.OpenPort())
            {
                TerminalMsg("ERROR OPENING PORT\n");
                return;
            }

            ShowPortInfo();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenButton.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Hidden;
            ChangeStateOfInputs(enable: true);

            TerminalMsg($"Closing....\n");
            if (!_port.ClosePort())
            {
                TerminalMsg("ERROR CLOSING PORT\n");
                return;
            }
            TerminalMsg($"Closed port {_config.Port}\n");
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
        }

        private void BitNumberDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? bitNumber = ((ComboBoxItem)BitNumberDropdown.SelectedItem)?.Content?.ToString();
            if (bitNumber is null)
                return;
            _config.DataBitsCount = int.Parse(bitNumber);
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
                    _config.StopBits = StopBits.None;
                    break;
            }            
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
                    _config.Parity = Parity.None;
                    break;
            }
        }

        private void BaudRateDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? baundRate = ((ComboBoxItem)BaudRateDropdown.SelectedItem)?.Content?.ToString();
            if(baundRate is null)
                return;
            _config.BaudRateInBps = int.Parse(baundRate.Split(" ").ElementAt(0));
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
                    _config.Parity = Parity.None;
                    break;
            }
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
    }
}
