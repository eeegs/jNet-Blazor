using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.MineSweeper.Shared
{
	public interface IClientWarSignals
	{
		Task UserClicked(string user, UserAction action);
		Task ListOthers(string[] others);
		Task GameList(IEnumerable<string> games);
	}
	public interface IServerWarSignals
	{
		Task NewGame(Game game);
		Task JointGame(string name);
		Task LockGame(string name);
	}

	public class UserAction
	{
		public int X { get; set; }
		public int Y { get; set; }
	}
}
