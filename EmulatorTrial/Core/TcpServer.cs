
// File: TcpServer.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorTrial.Core
{
    internal class TcpServer
    {
        private readonly ProtocolEngine _protocolEngine;
        private TcpListener _listener;

        public TcpServer(ProtocolEngine engine)
        {
            _protocolEngine = engine;
        }

        public async Task StartAsync(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();
            var buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            string response = _protocolEngine.GetResponse(command.Trim());
            byte[] responseBytes = Encoding.ASCII.GetBytes(response);
            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

            client.Close();
        }
    }
}

