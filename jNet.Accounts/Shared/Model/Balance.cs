using System;

namespace jNet.Accounts.Shared.Model
{
	public class Balance : IHaveKey
	{
		public string Key { get; init; } = string.Empty;
		public int FY { get; set; }
		public decimal Value { get; set; }
	}
}
