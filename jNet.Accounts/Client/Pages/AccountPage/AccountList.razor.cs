using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages.AccountPage
{
	public partial class AccountList : ComponentBase
	{
		[Inject] Store.Accounts Accounts { get; set; } = default!;
		[Inject] Store.Transactions Transactions { get; set; } = default!;
		[Inject] Store.Balances Balances { get; set; } = default!;

	}
}
