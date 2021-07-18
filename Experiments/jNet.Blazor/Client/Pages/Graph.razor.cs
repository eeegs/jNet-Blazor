using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	public partial class Graph
	{
		private int count = 0;

		private static Coord[] circles = new[]
		{
			new Coord(new Vector2(150, 100), 20, Color.FromArgb(200,192,192)) { Text = "0" },
			new Coord(new Vector2(200, 100), 20, Color.FromArgb(200,192,192)) { Text = "1" },
			new Coord(new Vector2(250, 100), 20, Color.FromArgb(200,192,192)) { Text = "2" },
			new Coord(new Vector2(300, 100), 20, Color.FromArgb(200,192,192)) { Text = "3" },
			//new Coord(new Vector2(350, 100), 20, Color.FromArgb(200,192,192)) { Text = "4" },
			//new Coord(new Vector2(400, 100), 20, Color.FromArgb(200,192,192)) { Text = "5" },
			//new Coord(new Vector2(450, 100), 20, Color.FromArgb(200,192,192)) { Text = "6" },
			//new Coord(new Vector2(500, 100), 20, Color.FromArgb(200,192,192)) { Text = "7" },
			//new Coord(new Vector2(550, 100), 20, Color.FromArgb(200,192,192)) { Text = "8" },
			//new Coord(new Vector2(600, 100), 20, Color.FromArgb(200,192,192)) { Text = "9" },
			//new Coord(new Vector2(150, 150), 20, Color.FromArgb(200,192,192)) { Text = "10" },
			//new Coord(new Vector2(200, 150), 20, Color.FromArgb(200,192,192)) { Text = "11" },
			//new Coord(new Vector2(250, 150), 20, Color.FromArgb(200,192,192)) { Text = "12" },
			//new Coord(new Vector2(300, 150), 20, Color.FromArgb(200,192,192)) { Text = "13" },
			//new Coord(new Vector2(350, 150), 20, Color.FromArgb(200,192,192)) { Text = "14" },
			//new Coord(new Vector2(400, 150), 20, Color.FromArgb(200,192,192)) { Text = "15" },
			//new Coord(new Vector2(450, 150), 20, Color.FromArgb(200,192,192)) { Text = "16" },
			//new Coord(new Vector2(500, 150), 20, Color.FromArgb(200,192,192)) { Text = "17" },
			//new Coord(new Vector2(550, 150), 20, Color.FromArgb(200,192,192)) { Text = "18" },
			//new Coord(new Vector2(600, 150), 20, Color.FromArgb(200,192,192)) { Text = "19" },
			//new Coord(new Vector2(155, 110), 20, Color.FromArgb(200,192,192)) { Text = "20" },
			//new Coord(new Vector2(205, 110), 20, Color.FromArgb(200,192,192)) { Text = "21" },
			//new Coord(new Vector2(255, 110), 20, Color.FromArgb(200,192,192)) { Text = "22" },
			//new Coord(new Vector2(305, 110), 20, Color.FromArgb(200,192,192)) { Text = "23" },
			//new Coord(new Vector2(355, 110), 20, Color.FromArgb(200,192,192)) { Text = "24" },
			//new Coord(new Vector2(405, 110), 20, Color.FromArgb(200,192,192)) { Text = "25" },
			//new Coord(new Vector2(455, 110), 20, Color.FromArgb(200,192,192)) { Text = "26" },
			//new Coord(new Vector2(505, 110), 20, Color.FromArgb(200,192,192)) { Text = "27" },
			//new Coord(new Vector2(555, 110), 20, Color.FromArgb(200,192,192)) { Text = "28" },
			//new Coord(new Vector2(605, 110), 20, Color.FromArgb(200,192,192)) { Text = "29" },
			//new Coord(new Vector2(155, 170), 20, Color.FromArgb(200,192,192)) { Text = "30" },
			//new Coord(new Vector2(205, 170), 20, Color.FromArgb(200,192,192)) { Text = "31" },
			//new Coord(new Vector2(255, 170), 20, Color.FromArgb(200,192,192)) { Text = "32" },
			//new Coord(new Vector2(305, 170), 20, Color.FromArgb(200,192,192)) { Text = "33" },
			//new Coord(new Vector2(355, 170), 20, Color.FromArgb(200,192,192)) { Text = "34" },
			//new Coord(new Vector2(405, 170), 20, Color.FromArgb(200,192,192)) { Text = "35" },
			//new Coord(new Vector2(455, 170), 20, Color.FromArgb(200,192,192)) { Text = "36" },
			//new Coord(new Vector2(505, 170), 20, Color.FromArgb(200,192,192)) { Text = "37" },
			//new Coord(new Vector2(555, 170), 20, Color.FromArgb(200,192,192)) { Text = "38" },
			//new Coord(new Vector2(605, 170), 20, Color.FromArgb(200,192,192)) { Text = "39" },
		};

		private Edge[] joins = new Edge[]
		{
			new Edge(new Join { Coord= circles[0], Angle =.25, Strength = 4}, new Join { Coord= circles[1], Angle = .75, Strength = 4}),
			new Edge(new Join { Coord= circles[1], Angle =.25, Strength = 4}, new Join { Coord= circles[2], Angle = .75, Strength = 4}),
			new Edge(new Join { Coord= circles[2], Angle =.25, Strength = 4}, new Join { Coord= circles[0], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[1], Angle =.25, Strength = 4}, new Join { Coord= circles[2], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[1], Angle =.25, Strength = 4}, new Join { Coord= circles[3], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[1], Angle =.25, Strength = 4}, new Join { Coord= circles[4], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[1], Angle =.25, Strength = 4}, new Join { Coord= circles[8], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[10], Angle =.25, Strength = 4}, new Join { Coord= circles[11], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[11], Angle =.25, Strength = 4}, new Join { Coord= circles[12], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[11], Angle =.25, Strength = 4}, new Join { Coord= circles[14], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[11], Angle =.25, Strength = 4}, new Join { Coord= circles[3], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[12], Angle =.25, Strength = 4}, new Join { Coord= circles[3], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[13], Angle =.25, Strength = 4}, new Join { Coord= circles[15], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[14], Angle =.25, Strength = 4}, new Join { Coord= circles[6], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[15], Angle =.25, Strength = 4}, new Join { Coord= circles[6], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[16], Angle =.25, Strength = 4}, new Join { Coord= circles[7], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[2], Angle =.25, Strength = 4}, new Join { Coord= circles[3], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[3], Angle =.25, Strength = 4}, new Join { Coord= circles[15], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[3], Angle =.25, Strength = 4}, new Join { Coord= circles[5], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[4], Angle =.25, Strength = 4}, new Join { Coord= circles[16], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[4], Angle =.25, Strength = 4}, new Join { Coord= circles[6], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[5], Angle =.25, Strength = 4}, new Join { Coord= circles[16], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[5], Angle =.25, Strength = 4}, new Join { Coord= circles[6], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[6], Angle =.25, Strength = 4}, new Join { Coord= circles[17], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[6], Angle =.25, Strength = 4}, new Join { Coord= circles[8], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[8], Angle =.25, Strength = 4}, new Join { Coord= circles[9], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[9], Angle =.25, Strength = 4}, new Join { Coord= circles[18], Angle = .75, Strength = 4}),
			//new Edge(new Join { Coord= circles[18], Angle =.25, Strength = 4}, new Join { Coord= circles[19], Angle = .75, Strength = 4}),
		};

		private Move[] moves;

		public Graph()
		{
			circles.Layout1(joins);
			foreach (var c in circles)
			{
				c.Location = c.Grid * 50;
			}
			moves = new Move[circles.Length];
			for (int i = 0; i < moves.Length; i++)
			{
				moves[i] = new Move();
			}
		}

		private bool RunAnimation = false;

		private async Task Run()
		{
			RunAnimation = true;
			while (RunAnimation)
			{
				await Task.Delay(50);
				if (RunAnimation)
				{
					count++;
					circles.ForceLayout(joins, moves);
					StateHasChanged();
				}
			}
		}

		private void Focused(bool focus, Coord coord)
		{
			if (focus)
			{
			}
			else
			{
			}
		}
	}

	public class Join
	{
		public Coord Coord;
		public double Angle;
		public double Strength;
	}

	public class Move
	{
		public Vector2 velocity;
		//public Vector2 force;
	}


	public class Coord
	{
		private Vector2 _location;
		private Func<Vector2, Vector2> _constrain;

		public Coord(Vector2 location, double radius, Color color, Func<Vector2, Vector2> constrain = null)
		{
			_location = location;
			Radius = radius;
			Color = color;
			_constrain = constrain;
		}
		public Vector2 Location
		{
			get => _location;
			set
			{
				_location = _constrain is not null ? _constrain(value) : value;
			}
		}
		public double Radius { get; set; }
		public Color Color { get; set; }
		public string Text { get; set; }
		public Vector2 Grid { get; set; }
	}

	public class Edge
	{
		public Edge(Join left, Join right)
		{
			Left = left;
			Right = right;
		}
		public Join Left { get; set; }
		public Join Right { get; set; }

		public void Deconstruct(out Join left, out Join right)
		{
			left = Left;
			right = Right;
		}
	}


	public static class Extentions
	{
		public static string toHex(this Color me)
		{
			if (me.IsEmpty)
			{
				return "none";
			}
			var hex = $"#{me.R:X2}{me.G:X2}{me.B:X2}{me.A:X2}";
			return hex;
		}

		public static void Longest(this Coord[] coords, Edge[] edges)
		{
			var x = coords.Select(q => q.Longest(edges, new()));
			var r = x.OrderByDescending(q => q.Count).FirstOrDefault();

			int i = 0;
			foreach (var c in r)
			{
				c.Grid = new Vector2(i++, 3);
			}
		}

		private static List<Coord> Longest(this Coord coord, Edge[] edges, List<Coord> path)
		{
			if (path.Contains(coord))
			{
				return path;
			}
			path.Add(coord);
			var r = edges.Where(q => q.Left.Coord == coord).Select(q => q.Right.Coord.Longest(edges, new(path)));
			var l = edges.Where(q => q.Right.Coord == coord).Select(q => q.Left.Coord.Longest(edges, new(path)));
			var x = r.Concat(l).OrderByDescending(q => q.Count).FirstOrDefault();
			return x;
		}

		public static void Layout1(this Coord[] coords, Edge[] edges)
		{
			var rnd = new Random();

			// randomly position
			foreach (var c in coords)
			{
				c.Grid = new Vector2(rnd.Next(12) + 3, rnd.Next(8) + 2);
			}
		}
		public static void Layout2(this Coord[] coords, Edge[] edges, params Coord[] fix)
		{
			var rnd = new Random();

			(int i, int j) center = (3, 3);

			foreach (var c in coords.Where(q => !fix.Contains(q)))
			{
				int m_min = int.MaxValue;
				(int x, int y) coord = (0, 0);
				var rights = edges.Where(q => q.Left.Coord == c).Select(q => q.Right.Coord);
				var lefts = edges.Where(q => q.Right.Coord == c).Select(q => q.Left.Coord);
				var all = lefts.Concat(rights);

				List<(int m, int d, int i, int j)> locs = new();

				for (int j = 1; j < coords.Length - 1; j++)
				{
					for (int i = 1; i < coords.Length - 1; i++)
					{
						if (!coords.Any(q => q.Grid == new Vector2(i, j)) || c.Grid == new Vector2(i, j))
						{
							var m = all.Min(q => q.Manhatten((i, j)));
							var d = (i - center.i) * (i - center.i) + (j - center.j) * (j - center.j);
							locs.Add((m, d, i, j));
						}
						else
						{
						}
					}
				}

				if (locs.Any())
				{
					var x = locs.OrderBy(q => (double)q.m)/*.ThenByDescending(q => q.d)*/.ToArray();
					var y = x.First();
					Console.WriteLine($"For {c.Text}: {y}");
					c.Grid = new Vector2(y.i, y.j);
				}
			}
		}

		public static int Manhatten(this Coord left, (int x, int y) right)
		{
			var dx = Math.Abs(left.Grid.X - right.x);
			var dy = Math.Abs(left.Grid.Y - right.y);
			return (int)(dx + dy);
		}

		public static void Layout4(this Coord[] coords, Edge[] edges)
		{
			for (int s = 1; s < coords.Length; s++)
			{
				coords.Layoutx(edges, s);
			}
		}

		public static void Layoutx(this Coord[] coords, Edge[] edges, int startRow)
		{
			var rnd = new Random();

			foreach (var c in coords.Where(q => q.Grid.Y >= startRow))
			{
				int m_min = int.MaxValue;
				var coord = Vector2.Zero; ;
				var rights = edges.Where(q => q.Left.Coord == c).Select(q => q.Right.Coord);
				var lefts = edges.Where(q => q.Right.Coord == c).Select(q => q.Left.Coord);
				var all = lefts.Concat(rights);

				for (int j = startRow; j < coords.Length; j++)
				{
					for (int i = 0; i < coords.Length; i++)
					{
						if (!coords.Any(q => q.Grid == new Vector2(i, j)) || c.Grid == new Vector2(i, j))
						{
							var m = all.Min(q => q.Manhatten((i, j)));
							if (m < m_min)
							{
								m_min = m;
								coord = new Vector2(i, j);
							}
						}
						else
						{
						}
					}
				}

				if (m_min < int.MaxValue)
				{
					c.Grid = coord;
				}
			}
		}

		public static void ForceLayout(this Coord[] coords, Edge[] edges, Move[] moves)
		{
			var centre = new Vector2(500, 300);

			for (int i = 0; i < coords.Length; i++)
			{
				var force = Vector2.Zero;
				var cl = coords[i];
				for (int j = 0; j < coords.Length; j++)
				{
					var cr = coords[j];

					var d2 = Vector2.DistanceSquared(cl.Location, cr.Location);
					if (d2 > 0)
					{
						var vd = Vector2.Normalize(cr.Location - cl.Location);
						force += -vd * 20000f / d2;
					}
					else
					{
						force += new Vector2(i * 0.1F, 0);
					}
				}

				//foreach(var j in edges)
				//{
				//	if (j.Left.Coord == cl || j.Right.Coord == cl) continue;
				//	var ec = (j.Left.Coord.Location + j.Right.Coord.Location) / 2;
				//	var d3 = Vector2.Distance(cl.Location, ec);
				//	if (d3 > 0)
				//	{
				//		var vd = Vector2.Normalize(ec - cl.Location);
				//		force += -vd * 200f / d3;
				//	}
				//	else
				//	{
				//		force += new Vector2(i * 0.1F, 0);
				//	}
				//}

				var dc = Vector2.Distance(cl.Location, centre);
				if (dc > 0)
				{
					var vd = Vector2.Normalize(centre - cl.Location);
					force += vd * dc * 0.2F;
				}
				else
				{
					force += new Vector2(i * 0.1F, 0);
				}

				//var vx = new Vector2(cl.Location.X, 300);
				//var dx = Vector2.Distance(cl.Location, vx);
				//if (dx > 0)
				//{
				//	var vd = Vector2.Normalize(vx - cl.Location);
				//	force += vd * dx * 0.1F;
				//}

				var left = edges.Where(q => q.Left.Coord == cl).Select(q => q.Right.Coord);
				var right = edges.Where(q => q.Right.Coord == cl).Select(q => q.Left.Coord);
				var all = left.Concat(right);
				foreach (var c in all)
				{
					var d = Vector2.Distance(cl.Location, c.Location) - 100;
					if (d != 0)
					{
						var vd = Vector2.Normalize(c.Location - cl.Location);
						force += vd * d * 0.3F;
					}
					else
					{
						force += new Vector2(i * 0.1F, 0);
					}
				}

				moves[i].velocity = (moves[i].velocity + force) * 0.5F;
			}

			var sum = Vector2.Zero;
			for (int k = 0; k < coords.Length; k++)
			{
				var loc = moves[k].velocity;
				coords[k].Location += loc;
				sum += coords[k].Location;
			}
			//sum /= coords.Length;
			//sum -= centre;
			//for (int p = 0; p < coords.Length; p++)
			//{
			//	coords[p].Location -= sum;
			//}
		}
	}
}
