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
            Socket clientSocket = null;
            try
            {
                clientSocket = _serverSocket.Accept();
                Console.WriteLine("Connection is established, listening for command...");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Fail to create client context\nError: {0}", ex.Message);
                clientSocket?.Dispose();
            }
            return new ClientContextHolder(clientSocket);
        }

        public void Dispose()
        {
            _serverSocket?.Dispose();
        }
    }
}
