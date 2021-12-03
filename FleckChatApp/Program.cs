using FleckChatApp.Helpers;
using System;

namespace FleckChatApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string port = "9999";

            Console.WriteLine("Is server? (y/n)");
            bool isServer = Console.ReadLine() == "y";

            ChatServer server = null;
            ChatClient client = null;

            if (isServer)
            {
                server = new ChatServer(port);
            }
            else
            {
                client = new ChatClient(port);
            }

            string text;
            do
            {
                text = Console.ReadLine();
                if (isServer && text != Constants.ExitCommand)
                {
                    client.SendMessage(text);
                }
                    
            } while (text != Constants.ExitCommand);

            if (isServer)
            {
                server.Close();
            }
            else
            {
                client.Close();
            }
            

        }
    }
}
