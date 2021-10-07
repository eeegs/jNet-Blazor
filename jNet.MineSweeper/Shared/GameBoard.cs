using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace jNet.MineSweeper.Shared
{
	public class GameBoard
	{
		readonly List<Piece> Pieces = new();
		readonly int Width = 16;
		readonly int Height = 16;
		readonly int Mines = 40;
		DateTime? Started;
		public GameStatus Status { get; private set; } = GameStatus.AwaitingFirstMove;
		public int RemainingCount => Mines - Pieces.Count(q => q.IsFlagged);
		public IEnumerable<Piece> GetRow(int row) => Pieces.Where(q => q.Y == row).OrderBy(q => q.X).ToList();
		public TimeSpan Time => Started is null ? TimeSpan.FromSeconds(0) : (DateTime.UtcNow - Started.Value);

		public GameBoard(int width, int height, int mineCount)
		{
			Width = width;
			Height = height;
			Mines = mineCount;
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					Pieces.Add(new(x, y));
				}
			}
		}

		public void MoveNeighbors(Piece piece)
		{
			var x = GetNeighbors(piece).Where(q => !q.IsOpen);
			foreach (var p in x)
			{
				Move(p);
			}
		}

		public void Move(Piece piece)
		{
			switch (Status)
			{
				case GameStatus.AwaitingFirstMove:
					Started = DateTime.UtcNow;
					FirstMove(piece);
					Status = GameStatus.InProgress;
					Move(piece);
					break;
				case GameStatus.InProgress:
					piece.Open();
					if (piece.IsOpen)
					{
						if (piece.HasMine)
						{
							Status = GameStatus.Failed;
							Move(piece);
						}
						else
						{
							RevealZeros(piece);
							CheckComplete(piece);
						}
					}
					break;
				case GameStatus.Failed:
					foreach (var p in Pieces.Where(q => q.HasMine)) p.Open();
					break;
				case GameStatus.Completed:
					break;
			}
		}

		private void CheckComplete(Piece piece)
		{
			if (Pieces.Where(q => !q.IsOpen).All(q => q.HasMine && q.IsFlagged))
			{
				Status = GameStatus.Completed;
				Move(piece);
			}
		}

		public void SetFlag(Piece piece)
		{
			piece.Flag();
			if(Status == GameStatus.InProgress)
			{
				CheckComplete(piece);
			}
		}

		private void FirstMove(Piece piece)
		{
			Random rand = new Random();
			var dontMine = GetNeighbors(piece).ToList();
			dontMine.Add(piece);
			var mineList = Pieces.Except(dontMine).OrderBy(q => rand.Next()).Take(Mines);
			foreach (var p in mineList) p.HasMine = true;
			foreach (var p in Pieces)
			{
				if (p.HasMine) continue;
				p.AdjacentMines = GetNeighbors(p).Count(q => q.HasMine);
			}
		}

		public void RevealZeros(Piece piece)
		{
			//Get all neighbor Pieces
			if (piece.AdjacentMines == 0)
			{
				var neighborPieces = GetNeighbors(piece).Where(Piece => !Piece.IsOpen);
				foreach (var neighbor in neighborPieces)
				{
					//For each neighbor Piece, reveal that Piece.
					neighbor.Open();
					//If the neighbor is also a 0, reveal all of its neighbors too.
					RevealZeros(neighbor);
				}
			}
		}

		private IEnumerable<Piece> GetNeighbors(Piece piece)
		{
			var (x, y) = piece;
			var nearbyPieces = Pieces
				.Where(p =>
					p.X >= x - 1 &&
					p.X <= x + 1 &&
					p.Y >= y - 1 &&
					p.Y <= y + 1 &&
					p != piece
				);
			return nearbyPieces;
		}
	}

	public class WarBoard : GameBoard
	{
		public WarBoard(int width, int height, int mineCount) : base(width, height, mineCount)
		{
		}
	}
}
