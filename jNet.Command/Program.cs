using jNet.Data;
using m = jNet.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;

namespace jNet.Command
{
	class Program
	{
		static void Main2(string[] args)
		{
			var o = new DbContextOptionsBuilder<AccountingDb>()
				.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
				.Options;

			var names = new[] { "Taurei", "The Design Spot", "Play Things & Swings", "One North", "Integral Consulting", "JarraNet", "StratGrid" };

			foreach (var n in names)
			{
				using (var db = new AccountingDb(o))
				{
					MakeAccounts(db, n);
				}
			}
		}

		private static void MakeAccounts(AccountingDb db, string name)
		{
			var b = MakeBusiness(db, name);
			db.BusinessId = b.Id;

			var ba = new m.Account(name, m.Account.Default) { IsSummaryAccount = true };

			var a =  new m.Account("Assets", ba, "Tangible and intangible items that the company owns that have value.") { IsSummaryAccount = true, Type = m.AccountType.Asset };
			var l = new m.Account("Liabilities", ba, "Money that the company owes to others.") { IsSummaryAccount = true, Type = m.AccountType.Liability };
			var q = new m.Account("Equity", ba, "That portion of the total assets that the owners or stockholders of the company fully own; have paid for outright.") { IsSummaryAccount = true, Type = m.AccountType.Equity };
			var r = new m.Account("Revenues", ba, "Money the company earns from its sales of products or services, and interest and dividends earned from marketable securities.") { IsSummaryAccount = true, Type = m.AccountType.Revenue };
			var x = new m.Account("Expences", ba, "Money the company spends to produce the goods or services that it sells.") { IsSummaryAccount = true, Type = m.AccountType.Expence };
			db.Accounts.AddRange(a,l,q,r,x);

			var ca = new m.Account("Current Assets", a) { IsSummaryAccount = true };
			var bk = new m.Account("Bank Accounts", ca) { IsSummaryAccount = true };
			for(int i = 0;i<4;i++)
			{
				db.Accounts.Add(new m.Account($"Bank account #{i + 1}", bk));
			}
			foreach (string s in new [] { "Cash on hand", "Petty Cash", "Debtors", "Stock on hand" })
			{
				db.Accounts.Add(new m.Account(s, ca));
			}

			b.Detail.ABN = 2498345633;
			b.Detail.ACN = 98345633;
			db.SaveChanges();
		}

		static m.Business MakeBusiness(AccountingDb db, string name)
		{
			var b = m.Business.Create(name);
			db.Businesses.Add(b);
			db.SaveChanges();
			return b;
		}


		static void Main(string[] args)
		{
			var cd = new Charts.Chart();
			var rnd = new Random();

			cd.AddSeries("Series1", Enumerable.Range(0, 10).Select(q => new Charts.PointData<int, DateTime>(q, DateTime.Now.AddSeconds(rnd.Next()))));
			cd.Format = new Charts.Format("00.0") { Color = Color.Red };
			cd[0].Format = new Charts.Format("yyyy-MM-dd HH:ss");
			foreach(var series in cd)
			{
				Console.WriteLine(cd.Title);
				foreach (var r in cd.Range)
				{
					Console.WriteLine($"\t{r}");
				}
				Console.WriteLine(series.Name);
				foreach (var v in series)
				{
					Console.WriteLine($"\t\t{v.RangeValue}, {v.SeriesValue}");
				}
			}
		}
	}
}
