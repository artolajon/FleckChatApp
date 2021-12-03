using FleckChatApp.Helpers;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace FleckChatApp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ChatApp app = new ChatApp();
            try
            {
                int port = 9999;

                app.IsServer = CheckIfItHasToBeAServer(port);
                app.Start(port);

                TextInputListener((text) => app.Input(text));

                // When listener ends means we need to close the aplication
                app.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.Message);

                if (app.IsActive)
                {
                    app.Close();
                }
            }

        }

        private static void TextInputListener(Action<string> callback)
        {
            string text;
            do
            {
                text = Console.ReadLine();
                if (text != Constants.ExitCommand)
                {
                    callback(text);
                }

            } while (text != Constants.ExitCommand);
        }

        private static bool CheckIfItHasToBeAServer(int port)
        {
            // Evaluate current system ports.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var activePorts = ipGlobalProperties.GetActiveTcpListeners().Concat(ipGlobalProperties.GetActiveUdpListeners());

            // If the port isn't on the lists means is free
            bool freePort = true;
            freePort = !activePorts.Where(c => c.Port == port).Any();

            return freePort;

        }
    }
}
