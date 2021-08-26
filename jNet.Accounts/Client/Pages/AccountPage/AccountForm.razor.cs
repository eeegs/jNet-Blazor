using jNet.Accounts.Shared.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages.AccountPage
{
	public partial class AccountForm
	{
		[Parameter] public Account Account { get; set; } = new();
		[Inject] Store.TaxEntries XStore { get; set; } = default!;
	}
}
