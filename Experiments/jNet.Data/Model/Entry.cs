using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Entry))]
	public class Entry : BaseData2
	{
		public Entry(Entity entity, Transaction transaction, Account account, long amount): base(entity.Name)
		{
			//Entity = entity;
			Transaction = transaction;
			Account = account;
			Amount = amount;
		}

		[Column(TypeName = "money")]
		public long Amount { get; init; }
		public long TransactionId { get; set; }
		public Transaction Transaction { get; set; }
		public long AccountId { get; set; }
		public Account Account { get; init; }
		//public long EntityId { get; set; }
		//public Entity Entity { get; set; }


		// these are provide to make reflection work when building a datacontext.
		private Entry(long amount) : base("NullTransaction")
		{
			//Entity = default!;
			Transaction = default!;
			Account = default!;
			Amount = amount;
		}
		internal static Transaction NullTransaction => new Transaction("NullTransaction");
	}
}
