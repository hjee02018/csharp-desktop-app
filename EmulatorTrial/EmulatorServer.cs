using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

/*
 * @@@ 잉크젯 통신 서버 애뮬레이터 동작 정의
 */
namespace EmulatorTrial
{
    public class EmulatorServer
    {
        private TcpListener _listener;
        private bool _running;

        public EmulatorServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            _running = true;
            _listener.Start();
            _listener.BeginAcceptTcpClient(HandleClient, null);
        }

        public void Stop()
        {
            _running = false;
            _listener.Stop();
        }

        private void HandleClient(IAsyncResult result)
        {
            if (!_running) return;

            TcpClient client = _listener.EndAcceptTcpClient(result);
            _listener.BeginAcceptTcpClient(HandleClient, null); // 다음 클라이언트 수신 준비

            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int read = stream.Read(buffer, 0, buffer.Length);

                string received = ProtocolConstants.Encoding.GetString(buffer, 0, read);
                Console.WriteLine("[RECV] " + received);

                string response = ProtocolParser.Process(received);
                if (!string.IsNullOrEmpty(response))
                {
                    byte[] sendBytes = ProtocolConstants.Encoding.GetBytes(response);
                    stream.Write(sendBytes, 0, sendBytes.Length);
                    Console.WriteLine("[SEND] " + response);
                }
            }
        }
    }
}
