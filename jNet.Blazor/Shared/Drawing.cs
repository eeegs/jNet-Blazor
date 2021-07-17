using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace jNet.Blazor.Shared
{
	public class Node
	{
		//public Guid Key { get; set; } = Guid.NewGuid();

		public string Title { get; set; } = "";

		public IEnumerable<Node> Peers(IEnumerable<Link> links)
		{
			var l = links.Where(q => q.Right == this).Select(q => q.Left);
			var r = links.Where(q => q.Left == this).Select(q => q.Right);
			var a = l.Concat(r);
			return a;
		}
	}

	public class Link
	{
		public Link(Node left, Node right)
		{
			Left = left;
			Right = right;
		}
		public Node Left { get; }
		public Node Right { get; }
	}

	public class Item : Node
	{
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
	}

	public class Entity
	{
		Item _item { get; }

		public string Name
		{
			get => _item.Title;
			set => _item.Title = value;
		}
		public string Description { get; set; } = "";

		static public implicit operator Item(Entity me)
		{
			return me._item;
		}
	}

	public class DataSet
	{
		public ICollection<Entity> Entities { get; } = new List<Entity>();
		public ICollection<Link> Links { get; } = new List<Link>();

		public void Save(string fileName)
		{
			var json = JsonSerializer.Serialize(this);
			System.IO.File.WriteAllText(fileName, json);
		}

		public static DataSet? Load(string fileName)
		{
			if (System.IO.File.Exists(fileName))
			{
				var json = System.IO.File.ReadAllText(fileName);
				var ds = JsonSerializer.Deserialize<DataSet>(json);
				return ds;
			}
			return new DataSet();
		}
	}
}
