using jNet.Accounts.Shared.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages.AccountPage
{
	public partial class Accounts : ComponentBase
	{
		[Inject] Store.Accounts AStore { get; set; } = default!;
		[Inject] Store.TaxEntries XStore { get; set; } = default!;
		[Inject] Store.Transactions TStore { get; set; } = default!;
		[Inject] Store.Balances BStore { get; set; } = default!;

		double[] widths = new double[] { 200 };

		Account selected = new();

		void Create()
		{
			Codebits.CreateAccounts(AStore, XStore, TStore);
			StateHasChanged();
		}

		Task Save()
		{
			return Task.WhenAll(
				AStore.Save(),
				TStore.Save(),
				BStore.Save(),
				XStore.Save()
			);
		}

		async Task Balance()
		{
			await Task.WhenAll(
				TStore.LoadTask,
				BStore.LoadTask
			);
			var fy = TStore.GetFY();
			BStore.CalculateForFY(fy, TStore);
		}

		void SetSelected()
		{
			selected = AStore["2dad5ffb-68d4-440f-8fba-a7ed4e9fc5df"];
		}
	}
}
