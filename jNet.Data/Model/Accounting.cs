using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Data.Model
{
	public class Transaction
	{
		public long Id { get; init; }
		public string? Description { get; set; }
		public DateTime TransactionDate { get; init; } = DateTime.UtcNow.Date;
		public DateTime ModifiedDate { get; init; }
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

	public class Entry
	{
		private Entry(decimal amount) : this(NullTransaction, Account.RootAccount, amount)
		{
		}

		public Entry(Transaction transaction, Account account, decimal amount)
		{
			Transaction = transaction;
			Account = account;
			Amount = amount;
		}

		[Column(TypeName = "money")]
		public decimal Amount { get; init; }
		public long Id { get; set; }
		public long TransactionId { get; set; }
		public Transaction Transaction { get; init; }
		public string? Description { get; set; }
		public long AccountId { get; set; }
		public Account Account { get; init; }
		internal static Transaction NullTransaction => new Transaction { Description = "The Null Transaction" };
	}

	public class Account
	{
		public Account(string name, Account parent, string? description = null)
		{
			Name = name;
			Description = description;
			Parent = parent;
		}

		public long Id { get; init; }
		public string Name { get; set; }
		public string? Description { get; set; }

		[Column(TypeName = "nvarchar(10)")]
		public AccountType Type { get; init; }
		public int Direction { get; init; }
		public string? AccountNumber { get; init; }
		public long ParentId { get; set; }
		public Account Parent { get; set; }
		public bool IsSummaryAccount { get; set; }
		public IList<Account> Children { get; } = new List<Account>();
		internal static Account RootAccount { get; } = new Account();

		private Account()
		{
			Name = "";
			Id = -1;
			Description = "The root account";
			ParentId = -1;
			//Parent = this;
		}

		public class Config : IEntityTypeConfiguration<Account>
		{
			public void Configure(EntityTypeBuilder<Account> builder)
			{
				builder
					.HasOne(p => p.Parent)
					.WithMany(p => p.Children)
					.OnDelete(DeleteBehavior.Restrict);
				builder
					.HasData(
						RootAccount
					);
			}
		}
	}

	public enum AccountType : byte
	{
		[Description("Tangible and intangible items that the company owns that have value.")]
		Asset = 1,
		[Description("Money that the company owes to others.")]
		Liability,
		[Description("That portion of the total assets that the owners or stockholders of the company fully own; have paid for outright.")]
		Equity,
		[Description("Money the company earns from its sales of products or services, and interest and dividends earned from marketable securities.")]
		Revenue,
		[Description("Money the company spends to produce the goods or services that it sells.")]
		Expence,
	}

	public class CompanyDetail
	{
		public CompanyDetail(string name)
		{
			Name = name;
		}

		public long Id { get; init; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public long ACN { get; set; }
		public long ABN { get; set; }

		public Business? Business { get; init; }
		public Supplier? Supplier { get; init; }
		public Customer? Customer { get; init; }

		public class Config : IEntityTypeConfiguration<CompanyDetail>
		{
			public void Configure(EntityTypeBuilder<CompanyDetail> builder)
			{
				builder.HasKey(q => q.Id);
				builder
					.HasOne(q => q.Business)
					.WithOne(q => q.Detail)
					.HasForeignKey<Business>(q => q.Id)
					.IsRequired();
			}
		}
	}

	public class Customer
	{
		public long Id { get; set; }
	}

	public class Supplier
	{
		public long Id { get; set; }
	}

	public class Business
	{
		public Business(string name)
		{
			Detail = new CompanyDetail(name);
			Account = new Account(name, Account.RootAccount);
			Name = name;
		}

		public long Id { get; init; }
		public string Name { get; init; }
		public CompanyDetail Detail { get; init; }
		public Account Account { get; init; }
		public long AccountId { get; init; }

		public class Config : IEntityTypeConfiguration<Business>
		{
			public void Configure(EntityTypeBuilder<Business> builder)
			{
				//builder.HasKey(q => q.Id);
				//builder
				//	.HasOne(q => q.Detail)
				//	.WithOne(q => q.Business)
				//	.HasForeignKey<CompanyDetail>(q => q.Id)
				//	.IsRequired();
			}
		}
	}
}
