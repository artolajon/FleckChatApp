using FleckChatApp.Helpers;
using System;

namespace FleckChatApp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string port = "9999";

            ChatApp app = new ChatApp();

            app.IsServer = CheckIfIsAServer();
            app.Start(port);

            TextInputListener((text) => app.Input(text));

            // When listener ends means we need to close the aplication
            app.Close();

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

        private static bool CheckIfIsAServer()
        {
            Console.WriteLine("Is server? (y/n)");
            return Console.ReadLine() == "y";
        }
    }
}
