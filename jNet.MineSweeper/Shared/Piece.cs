
namespace jNet.MineSweeper.Shared
{
	public class Piece
	{
		static int iDs = 0;
		public int X { get; }
		public int Y { get; }
		public int Id { get; } = iDs++;
		public bool HasMine { get; set; }
		public int AdjacentMines { get; set; }
		public bool IsOpen { get; private set; }
		public bool IsFlagged { get; private set; }

		public Piece(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void Flag()
		{
			if (!IsOpen)
			{
				IsFlagged = !IsFlagged;
			}
		}

		public void Open()
		{
			if(!IsFlagged)
			{
				IsOpen = true;
			}
		}

		public void Deconstruct(out int x, out int y)
		{
			x = X;
			y = Y;
		}

		public string Character => this switch
		{
			{ IsOpen: true, HasMine: true } => "💣",
			{ IsOpen: true, AdjacentMines: not 0 } => AdjacentMines.ToString(),
			{ IsFlagged: true } => "🚩",
			_ => ""
		};

		public string Color
		{
			get
			{
				if (!IsOpen || AdjacentMines == 0)
				{
					return "#000000";
				}
				var level = 255 / ((AdjacentMines / 3) + 1);
				return (AdjacentMines % 3) switch
				{
					0 => $"#{level:x2}{0:x2}{0:x2}",
					1 => $"#{0:x2}{0:x2}{level:x2}",
					_ => $"#{0:x2}{level>>1:x2}{0:x2}",
				};
			}
		}
	}
}
