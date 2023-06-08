﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;

public struct RS232Params
{
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
}
