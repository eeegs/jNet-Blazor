using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Shared.Code
{
	public class Link : BaseImpObj, IHaveName, IHaveKey
	{
		private const string JOIN = " ==> ";
		private Guid inverse;

		public Guid Key { get; init; } = Guid.NewGuid();
		public string Name { get; set; } = "";
		public Guid NodeAKey { get; init; }
		public Guid NodeBKey { get; init; }
		public float Length { get; set; }
		public Guid Inverse { get => inverse; init => inverse = value; }

		public Link()
		{
		}

		public Link(Node a, Node b)
		{
			Name = $"{a.Name}{JOIN}{b.Name}";
			NodeAKey = a.Key;
			NodeBKey = b.Key;
		}

		public static Link operator -(Link link)
		{
			var names = link.Name.Split(JOIN)[..2].Reverse();
			var linkr = new Link()
			{
				Name = string.Join(JOIN, names),
				NodeAKey = link.NodeBKey,
				NodeBKey = link.NodeAKey,
				Inverse = link.Key,
			};
			link.inverse = linkr.Key;
			return linkr;
		}

		public float Geolength()
		{
			throw new NotImplementedException();
		}
	}

	public class Node : BaseImpObj, IHaveName, IHaveKey
	{
		public Guid Key { get; init; } = Guid.NewGuid();
		public string Name { get; set; } = "";

		//public Geo Location => throw new NotImplementedException();
		public static Link operator +(Node a, Node b) => new Link(a, b);
	}

	public abstract class BaseImp<T> : IHaveBag<T>
	{
		public Dictionary<string, T?> Variables { get; init; } = new();
		public virtual T? this[string key]
		{
			get => Variables.TryGetValue(key, out var value) ? value : default;
			set => Variables[key] = value;
		}
	}

	public abstract class BaseImpObj : BaseImp<object>, IHaveBag<object>
	{
	}

	public abstract class BaseImpNew<T> : BaseImp<T>, IHaveBag<T>
		where T : new()
	{
		public override T? this[string key]
		{
			get
			{
				var value = base[key];
				if (value is null)
				{
					value = new();
					this[key] = value;
				}
				return value;
			}
			set => base[key] = value;
		}
	}
}

