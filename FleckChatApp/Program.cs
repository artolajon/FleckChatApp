using System;

namespace FleckChatApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string port = "9999";

            ChatServer server = new ChatServer(port);

            // Keep alive
            Console.ReadLine();
        }
    }
}
