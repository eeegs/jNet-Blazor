using jNet.Blazor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace jNet.Blazor.Client.Pages
{
	public partial class Entities
	{
		[Inject]
		protected HttpClient Http { get; private set; }

		protected DataSet Data { get; set; } = new();

		async Task Load()
		{
			var data = await Http.GetFromJsonAsync<DataSet>($"/api/entity");
			Data = data;
		}

		async Task Save()
		{
			await Http.PostAsJsonAsync($"/api/entity", Data);
		}

		void AddNode()
		{
			Data.Entities.Add(
				new() {
					Name = $"Node #{Data.Entities.Count + 1}"
				}
			);
		}
	}
}
