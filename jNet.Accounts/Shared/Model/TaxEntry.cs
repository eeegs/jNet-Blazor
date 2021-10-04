using System;

namespace jNet.Accounts.Shared.Model
{
	public class TaxEntry : IHaveKey, IHaveName
	{
		public TaxEntry() { }

		public TaxEntry(string name, Account creditAccount, Account debitAccount)
		{
			Key = name;
			CreditAccountKey = creditAccount.Key;
			DebitAccountKey = debitAccount.Key;
		}

		public string Key { get; init; } = "";
		public string Name => Key;
		public decimal Formula { get; set; } = 0.1m;
		public string DebitAccountKey { get; set; } = string.Empty;
		public string CreditAccountKey { get; set; } = string.Empty;
	}
}
