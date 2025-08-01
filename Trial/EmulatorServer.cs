using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Trial
{
    public class EmulatorServer
    {
        private TcpListener _listener;
        private bool _running;
        private readonly Action<string> _logAction;

        public EmulatorServer(int port, Action<string> logAction)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _logAction = logAction;
        }

        public void Start()
        {
            _running = true;
            _listener.Start();
            _logAction?.Invoke($"서버 시작됨 (포트 {_listener.LocalEndpoint})");
            _listener.BeginAcceptTcpClient(HandleClient, null);
        }

        public void Stop()
        {
            _running = false;
            _listener.Stop();
            _logAction?.Invoke("서버 중지됨");
        }

        private void HandleClient(IAsyncResult result)
        {
            if (!_running) return;

            TcpClient client = _listener.EndAcceptTcpClient(result);
            _listener.BeginAcceptTcpClient(HandleClient, null); // 다음 클라이언트 수신 준비

            _ = Task.Run(() =>
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    while (_running && client.Connected)
                    {
                        try
                        {
                            int read = stream.Read(buffer, 0, buffer.Length);
                            if (read == 0) break; // 연결 종료됨

                            string received = ProtocolConstants.Encoding.GetString(buffer, 0, read);
                            _logAction?.Invoke("[RECV] " + received);
                            string response = ProtocolParser.Process(received);

                            if (!string.IsNullOrEmpty(response))
                            {
                                byte[] sendBytes = ProtocolConstants.Encoding.GetBytes(response);
                                stream.Write(sendBytes, 0, sendBytes.Length);
                                _logAction?.Invoke("[SEND] " + response);
                            }
                        }
                        //catch (IOException) { break; } // 클라이언트 종료 시 예외
                        catch (Exception ex) 
                        {
                            break; 
                        }
                    }
                }
            });
        }


        //private void HandleClient(IAsyncResult result)
        //{
        //    if (!_running) return;

        //    TcpClient client = null;

        //    try
        //    {
        //        client = _listener.EndAcceptTcpClient(result);
        //        _logAction?.Invoke($"클라이언트 연결됨: {client.Client.RemoteEndPoint}");
        //        _listener.BeginAcceptTcpClient(HandleClient, null); // 다음 클라이언트 수신

        //        using (NetworkStream stream = client.GetStream())
        //        {
        //            byte[] buffer = new byte[1024];
        //            int read = stream.Read(buffer, 0, buffer.Length);

        //            string received = ProtocolConstants.Encoding.GetString(buffer, 0, read);
        //            _logAction?.Invoke("[RECV] " + received);

        //            string response = ProtocolParser.Process(received);
        //            if (!string.IsNullOrEmpty(response))
        //            {
        //                byte[] sendBytes = ProtocolConstants.Encoding.GetBytes(response);
        //                stream.Write(sendBytes, 0, sendBytes.Length);
        //                _logAction?.Invoke("[SEND] " + response);
        //            }
        //        }

        //        _logAction?.Invoke("클라이언트 연결 종료");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logAction?.Invoke($"[ERROR] {ex.Message}");
        //    }
        //    finally
        //    {
        //        client?.Close();
        //    }
        //}
    }
}
