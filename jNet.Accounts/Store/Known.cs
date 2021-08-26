using jNet.Accounts.Code;
using jNet.Accounts.Shared;
using System.Collections.Generic;
using System.Net.Http;

namespace jNet.Accounts.Store
{
	public abstract class Known<T, Tk> : Store<T, Tk>
		where T : IHaveKey<Tk>, new()
		where Tk : notnull
	{
		protected Known(HttpClient httpClient) : base(httpClient)
		{
		}

		public Dictionary<string, Tk> KnownEntities { get; init; } = new();

		public T this[string name]
		{
			get
			{
				if (KnownEntities.TryGetValue(name, out var key))
				{
					return this[key];
				}
				var newOne = new T() { };
				KnownEntities[name] = newOne.Key;
				return newOne;
			}
		}
	}
}
