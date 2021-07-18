using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace jNet.Data.Model
{
	[Table(nameof(Account))]
	public class Account : BaseData3
	{
		public Account(string name, Account parent, string? description = null) : base(name)
		{
			Description = description;
			Parent = parent;
			Type = parent.Type;
		}

		//public long Id { get; init; }
		//public string Name { get; set; }
		//public string? Description { get; set; }

		[Column(TypeName = "nvarchar(10)")]
		public AccountType Type { get; init; }
		public int Direction { get; init; }
		public string? AccountNumber { get; init; }
		public long ParentId { get; set; }
		public Account Parent { get; set; }
		public bool IsSummaryAccount { get; set; }
		public IList<Account> Children { get; } = new List<Account>();
		public static Account Default { get; } = new Account("Root")
		{
			Id = -1,
			Description = "The root account",
			ParentId = -1,
			AccountNumber = string.Empty,
			BusinessId = Business.Default.Id
		};

		private Account(string name) : base(name)
		{
			Parent = default!;
		}

		public class Config : IEntityTypeConfiguration<Account>
		{
			public void Configure(EntityTypeBuilder<Account> builder)
			{
				builder
					.HasOne<Business>()
					.WithMany()
					.IsRequired();
				builder
					.HasOne(p => p.Parent)
					.WithMany(p => p.Children)
					.OnDelete(DeleteBehavior.Restrict);
				builder
					.HasData(
						Default
					);
			}
		}
	}
}
