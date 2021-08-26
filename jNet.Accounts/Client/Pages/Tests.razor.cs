using jNet.Accounts.Components;
using jNet.Accounts.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages
{
	public partial class Tests : ComponentBase
	{
		[Inject] Store.Accounts AStore { get; set; } = default!;
		[Inject] Store.Transactions TStore { get; set; } = default!;

		double[] widths = new double[] { 200 };
		Dictionary<Guid, decimal> AccountValues = new();
		PanelContent? PanelContent;
		Account? Selected;

		void OpenPanel(MouseEventArgs args)
		{
			PanelContent?.Open();
		}

		protected async override Task OnInitializedAsync()
		{
			if (AStore is not null)
			{
				//await Store.Load<Account>();
				//await Store.Load<Transaction>();
			}
			await base.OnInitializedAsync();
		}

		void Create()
		{
			if (AStore is not null)
			{
				//Codebits.CreateAccounts(Store);
				//AccountValues = Values(Store).ToDictionary(q => q.Account.Key, q => q.Value);
				StateHasChanged();
			}
		}

		//IEnumerable<(Account Account, decimal Value)> Values(AccountStore store)
		//{
		//	var q1 = from t in store.Get<Transaction>()
		//			 from e in t.Entries
		//			 group e by e.AccountKey into gp
		//			 select (gp.Key, Sum: gp.Sum(q => q.Amount * (int)q.Type));
		//	var accountValues = q1.ToDictionary(q => q.Key, q => q.Sum);

		//	return GetSums(null);

		//	IEnumerable<(Account Account, decimal Value)> GetSums(Account? parent)
		//	{
		//		var parentKey = parent == null ? Guid.Empty : parent.Key;
		//		var items = store.Get<Account>(q => q.ParentKey == parentKey);

		//		foreach (var i in items)
		//		{
		//			decimal sum = 0;
		//			foreach (var c in GetSums(i))
		//			{
		//				sum += c.Value;
		//				yield return c;
		//			}
		//			var itemValue = (accountValues?.Try(i.Key)) ?? 0m;
		//			Console.WriteLine($"{i.Name}, sum: {sum}, val: {itemValue}");
		//			yield return (i, itemValue + sum);
		//		}
		//	}
		//}
	}
}
