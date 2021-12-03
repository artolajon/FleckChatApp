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

            if (isServer)
            {
                ChatServer server = new ChatServer(port);

                // Keep alive
                Console.ReadLine();
            }
            else
            {
                ChatClient client = new ChatClient(port);

                string text;
                do
                {
                    text = Console.ReadLine();
                    client.SendMessage(text);
                } while (text != "EXIT");
                client.Close();
               
            }

        }
    }
}
