using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;

public struct RS232Params
{
    public enum TerminatorType
    {
        None,
        CR,
        LF,
        CRLF,
        Custom
    }

    public static Dictionary<TerminatorType, string> Terminator = new Dictionary<TerminatorType, string>()
    {
        { TerminatorType.None, ""},
        { TerminatorType.CR, "\r"},
        { TerminatorType.LF, "\n"},
        { TerminatorType.CRLF, "\r\n"},
        { TerminatorType.Custom, ""},
    };

    public string Port;
    public int BaudRateInBps;
    public int DataBitsCount;
    public StopBits StopBits;
    public Handshake Handshake;
    public Parity Parity;
}


public class RS232Port
{
    RS232Params _config;
    SerialPort _serialPort = new SerialPort();
    Stopwatch _stopwatch = new Stopwatch(); 
    public bool IsOpen  => _serialPort.IsOpen; 

    public static string[] GetAvailablePorts()
    {
        return SerialPort.GetPortNames();
    }

    public RS232Port(RS232Params config, SerialDataReceivedEventHandler onRecievedData)
    {
        _config = config;
        ConfigSerialPort(_config, onRecievedData);
    }

    public void ConfigSerialPort(RS232Params config, SerialDataReceivedEventHandler onRecievedData)
    {
        _serialPort.PortName = config.Port;
        _serialPort.BaudRate = config.BaudRateInBps;
        _serialPort.DataBits = config.DataBitsCount;
        _serialPort.StopBits = config.StopBits;
        _serialPort.Handshake = config.Handshake;
        _serialPort.Parity = config.Parity;
        _serialPort.DataReceived += onRecievedData;
    }

    public bool OpenPort()
    {
        try
        {
            _serialPort.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }    
    }
    public bool ClosePort()
    {
        try
        {
            _serialPort.Close();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SendData(string data)
    {
        if(IsOpen && !data.Equals(string.Empty) && data is not null)
        {
            try
            {
                _serialPort.Write(data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }          
        }

        return false;
    }

    public bool ReadData(out string output)
    {
        if (IsOpen)
        {
            try
            {
                output = _serialPort.ReadExisting() + Environment.NewLine;
                return true;
            }
            catch (Exception)
            {
                output = string.Empty;
                return false;
            }
        }
        output = string.Empty;
        return false;
    }
}
