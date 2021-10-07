using System;
using System.Linq;
using System.Threading.Tasks;
using jNet.MineSweeper.Shared;
using Microsoft.AspNetCore.SignalR;

namespace jNet.MineSweeper.Server.Hubs
{
	public class WarHub : Hub<IClientWarSignals>, IServerWarSignals
	{
		private readonly WarService warService;

		public WarHub(WarService warService)
		{
			this.warService = warService;
		}

		public override Task OnConnectedAsync()
		{
			Clients.All.GameList(new[] { "Tom", "Fred", "Jane" });


			return base.OnConnectedAsync();
		}

		public Task NewGame(Game game)
		{
			warService.Games.Add(game);
			return Clients.All.GameList(warService.Games.Select(q => q.GameName));
		}

		public Task JointGame(string name)
		{
			return Task.CompletedTask;
		}

		public Task LockGame(string name)
		{
			return Task.CompletedTask;
		}
	}
}
