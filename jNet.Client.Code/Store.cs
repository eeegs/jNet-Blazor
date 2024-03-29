﻿using jNet.Shared.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace jNet.Client.Code
{

	public class Store
	{
		readonly Dictionary<Guid, object> store = new();
		readonly HttpClient httpClient;

		public Store(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<IEnumerable<T>> Get<T>(Func<T, bool> predicate = null)
			where T : IHaveKey
		{
			if (!store.Values.OfType<T>().Any())
			{
				await Load<T>();
			}
			var res = store.Values.OfType<T>();
			return predicate is null ? res : res.Where(predicate);
		}

		public async Task<T?> Get<T>(int index)
			where T : IHaveKey
		{
			if (!store.Values.OfType<T>().Any())
			{
				await Load<T>();
			}
			var res = store.Values.OfType<T>().ToArray();
			if (index >= res.Length) return default;
			return res[index];
		}

		public async Task<T?> Get<T>(Guid key)
			where T : IHaveKey
		{
			var res = Retrieve<T>(key);
			if (res is null)
			{
				await Load<T>();
				res = Retrieve<T>(key);
			}
			return res;
		}

		public async Task<Setting> GetSetting<T>(string userName)
			where T : class
		{
			var type = typeof(T).FullName ?? "x";
			var res = store.OfType<Setting>().Where(q => q.Name == type && q.UserName == userName).SingleOrDefault();
			if (res is null)
			{
				res = await httpClient.GetFromJsonAsync<Setting>($"api/Settings/{type}/{userName}") ?? new() { Name = type, UserName = userName };
				Set(res);
			}
			return res;
		}

		private T? Retrieve<T>(Guid key)
			where T : IHaveKey
		{
			if (store.TryGetValue(key, out var val))
			{
				if (val is T x)
				{
					return x;
				}
				throw new InvalidCastException($"Object with a key of '{key}' exists, but is of the type '{val.GetType().FullName}', not the requested type '{typeof(T).FullName}'.");
			}
			return default;
		}

		public void Set<T>(IEnumerable<T> values) where T : IHaveKey => SetInt(values);
		public void Set<T>(params T[] values) where T : IHaveKey => SetInt(values);
		void SetInt<T>(IEnumerable<T> values)
			where T : IHaveKey
		{
			foreach (var v in values)
			{
				store[v.Key] = v;
			}
		}

		public async Task Save()
		{
			var types = store.Values.ToLookup(q => q.GetType().FullName);
			foreach (var items in types)
			{
				if (items.Key == typeof(Setting).FullName)
				{
					await httpClient.PostAsJsonAsync($"api/Settings/", items, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
				}
				else
				{
					await httpClient.PostAsJsonAsync($"api/Store/{items.Key}", items, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
				}
			}
		}

		public async Task Load<T>()
			where T : IHaveKey
		{
			//await Task.Delay(1000);
			var result = await httpClient.GetFromJsonAsync<IEnumerable<T>>($"api/Store/{typeof(T).FullName}");
			if (result is not null)
			{
				foreach (var i in result) Set(i);
			}
		}
	}
}
