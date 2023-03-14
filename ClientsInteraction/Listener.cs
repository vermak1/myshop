using System;
using System.Net.Sockets;

namespace MyShop.ClientsInteraction
{
    internal class Listener : IDisposable
    {
        private readonly ServerSocket _serverSocket;

        public Listener()
        {
            _serverSocket = new ServerSocket();
        }

        public ClientContextHolder StartWaitForClient()
        {
            Console.WriteLine("Waiting for new connections...");
            _serverSocket.BindAndStartListen();
            Socket clientSocket = _serverSocket.Accept();
            Console.WriteLine("Connection is established, listening for command...");
            return new ClientContextHolder(clientSocket);
        }

        public void Dispose()
        {
            _serverSocket?.Dispose();
        }
    }
}
