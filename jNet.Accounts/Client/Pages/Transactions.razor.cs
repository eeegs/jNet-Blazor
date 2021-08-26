using jNet.Accounts.Components;
using jNet.Accounts.Shared.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Client.Pages
{
	public class ValidateTransactionEntries : IValidate<Entry>
	{
		public string? IsValid(Entry value)
		{
			return null;
		}

		InvalidList<Entry> IValidate<Entry>.IsValid(IEnumerable<Entry> values)
		{
			return new InvalidList<Entry>();
		}
	}

	public partial class Transactions : ComponentBase
	{
		ValidateTransactionEntries Validator = new();

		List<Entry> Entries = new();
	}
}
