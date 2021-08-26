using jNet.Accounts.Components;
using jNet.Accounts.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Components
{
	public partial class AccountPicker : ComponentBase
	{
		[Inject] Store.Accounts AStore { get; set; } = default!;

		[Parameter] public Account? Selected { get; set; }
		[Parameter] public EventCallback<Account> SelectedChanged { get; set; }

		Dropdown picker = default!;
		string currentText = "";
		bool typing = false;

		Task OnInput(ChangeEventArgs args)
		{
			//AStore.AnyChildren
			currentText = args.Value?.ToString() ?? "";

			typing = true;
			if (currentText.Length < 3)
			{
				picker.Close();
				typing = false;
			}
			return Task.CompletedTask;
		}

		IEnumerable<Account> results()
		{
			picker.Open();
			typing = true;
			var q1 = from a in AStore
					 where
						a.AccountNumber.Contains(currentText) ||
						a.Name.Contains(currentText)
					 select a;
			return q1.ToList();
		}

		Task OnSelected(Account selected)
		{
			return SelectedChanged.InvokeAsync(selected);
		}
	}
}
