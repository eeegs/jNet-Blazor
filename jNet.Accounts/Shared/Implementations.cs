using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Shared
{
	public abstract class Bag<T> : IHaveBag<T>
	{
		public Dictionary<string, T?> Variables { get; init; } = new();
		public virtual T? this[string key]
		{
			get => Variables.TryGetValue(key, out var value) ? value : default;
			set => Variables[key] = value;
		}
	}

	public abstract class BagNew<T> : Bag<T>, IHaveBag<T>
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
