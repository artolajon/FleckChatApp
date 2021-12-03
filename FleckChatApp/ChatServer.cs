using Fleck;
using FleckChatApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FleckChatApp
{
    internal class ChatServer
    {
        private WebSocketServer Server;
        private Dictionary<Guid, string> UserList = new Dictionary<Guid, string>();
        List<IWebSocketConnection> AllSockets = new List<IWebSocketConnection>();

        public ChatServer(string port)
        {
            Server = new WebSocketServer($"{Constants.BaseUrl}:{port}");
            Server.Start(socket =>
            {
                socket.OnOpen = () => OnOpen(socket);
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = message => OnMessage(socket, message);
            });
        }

        public void Close()
        {
            AllSockets.ForEach(socket => socket.Close());

            Server.Dispose();

        }

        private void OnOpen(IWebSocketConnection socket)
        {
            Console.WriteLine("New connection was created");

            AllSockets.Add(socket);
            socket.Send("Hi! Who are you?");
        }

        private void OnClose(IWebSocketConnection socket)
        {
            Console.WriteLine("Connection closed");

            AllSockets.Remove(socket);

            Guid connectionId = socket.ConnectionInfo.Id;
            if (UserList.ContainsKey(connectionId))
            {
                string username = UserList[connectionId];
                TellEveryone($"{username} has logged out");
            }
        }

        private void OnMessage(IWebSocketConnection socket, string message)
        {
            Console.WriteLine("New message received");

            // Check if is a new connection
            Guid connectionId = socket.ConnectionInfo.Id;
            if (UserList.ContainsKey(connectionId))
            {
                // Send message
                string username = UserList[connectionId];
                TellEveryone($"{username}: {message}");
            }
            else
            {
                // Save usarname in list
                string newUsername = message;
                
                UserList[connectionId] = newUsername;
                TellEveryone($"{newUsername} has logged in");
            }
        }

        private void TellEveryone(string text)
        {
            AllSockets.ForEach(socket => socket.Send(text));
        }

        
    }
}
