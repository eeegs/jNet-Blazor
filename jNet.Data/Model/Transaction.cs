using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Transaction))]
	public class Transaction: BaseData3
	{
		public Transaction(string name) : base(name)
		{
		}

		//public long Id { get; init; }
		//public string? Description { get; set; }
		public DateTime TransactionDate { get; init; } = DateTime.UtcNow.Date;
		public IList<Entry> Entries { get; } = new List<Entry>();

		public class Config : IEntityTypeConfiguration<Transaction>
		{
			public void Configure(EntityTypeBuilder<Transaction> builder)
			{
				builder
					.Property(m => m.ModifiedDate)
						.HasDefaultValueSql("getdate()");
			}
		}
	}
}
