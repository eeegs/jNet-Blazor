using jNet.MineSweeper.Shared;
using System.Collections.Generic;

namespace jNet.MineSweeper.Server
{
	public class WarService
	{
		public WarService()
		{
		}

		public List<Game> Games { get; } = new();
	}
}
