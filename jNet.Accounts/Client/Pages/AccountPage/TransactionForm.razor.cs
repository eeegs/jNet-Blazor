using jNet.Accounts.Shared.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages.AccountPage
{
	public partial class TransactionForm : ComponentBase
	{
		[Parameter] public Transaction Transaction { get; set; } = new("");
		[Inject] Store.TaxEntries XStore { get; set; } = default!;
		[Inject] Store.Accounts AStore { get; set; } = default!;

		Task EntriesChanged(IEnumerable<Entry> entries)
		{
			return Task.CompletedTask;
		}
	}
}
