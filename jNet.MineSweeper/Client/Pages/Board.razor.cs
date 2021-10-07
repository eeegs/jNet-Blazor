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
using System.Diagnostics;
using System.Threading;
using jNet.MineSweeper.Shared;

namespace jNet.MineSweeper.Client.Pages
{
	public partial class Board : IDisposable
	{
		Timer timer;
		GameBoard game = default!;
		[Parameter] public int Width { get; set; } = 16;
		[Parameter] public int Height { get; set; } = 16;
		[Parameter] public int Mines { get; set; } = 40;
		public GameStatus Status { get; private set; } = GameStatus.AwaitingFirstMove;

		protected override Task OnParametersSetAsync()
		{
			NewGame();
			return base.OnParametersSetAsync();
		}

		public Board()
		{
			timer = new Timer(TimeCallBack, null, 1000, 1000);
		}

		void OnClick(MouseEventArgs e, Piece piece)
		{
			if (game.Status is GameStatus.AwaitingFirstMove or GameStatus.InProgress)
			{
				if (e.Button == 2)
				{
					game.SetFlag(piece);
				}
				else
				{
					if (e.CtrlKey && piece.IsOpen)
					{
						game.MoveNeighbors(piece);
					}
					else
					{
						game.Move(piece);
					}
				}
			}
		}

		void NewGame()
		{
			game = new GameBoard(Width, Height, Mines);
		}

		void TimeCallBack(object? state)
		{
			InvokeAsync(StateHasChanged);
		}

		public void Dispose()
		{
			timer.Dispose();
		}
	}
}