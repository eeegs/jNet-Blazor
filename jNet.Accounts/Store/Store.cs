using jNet.Accounts.Shared;
using jNet.Accounts.Shared.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace jNet.Accounts.Code
{

	public class StoreManager
	{
		readonly HttpClient httpClient;
		readonly Dictionary<Type, BaseStore> stores = new();

		public StoreManager(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public T GetStore<T>()
			where T : BaseStore, new()
		{
			if (stores.TryGetValue(typeof(T), out var store))
			{
				if (store is T x)
				{
					return x;
				}
				throw new InvalidProgramException("Somehow the infrastructre as stare a store of the wrong type");
			}
			var res = new T();
			stores[typeof(T)] = res;
			return res;
		}
	}

	public abstract class BaseStore
	{
		protected object Locker = new();
		protected const string API = "api/Store";
		protected readonly HttpClient httpClient;
		protected bool Loaded;

		protected BaseStore(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}
	}

	public abstract class Store<T> : BaseStore, IEnumerable<T>
		where T : IHaveKey
	{
		static readonly string _fullName = typeof(T).FullName ?? "how did this happen";
		public static IEnumerable<T> Empty { get; } = Enumerable.Empty<T>();

		readonly Dictionary<string, T> store = new();

		protected Store(HttpClient httpClient) : base(httpClient)
		{
			var t = Load();
			LoadTask = t.ContinueWith(_ => this);
		}

		public bool isLoaded => Loaded;
		public Task<Store<T>> LoadTask { get; }
		protected bool Any(Func<T, bool> predicate) => store.Values.Any(predicate);
		protected bool Any() => store.Values.Any();
		public IEnumerable<T> Where(Func<T, bool> predicate) => store.Values.Where(predicate).ToList();
		protected bool Exists(string key) => store.ContainsKey(key);
		public virtual T this[string key] => store[key];
		public void Set(IEnumerable<T> values) => SetInt(values);
		public void Set(params T[] values) => SetInt(values);
		void SetInt(IEnumerable<T> values)

		{
			lock (Locker)
			{
				foreach (var v in values)
				{
					store[v.Key] = v;
				}
			}
		}
		public bool Delete(IEnumerable<T> values) => DeleteInt(values);
		public bool Delete(params T[] values) => DeleteInt(values);
		bool DeleteInt(IEnumerable<T> values)
		{
			lock (Locker)
			{
				bool res = true;
				foreach (var v in values)
				{
					res &= store.Remove(v.Key);
				}
				return res;
			}
		}
		public bool Delete(string key)
		{
			return store.Remove(key);
		}

		public virtual Task Save()
		{
			return httpClient.PostAsJsonAsync($"{API}/{_fullName}", store.Values, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
		}

		public async Task Load()
		{
			if (!Loaded)
			{
				var items = await LoadData(httpClient);
				lock (Locker)
				{
					if (items is not null)
					{
						foreach (var i in items)
						{
							Set(i);
						}
						Loaded = true;
					}
				}
			}
		}

		protected virtual Task<IEnumerable<T>?> LoadData(HttpClient httpClient)
		{
			return httpClient.GetFromJsonAsync<IEnumerable<T>>($"{API}/{_fullName}");
		}

		public IEnumerator<T> GetEnumerator() => store.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => store.Values.GetEnumerator();
	}

	public class MagicStore<T> : Store<T>
		where T : IHaveKey, new()
	{
		public MagicStore(HttpClient httpClient) : base(httpClient) { }

		public override T this[string key]
		{
			get
			{
				lock (Locker)
				{
					if (Exists(key))
					{
						return base[key];
					}
					var res = new T() { Key = key };
					Set(res);
					return res;
				}
			}
		}
	}
}

