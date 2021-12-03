using FleckChatApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleckChatApp
{
    internal class ChatApp
    {
        public bool IsServer;
        public bool IsActive = false;
        public int Port = 0;
        private ChatServer Server;
        private ChatClient Client;

        public void Start(int port)
        {
            Port = port;
            PrintHeaderInfo();
            if (IsServer)
            {
                Server = new ChatServer(port);
            }
            else
            {
                Client = new ChatClient(port);
            }
            IsActive = true;
            
        }

        private void PrintHeaderInfo()
        {
            Console.WriteLine("");
            Console.WriteLine(@"    ________          __      ________          __     ___              ");
            Console.WriteLine(@"   / ____/ /__  _____/ /__   / ____/ /_  ____ _/ /_   /   |  ____  ____ ");
            Console.WriteLine(@"  / /_  / / _ \/ ___/ //_/  / /   / __ \/ __ `/ __/  / /| | / __ \/ __ \");
            Console.WriteLine(@" / __/ / /  __/ /__/ ,<    / /___/ / / / /_/ / /_   / ___ |/ /_/ / /_/ /");
            Console.WriteLine(@"/_/   /_/\___/\___/_/|_|   \____/_/ /_/\__,_/\__/  /_/  |_/ .___/ .___/ ");
            Console.WriteLine(@"                                                         /_/   /_/      ");
            Console.WriteLine("");
            Console.WriteLine("Local chat application that is communicated through the Web Services Fleck library");
            Console.WriteLine(IsServer ? "Server mode" : "Client mode");
            Console.WriteLine("Port: "+Port);
            Console.WriteLine("");
            Console.WriteLine($"Write {Constants.ExitCommand} to close");
            Console.WriteLine("");
        }

        public void Input(string text)
        {
            if (!IsServer)
            {
                Client.SendMessage(text);
            }
        }

        public void Close()
        {
            if (IsServer)
            {
                Server.Close();
                Console.WriteLine("Server closed");
            }
            else
            {
                Client.Close();
                
            }
        }


    }
}
