using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Entry))]
	public class Entry : BaseData2
	{
		private Entry(decimal amount) : this("NullTransaction", NullTransaction, Account.Default, amount)
		{
		}

		public Entry(string	name, Transaction transaction, Account account, decimal amount): base(name)
		{
			Transaction = transaction;
			Account = account;
			Amount = amount;
		}

		//public long Id { get; init; }
		
		[Column(TypeName = "money")]
		public decimal Amount { get; init; }
		public long TransactionId { get; set; }
		public Transaction Transaction { get; init; }
		//public string? Description { get; set; }
		public long AccountId { get; set; }
		public Account Account { get; init; }
		internal static Transaction NullTransaction => new Transaction("NullTransaction") { Description = "The Null Transaction" };
	}
}
