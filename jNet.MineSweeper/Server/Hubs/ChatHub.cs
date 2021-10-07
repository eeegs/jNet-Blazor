using System;
using System.Threading.Tasks;
using jNet.MineSweeper.Shared;
using Microsoft.AspNetCore.SignalR;

namespace jNet.MineSweeper.Server.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			return base.OnDisconnectedAsync(exception);
		}
	}
}
