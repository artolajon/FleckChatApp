using System;
using System.Collections.Generic;
using System.Text;

namespace FleckChatApp
{
    internal class ChatApp
    {
        public bool IsServer;
        public bool IsActive = false;
        private ChatServer Server;
        private ChatClient Client;

        public void Start(int port)
        {
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
