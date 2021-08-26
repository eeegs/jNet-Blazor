using System;

namespace jNet.Accounts.Shared.Model
{
	public class TaxEntry : IHaveKey<string>, IHaveName
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
		public Guid DebitAccountKey { get; set; }
		public Guid CreditAccountKey { get; set; }
	}
}
