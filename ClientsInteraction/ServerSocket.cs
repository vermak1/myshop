using System;
using System.Net.Sockets;
using System.Net;

namespace MyShop.ClientsInteraction
{
    internal class ServerSocket : Socket
    {
        private readonly IPEndPoint _ipEndPoint;
        private const String SERVER_URL = "127.0.0.1";
        private const Int32 SERVER_PORT = 6667;

        public ServerSocket() : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {
            IPAddress ipAddr = IPAddress.Parse(SERVER_URL);
            _ipEndPoint = new IPEndPoint(ipAddr, SERVER_PORT);
        }

        public void BindAndStartListen()
        {
            Bind(_ipEndPoint);
            Listen(Int32.MaxValue);
        }
    }
}