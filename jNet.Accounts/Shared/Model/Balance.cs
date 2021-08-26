using System;

namespace jNet.Accounts.Shared.Model
{
	public class Balance : IHaveKey<Guid>
	{
		public Guid Key { get; init; }
		public int FY { get; set; }
		public decimal Value { get; set; }
	}
}
