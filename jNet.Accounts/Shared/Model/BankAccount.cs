using System;

namespace jNet.Accounts.Shared.Model
{
	public class BankAccount : BaseData, IHaveKey
	{
		public string Key { get; init; } = string.Empty;
		public string BankAccountNumber { get; set; } = string.Empty;
		public BankAccount(Account account)
		{
			Key = account.Key;
			account.Specialisation = "Bank";
		}

		public BankAccount() { }
		public string Bank { get; set; } = string.Empty;
		public string BSB { get; set; } = string.Empty;
	}
}
