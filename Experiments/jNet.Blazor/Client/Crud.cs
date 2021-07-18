using jNet.Autoform;
using jNet.CRUD;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace jNet.Blazor.Client
{
	public class Crud<TModel, TKey> : ICrudView<TModel, TKey>
		where TModel : class, IHaveId<TKey>, new()
	{
		private Dictionary<TKey, TModel> Cache = new();
		public readonly HttpClient Http;
		public readonly string Endpoint;
		private readonly string QueryParam = "";

		Func<TKey, string> ICrudView<TModel, TKey>.ID2URI { get; set; }

		public Crud(HttpClient http)
		{
			Http = http;
			Endpoint = "/api/business";
		}

		public Crud(HttpClient http, string endpoint, long businessId)
		{
			Http = http;
			Endpoint = "/api/" + endpoint;
			QueryParam = $"?BID={businessId}";
		}

		Task<TModel> ICrud<TModel, TKey>.Get(TKey id)
		{
			if (((ICrud<TModel, TKey>)this).IsDefaultKey(id))
			{
				return Task.FromResult<TModel>(default);
			}

			if (Cache.TryGetValue(id, out var res))
			{
				return Task.FromResult(res);
			}
			var tsk = Http.GetFromJsonAsync<TModel>($"{Endpoint}/{id}{QueryParam}");
			return tsk;
		}

		Task<IEnumerable<TModel>> ICrud<TModel, TKey>.Get()
		{
			try
			{
				var res = Http.GetFromJsonAsync<IEnumerable<TModel>>($"{Endpoint}{QueryParam}");
				return res;
			}
			catch (AccessTokenNotAvailableException exception)
			{
				exception.Redirect();
			}
			return Task.FromResult(Cache.Values.AsEnumerable());
		}

		Task<bool> ICrud<TModel, TKey>.Save(TModel entity)
		{
			throw new NotImplementedException();
		}

		Task<bool> ICrud<TModel, TKey>.Delete(TKey id)
		{
			throw new NotImplementedException();
		}

		bool ICrud<TModel, TKey>.IsDefaultKey(TKey value)
		{
			return value switch
			{
				long s => s == default,
				int s => s == default,
				byte s => s == default,
				object s => s == default,
				_ => false
			};
		}
	}
}
