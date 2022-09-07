using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, string> _connections;
        public ChatHub(IDictionary<string, string> connections)
        {
            _connections = connections;
        }

        public void AddUser(string userId) {
            if (userId != "") {
                Console.WriteLine($"Add user : {userId}");
                if (_connections.ContainsKey(userId)) {
                    _connections.Remove(userId);
                }
                    _connections.Add(userId, Context.ConnectionId);
            }
        }

        public void RemoveUser(string userId)
        {
            if (userId != "")
            {
                Console.WriteLine($"Remove user : {userId}");
                if (_connections.ContainsKey(userId))
                {
                    _connections.Remove(userId);
                }
            }
        }

        public async Task SendMessage(string to, string message) {
            if (_connections.TryGetValue(to, out string connectionReceiverId)) {
                string senderId = _connections.FirstOrDefault(c => c.Value == Context.ConnectionId).Key;
                Console.WriteLine($"Sender: {senderId} \n Receiver: ${connectionReceiverId} \n Message: {message}");
                await Clients.Client(connectionReceiverId).SendAsync("message-receive", message, senderId);
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
