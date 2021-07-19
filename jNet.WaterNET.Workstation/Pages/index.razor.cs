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

		Setting settings = new() { Name = nameof(Index) };
		protected async override Task OnInitializedAsync()
		{
			if (Store is not null)
			{
				settings = (await Store.Get<Setting>(q=>q.Name== nameof(Index))).FirstOrDefault() ?? settings;
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
