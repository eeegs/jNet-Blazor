using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jNet.Network
{
	public class Link<T> : InfoImp, IHaveInfo
		where T : Link<T>
	{
		public Link(Node a, Node b, string? name = null) : base(name ?? $"{a.Name} ==> {b.Name}")
		{
			NodeA = a;
			NodeB = b;
		}
		public Node NodeA { get; init; }
		public Node NodeB { get; init; }
		public float Length { get; set; }
		public T? Inverse { get; set; }

		public float Geolength()
		{
			throw new NotImplementedException();
		}
	}

	public class Node : InfoImp, IHaveInfo
	{
		//public Geo Location => throw new NotImplementedException();
		public Node(string name) : base(name)
		{
		}
	}

	public abstract class InfoImp : IHaveInfo
	{
		readonly bool ReadOnly = false;
		readonly Dictionary<string, object?> bag = new();
		public int Id { get; set; }
		public string Name { get; init; }
		public object? this[string key]
		{
			get => ReadOnly ? null : bag.TryGetValue(key, out var value) ? value : null;
			set { if (!ReadOnly) bag[key] = value; }
		}
		protected InfoImp(string name, bool readOnly = false)
		{
			Name = name;
			ReadOnly = readOnly;
		}

		[JsonProperty]
		private Dictionary<string, object?> Properties => bag;
	}
}
