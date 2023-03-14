using MyShop.ClientsInteraction;
using System;

namespace MyShop
{
    internal class Program
    {
        static void Main()
        {
            Listener listener = null;
            try
            {
                listener = new Listener();
                ClientContextHolder clientContext = listener.StartWaitForClient();
                clientContext.StartCommunication();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application failed with error: {0}", ex.Message);
                Console.WriteLine("Stack trace: {0}", ex.StackTrace);
                Environment.Exit(1);
            }
            finally
            {
                listener?.Dispose();
            }
        }
    }
}
