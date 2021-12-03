using System;
using System.Collections.Generic;
using System.Text;

namespace FleckChatApp
{
    internal class ChatApp
    {
        public bool IsServer;
        private ChatServer Server;
        private ChatClient Client;

        public void Start(string port)
        {
            if (IsServer)
            {
                Server = new ChatServer(port);
            }
            else
            {
                Client = new ChatClient(port);
            }
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
            }
            else
            {
                Client.Close();
            }
        }


    }
}
