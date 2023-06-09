using System;
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

           _port = new RS232Port(config: _config, SerialDataReceivedEvent);

        }

        void SerialDataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {           
            if(_port.ReadData(out string data))
            {
                TerminalTextbox.Text += data;
            }
        }

        private void SendCommandButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeStateOfInputs(enable: false);
            if (!_port.OpenPort())
            {
                TerminalTextbox.Text += "ERROR OPENING PORT\n";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeStateOfInputs(enable: true);
            if (!_port.ClosePort())
            {
                TerminalTextbox.Text += "ERROR CLOSING PORT\n";
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
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TerminatorTextbox.Clear();
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
            string? baundRate = ((ComboBoxItem)BitNumberDropdown.SelectedItem)?.Content?.ToString();
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

        private void TextboxTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //komand
            
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

        private void AddPorts()
        {
            PortDropdown.ItemsSource = RS232Port.GetAvailablePorts();
            PortDropdown.SelectedIndex = 0;
        }

        private void PopulateMembers()
        {
            PortDropdown_SelectionChanged(null, null);
            BitNumberDropdown_SelectionChanged(null, null);
            StopBitDropdown_SelectionChanged(null, null);
            ParityDropdown_SelectionChanged(null, null);
            TerminatorDropdown_SelectionChanged(null, null);
        }
    }
}
