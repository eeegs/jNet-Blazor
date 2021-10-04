using System;

namespace jNet.Accounts.Shared.Model
{
	public class Entry
	{
		public Entry()
		{
			TaxTypeKey = "";
		}

		public Entry(string accountKey, decimal amount, string taxTypeKey)
		{
			AccountKey = accountKey;
			Amount = amount;
			TaxTypeKey = taxTypeKey;
		}
		public EntryType Type { get; set; }

		public decimal Amount { get; init; }
		public string AccountKey { get; init; } = string.Empty;
		public string? Note { get; set; }
		public string TaxTypeKey { get; set; }

	}
}
