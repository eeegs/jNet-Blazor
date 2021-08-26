using System;

namespace jNet.Accounts.Shared.Model
{
	public class Entry
	{
		public Entry()
		{
			TaxTypeKey = "";
		}

		public Entry(Guid accountKey, decimal amount, string taxTypeKey)
		{
			AccountKey = accountKey;
			Amount = amount;
			TaxTypeKey = taxTypeKey;
		}
		public EntryType Type { get; set; }

		public decimal Amount { get; init; }
		public Guid AccountKey { get; init; }
		public string? Note { get; set; }
		public string TaxTypeKey { get; set; }

	}
}
