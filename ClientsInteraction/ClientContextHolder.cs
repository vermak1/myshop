using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using MyShop.Commands;
using MyShop.CommonLib;

namespace MyShop.ClientsInteraction
{
    internal class ClientContextHolder : IDisposable
    {
        private readonly Socket _clientSocket;
        public ClientContextHolder(Socket socket)
        {
            try
            {
                _clientSocket = socket;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to create ClientContextHolder.\nError: {0}", ex.Message);
                _clientSocket?.Dispose();
            }
            
        }

        public async Task StartCommunicationCycle()
        {
            try
            {
                while (true)
                {
                    String json = await MessageProcessor.ReceiveMessageAsync(_clientSocket);
                    CommandInfo command = CommandParser.GetCommandFromJson(json);
                    await CommandExecutor.ExecuteCommandAsync(command);
                    //TODO: asnwer to client
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error within communication cycle occured: {0}", ex.Message);
                throw;
            }
        }
        public void Dispose() 
        {
            _clientSocket?.Dispose();
        }

    }
}
