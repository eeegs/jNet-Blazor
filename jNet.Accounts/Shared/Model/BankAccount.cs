using System;

namespace jNet.Accounts.Shared.Model
{
	public class BankAccount : BaseData, IHaveKey<Guid>
	{
		public Guid Key { get; init; }
		public string BankAccountNumber { get; set; } = "";
		public BankAccount(Account account)
		{
			Key = account.Key;
			account.Specialisation = "Bank";
		}

		public BankAccount() { }
		public string Bank { get; set; } = "";
		public string BSB { get; set; } = "";
	}
}
