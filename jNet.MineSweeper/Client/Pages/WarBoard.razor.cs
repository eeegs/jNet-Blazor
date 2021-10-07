using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using jNet.MineSweeper.Client;
using jNet.MineSweeper.Client.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using jNet.MineSweeper.Shared;

namespace jNet.MineSweeper.Client.Pages
{
	public partial class WarBoard : IAsyncDisposable
	{
		HubConnection hubConnection = default!;
		IEnumerable<string> Games = Enumerable.Empty<string>();
		string? selectedGame;
		string GameName = "Game Name";

		public WarBoard()
		{
		}

		private Task HubConnection_Reconnected(string arg)
		{
			return InvokeAsync(StateHasChanged);
		}

		private Task HubConnection_Reconnecting(Exception arg)
		{
			return InvokeAsync(StateHasChanged);
		}

		public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;
		public string ConnectionState => hubConnection?.State.ToString() ?? "Diconnected";

		Task Connect(MouseEventArgs e)
		{
			return hubConnection.StartAsync();
		}

		Task NewGame(MouseEventArgs e)
		{
			var player = new Player("scott", hubConnection.ConnectionId);
			var game = new Game { GameName = GameName };
			game.Players.Add(player);

			return hubConnection.InvokeAsync<Game>("NewGame", game);
		}

		protected override Task OnInitializedAsync()
		{
			hubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/wargame"))
				.WithAutomaticReconnect()
				.Build();

			hubConnection.Reconnecting += HubConnection_Reconnecting;
			hubConnection.Reconnected += HubConnection_Reconnected;
			hubConnection.Closed += HubConnection_Reconnecting;

			hubConnection.On<IEnumerable<string>>("GameList", list => {
				Games = list;
				InvokeAsync(StateHasChanged);
			});

			return base.OnInitializedAsync();

		}

		public async ValueTask DisposeAsync()
		{
			if (hubConnection is not null)
			{
				hubConnection.Reconnecting -= HubConnection_Reconnecting;
				hubConnection.Reconnected -= HubConnection_Reconnected;
				hubConnection.Closed -= HubConnection_Reconnecting;
				await hubConnection.DisposeAsync();
			}
		}
	}
}