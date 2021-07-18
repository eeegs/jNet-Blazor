using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace jNet.Data.Model
{
	[Table(nameof(Transaction))]
	public class Transaction : BaseData3
	{
		private DateTime transactionDate = DateTime.UtcNow.Date;

		public Transaction(string name) : base(name)
		{
		}

		public DateTime TransactionDate
		{
			get => transactionDate;
			init => transactionDate = value.ToUniversalTime();
		}
		public List<Entry> Entries { get; } = new List<Entry>();

		public class Config : IEntityTypeConfiguration<Transaction>
		{
			public void Configure(EntityTypeBuilder<Transaction> builder)
			{
				builder
					.Property(m => m.ModifiedDate)
						.HasDefaultValueSql("getutcdate()");
			}
		}

		//public static Transaction Create(string name, DateTime transactionDate, params Entry[] entries)
		//{
		//	if (entries.Sum(q => q.Amount) != 0)
		//	{
		//		throw new ArgumentException("I cannot create an unbalanced transaction. Transaction enties need to sum to zero.");
		//	}

		//	var t = new Transaction(name)
		//	{
		//		TransactionDate = transactionDate
		//	};
		//	t.Entries.AddRange(entries);
		//	return t;
		//}
	}
}
