using jNet.Data;
using m = jNet.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace jNet.Command
{
	class Program
	{
		static void Main(string[] args)
		{
			var o = new DbContextOptionsBuilder<AccountingDb>()
				.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
				.Options;

			using var db = new AccountingDb(o);

			var b = new m.Business("The Design Spot");
			//db.Entry(b.Account.Parent).State = EntityState.Unchanged;

			db.Accounts.AddRange(
				new m.Account("Assets", b.Account, "Tangible and intangible items that the company owns that have value.") { IsSummaryAccount = true, Type = m.AccountType.Asset },
				new m.Account("Liabilities", b.Account, "Money that the company owes to others.") { IsSummaryAccount = true, Type = m.AccountType.Liability },
				new m.Account("Equity", b.Account, "That portion of the total assets that the owners or stockholders of the company fully own; have paid for outright.") { IsSummaryAccount = true, Type = m.AccountType.Equity },
				new m.Account("Revenues", b.Account, "Money the company earns from its sales of products or services, and interest and dividends earned from marketable securities.") { IsSummaryAccount = true, Type = m.AccountType.Revenue },
				new m.Account("Expences", b.Account, "Money the company spends to produce the goods or services that it sells.") { IsSummaryAccount = true, Type = m.AccountType.Expence }
			);
			b.Detail.ABN = 98345633;
			db.Businesses.Add(b);
			db.SaveChanges();
		}
	}
}
