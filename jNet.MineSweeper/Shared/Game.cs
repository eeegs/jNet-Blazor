using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.MineSweeper.Shared
{

	public class Game
	{
		public bool IsRunning { get; set; }

		public string GameName { get; init; } = "";

		public int UserLimit { get; init; } = 4;

		public List<Player> Players { get; init; } = new();
	}


	public class Player
	{
		public Player(string name, string connectionId)
		{
			Name = name;
			ConnectionId = connectionId;
		}

		public string Name { get; }
		public string ConnectionId { get; }
	}
}
