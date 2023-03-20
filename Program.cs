using MyShop.ClientsInteraction;
using System;
using System.Threading.Tasks;

namespace MyShop
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                using (var listener = new Listener())
                using (ClientContextHolder clientContext = listener.StartWaitForClient())
                {
                    await clientContext.StartCommunicationCycle();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application failed with error: {0}", ex.Message);
                Console.WriteLine("Stack trace: {0}", ex.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}
