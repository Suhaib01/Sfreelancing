using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.SignalR;

namespace LiveChat.signalr.hubs
{
    public class ChatHub : Hub
    {
        public async void Send(string name, string message)
        {
            await Clients.All.SendAsync(name, message);
        }
    }
}