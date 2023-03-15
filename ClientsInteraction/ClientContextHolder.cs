using System;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace MyShop.ClientsInteraction
{
    internal class ClientContextHolder
    {
        private const Int32 BUFFER_SIZE = 512;

        private readonly Socket _clientSocket;
        public ClientContextHolder(Socket socket)
        {
            _clientSocket = socket;
        }

        public void StartCommunication()
        {
            try
            {
                while (true)
                {
                    String command = GetCommandFromClient();
                    SendResult(command);
                    Console.WriteLine("Command {0} was received and answered", command);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error within communication cycle occured: {0}", ex.Message);
                throw;
            }
        }

        private String GetCommandFromClient()
        {
            Byte[] buffer = new Byte[BUFFER_SIZE];
            StringBuilder sb = new StringBuilder();
            try
            {
                do
                {
                    Int32 bytes = _clientSocket.Receive(buffer, BUFFER_SIZE, 0);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (_clientSocket.Available != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed while listening for command from client: {0}", ex.Message);
                throw;
            }
            return sb.ToString();
        }

        private void SendResult(String command)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed while sending result to client: {0}", ex.Message);
                throw;
            }
        }

    }
}
