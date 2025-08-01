
// File: SerialHandler.cs
using System;
using System.IO.Ports;
using System.Text;

namespace EmulatorTrial.Core
{
    internal class SerialHandler
    {
        private readonly SerialPort _port;
        private readonly ProtocolEngine _protocolEngine;

        public SerialHandler(string portName, ProtocolEngine engine)
        {
            _protocolEngine = engine;
            _port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            _port.DataReceived += OnDataReceived;
        }

        public void Open()
        {
            _port.Open();
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string command = _port.ReadLine();
            string response = _protocolEngine.GetResponse(command.Trim());
            _port.WriteLine(response);
        }

        public void Close()
        {
            if (_port.IsOpen)
                _port.Close();
        }
    }
}
