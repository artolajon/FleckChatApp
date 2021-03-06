using FleckChatApp.Helpers;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleckChatApp
{
    internal class ChatClient
    {
        private ClientWebSocket ClientWebSocket;
        private CancellationTokenSource CancellationToken;
        private Task Connection;

        public ChatClient(int port)
        {
            ClientWebSocket = new ClientWebSocket();
            CancellationToken = new CancellationTokenSource();
            Connection = ClientWebSocket.ConnectAsync(new Uri($"{Constants.BaseUrl}:{port}"), CancellationToken.Token);

            Connection.ContinueWith(async tsk =>
            {
                await MessageListener();
            });
            
        }

        private Task MessageListener()
        {
            return Task.Run(async () => {
                while (true)
                {
                    // wait for new messages
                    byte[] buffer = new byte[1024];
                    var result = await ClientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.Token);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        OnMessage(message);
                    }
                    else if(result.MessageType == WebSocketMessageType.Close)
                    {
                        OnClose();
                    }
                    
                }
            });
        }

        private void OnClose()
        {
            Console.WriteLine("Connection closed");
        }

        private void OnMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            
        }

        public void SendMessage(string message)
        {
            Connection.ContinueWith(async tsk =>
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));

                await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.Token);
            });
        }

        public void Close()
        {
            ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "User logged out", CancellationToken.Token);
            CancellationToken.Cancel();
        }
    }
}
