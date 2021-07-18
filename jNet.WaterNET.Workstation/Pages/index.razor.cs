using jNet.Client.Code;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.WaterNET.Workstation.Pages
{
	public partial class Index
	{
		[Inject] Store? Store { get; set; }

		Setting settings = new() { Name = "Index" };
		protected async override Task OnInitializedAsync()
		{
			if (Store is not null)
			{
				settings = await Store.Get<Setting>(new Guid("6fe02466-bf8a-4cb8-be77-5a3cc1da564f")) ?? settings;
				Store.Set(settings);
			}
			await base.OnInitializedAsync();
		}

		Task Save()
		{
			if (Store is not null)
			{
				return Store.Save();
			}
			return Task.CompletedTask;
		}
	}
}
