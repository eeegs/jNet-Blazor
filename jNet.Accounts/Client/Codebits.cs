using jNet.Accounts.Shared.Model;
using System;
using System.Linq;

namespace jNet.Accounts.Client
{
	public static class Codebits
	{
		public static void CreateAccounts(Store.Accounts astore, Store.TaxEntries xstore, Store.Transactions tstore)
		{
			var a = new Account("Assets", null, EntryType.Debit) { Type = AccountType.Asset, AccountNumber = "1000" };
			var ac = new Account("Current Assets", a) { Type = AccountType.Asset, AccountNumber = "1100" };
			var acb = new Account("Bank", ac) { Type = AccountType.Asset, AccountNumber = "1110" };
			var acc = new Account("Cash on hand", ac) { Type = AccountType.Asset, AccountNumber = "1120" };
			var acp = new Account("Petty Cash", ac) { Type = AccountType.Asset, AccountNumber = "1130" };
			var acd = new Account("Debtors", ac) { Type = AccountType.Asset, AccountNumber = "1140" };
			var acs = new Account("Stock on hand", ac) { Type = AccountType.Asset, AccountNumber = "1150" };
			var acg = new Account("GST Paid", ac) { Type = AccountType.Asset, AccountNumber = "1160" };
			var an = new Account("Non-current Assets", a) { Type = AccountType.Asset, AccountNumber = "1200" };
			var anf = new Account("Furniture and Fittings", an) { Type = AccountType.Asset, AccountNumber = "1210" };
			var ano = new Account("Office Equipment", an) { Type = AccountType.Asset, AccountNumber = "1220" };
			var anc = new Account("Company Car", an) { Type = AccountType.Asset, AccountNumber = "1230" };
			var anp = new Account("Plant and Machinery", an) { Type = AccountType.Asset, AccountNumber = "1240" };
			var l = new Account("Liabilities", null, EntryType.Credit) { Type = AccountType.Liability, AccountNumber = "2000" };
			var lc = new Account("Current liabilities", l) { Type = AccountType.Liability, AccountNumber = "2100" };
			var lcb = new Account("Bank Overdraft", lc) { Type = AccountType.Liability, AccountNumber = "2110" };
			var lcc = new Account("Creditors", lc) { Type = AccountType.Liability, AccountNumber = "2120" };
			var lcg = new Account("GST Received", lc) { Type = AccountType.Liability, AccountNumber = "2130" };
			var lcp = new Account("PAYE tax payable", lc) { Type = AccountType.Liability, AccountNumber = "2140" };
			var lcl = new Account("Loan from Director", lc) { Type = AccountType.Liability, AccountNumber = "2150" };
			var ln = new Account("Non-current Liabilities", l) { Type = AccountType.Liability, AccountNumber = "2200" };
			var lnc = new Account("Company Car Loan", ln) { Type = AccountType.Liability, AccountNumber = "2210" };
			var lne = new Account("Equipment Loan", ln) { Type = AccountType.Liability, AccountNumber = "2220" };
			var lnl = new Account("Long Term Loan", ln) { Type = AccountType.Liability, AccountNumber = "2230" };
			var e = new Account("Equity", null, EntryType.Credit) { Type = AccountType.Equity, AccountNumber = "3000" };
			var eo = new Account("Owners Capital", e) { Type = AccountType.Equity, AccountNumber = "3110" };
			var er = new Account("Retained Earnings", e) { Type = AccountType.Equity, AccountNumber = "3220" };
			var ep = new Account("Current Profit", e) { Type = AccountType.Equity, AccountNumber = "3300" };
			var r = new Account("Revenue", null, EntryType.Credit) { Type = AccountType.Revenue, AccountNumber = "4000" };
			var rl = new Account("Licences", r) { Type = AccountType.Revenue, AccountNumber = "4100" };
			var c = new Account("Cost of Goods Sold", null, EntryType.Debit) { Type = AccountType.CoGS, AccountNumber = "5000" };
			var x = new Account("Expenses", null, EntryType.Debit) { Type = AccountType.Expense, AccountNumber = "6000" };
			var xx = new Account("Fixed", x) { Type = AccountType.Expense, AccountNumber = "6100" };
			var xxr = new Account("Rent", xx) { Type = AccountType.Expense, AccountNumber = "6110" };
			var xxw = new Account("Wages/salaries", xx) { Type = AccountType.Expense, AccountNumber = "6120" };
			var xxc = new Account("Company car expenses", xx) { Type = AccountType.Expense, AccountNumber = "6130" };
			var xxs = new Account("Software Subscription", xx) { Type = AccountType.Expense, AccountNumber = "6140" };
			var xxu = new Account("Utilities", xx) { Type = AccountType.Expense, AccountNumber = "6150" };
			var xv = new Account("Variable", x) { Type = AccountType.Expense, AccountNumber = "6200" };
			var xva = new Account("Advertising", xv) { Type = AccountType.Expense, AccountNumber = "6210" };
			var xvp = new Account("Postage", xv) { Type = AccountType.Expense, AccountNumber = "6220" };
			var xa = new Account("Accounting fees", x) { Type = AccountType.Expense, AccountNumber = "6300" };
			var xf = new Account("Farm Expence", x) { Type = AccountType.Expense, AccountNumber = "6400" };
			var xfl = new Account("Livestock Expence", xf) { Type = AccountType.Expense, AccountNumber = "6410" };
			var xfc = new Account("Instant Write-off Capital", xf) { Type = AccountType.Expense, AccountNumber = "6420" };
			var xfe = new Account("Farm Equipmant", xf) { Type = AccountType.Expense, AccountNumber = "6430" };
			var xfem = new Account("Equipment Maintenance", xfe) { Type = AccountType.Expense, AccountNumber = "6431" };
			var xfec = new Account("Equipment Consumables", xfe) { Type = AccountType.Expense, AccountNumber = "6432" };
			var xfex = new Account("Equipment Misc", xfe) { Type = AccountType.Expense, AccountNumber = "6433" };
			var xfeh = new Account("Equipment Hardware", xfe) { Type = AccountType.Expense, AccountNumber = "6434" };
			var xfs = new Account("Farm Sheds", xf) { Type = AccountType.Expense, AccountNumber = "6440" };
			var xfsr = new Account("Farm Shed Repair", xfs) { Type = AccountType.Expense, AccountNumber = "6441" };
			var xfsm = new Account("Farm Shed Maintenance", xfs) { Type = AccountType.Expense, AccountNumber = "6442" };
			var i = new Account("Other Income", null, EntryType.Credit) { Type = AccountType.OtherRevenue, AccountNumber = "8000" };
			var o = new Account("Other Expense", null, EntryType.Debit) { Type = AccountType.OtherExpense, AccountNumber = "9000" };
			var ob = new Account("Bank", o) { Type = AccountType.OtherExpense, AccountNumber = "9100" };
			var obf = new Account("Fees", ob) { Type = AccountType.OtherExpense, AccountNumber = "9110" };
			var obi = new Account("Interest", ob) { Type = AccountType.OtherExpense, AccountNumber = "9120" };
			var oa = new Account("ASIC", o) { Type = AccountType.OtherExpense, AccountNumber = "9200" };

			var banks = Enumerable.Range(1, 5).Select(q => new Account($"Bank account #{q}", acb));

			astore.Set(a, ac, acb, acc, acp, acd, acs, acg, an, anf, ano, anc, anp, l, lc, lcb, lcc, lcg, lcp, lcl, ln, lnc, lne, lnl, e, eo, er, ep, r, rl, c, x, xx, xxr, xxw, xxc, xxs, xxu, xv, xva, xvp, xa, xf, xfl, xfc, xfe, xfem, xfec, xfex, xfeh, xfs, xfsr, xfsm, i, o, ob, obf, obi, oa);
			astore.Set(banks);

			var gst = new TaxEntry("GST", acg, lcg) { Formula = 0.1m };
			var none = new TaxEntry("NONE", acg, lcg) { Formula = 0.0m };
			xstore.Set(gst);

			foreach (var aaaa in astore)
			{
				aaaa.TaxTypeKey = none.Key;
			}

			foreach (var aaaa in astore.Where(q => q.Type == AccountType.Revenue))
			{
				aaaa.TaxTypeKey = gst.Key;
			}
			foreach (var aaaa in astore.Where(q => q.Type == AccountType.CoGS))
			{
				aaaa.TaxTypeKey = gst.Key;
			}
			foreach (var aaaa in astore.Where(q => q.Type == AccountType.Expense))
			{
				aaaa.TaxTypeKey = gst.Key;
			}

			a.Description = "Tangible and intangible items that the company owns that have value.";
			l.Description = "Money that the company owes to others.";
			e.Description = "That portion of the total assets that the owners or stockholders of the company fully own; have paid for outright.";
			r.Description = "Money the company earns from its sales of products or services, and interest and dividends earned from marketable securities.";
			x.Description = "Money the company spends to produce the goods or services that it sells.";

			var txs = new[] {
					Transaction.ReceivePayment("Initial loan", DateTime.Parse("2018-10-08"), 630.0000m, acc, lcl, none, none),
					Transaction.ReceivePayment("Establishment", DateTime.Parse("2018-10-08"), 630.0000m, xa, acc, none, none),
					Transaction.ReceivePayment("Initial Shares", DateTime.Parse("2018-10-09"), 10.0000m, acc, eo, none, none),
					Transaction.ReceivePayment("Company Statement", DateTime.Parse("2019-10-09"), 267.0000m, oa, acc, none, none),
					Transaction.ReceivePayment("Another loan", DateTime.Parse("2019-12-01"), 32414.6400m, acc, lcl, none, none),
					Transaction.ReceivePayment("Chainsaw", DateTime.Parse("2019-12-24"), 271.8200m, xfex, acc, gst, none),
					Transaction.ReceivePayment("Farm electricity - 25% of total", DateTime.Parse("2020-04-17"), 527773, xxu, acc, gst, none),
					Transaction.ReceivePayment("Tractor", DateTime.Parse("2020-06-11"), 287636400, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Shears and lopper", DateTime.Parse("2020-06-27"), 1368909, xfex, acc, gst, none),
					Transaction.ReceivePayment("Saw blade", DateTime.Parse("2020-07-06"), 581818, xfex, acc, gst, none),
					Transaction.ReceivePayment("Concrete equipment hire", DateTime.Parse("2020-07-07"), 672727, xfsr, acc, gst, none),
					Transaction.ReceivePayment("Concrete", DateTime.Parse("2020-07-08"), 6828000, xfsr, acc, gst, none),
					Transaction.ReceivePayment("Farm electricity - 25% of total", DateTime.Parse("2020-07-13"), 1206841, xxu, acc, gst, none),
					Transaction.ReceivePayment("Generator", DateTime.Parse("2020-07-18"), 7718182, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Masonary bit", DateTime.Parse("2020-07-18"), 129545, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Farm Office Repair Hardware", DateTime.Parse("2020-07-18"), 8598818, xfsr, acc, gst, none),
					Transaction.ReceivePayment("Ladder", DateTime.Parse("2020-08-23"), 6558900, xfex, acc, gst, none),
					Transaction.ReceivePayment("Farm Office Paint", DateTime.Parse("2020-08-31"), 744545, xfsm, acc, gst, none),
					Transaction.ReceivePayment("Bolts etc", DateTime.Parse("2020-09-05"), 3059200, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Farm Office A/C", DateTime.Parse("2020-09-15"), 42000000, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Farm Office replacment toilet", DateTime.Parse("2020-10-03"), 6081818, xfex, acc, gst, none),
					Transaction.ReceivePayment("Idler", DateTime.Parse("2020-10-08"), 351600, xfec, acc, gst, none),
					Transaction.ReceivePayment("Brush cutter blade/line", DateTime.Parse("2020-10-15"), 390545, xfec, acc, gst, none),
					Transaction.ReceivePayment("Pri bar + Chainsaw chain", DateTime.Parse("2020-10-17"), 634182, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Farm electricity - 25% of total", DateTime.Parse("2020-10-22"), 1445886, xxu, acc, gst, none),
					Transaction.ReceivePayment("Shearing", DateTime.Parse("2020-11-13"), 1818200, xfl, acc, gst, none),
					Transaction.ReceivePayment("Mower parts", DateTime.Parse("2020-11-23"), 1015800, xfec, acc, gst, none),
					Transaction.ReceivePayment("Farm Office toilet installation", DateTime.Parse("2020-11-29"), 7500000, xfem, acc, gst, none),
					Transaction.ReceivePayment("Hydrolic Fluid", DateTime.Parse("2021-01-16"), 868200, xfec, acc, gst, none),
					Transaction.ReceivePayment("Company Statement", DateTime.Parse("2021-01-19"), 6130000, oa, acc, none, none),
					Transaction.ReceivePayment("Farm electricity - 25% of total", DateTime.Parse("2021-01-21"), 1077432, xxu, acc, gst, none),
					Transaction.ReceivePayment("Tractor Parts", DateTime.Parse("2021-02-10"), 62700, xfem, acc, gst, none),
					Transaction.ReceivePayment("V-Belt", DateTime.Parse("2021-03-01"), 686800, xfec, acc, gst, none),
					Transaction.ReceivePayment("Farm Septic - 50%", DateTime.Parse("2021-03-03"), 3636364, xxu, acc, gst, none),
					Transaction.ReceivePayment("Farm Office replacment toilet - moulding", DateTime.Parse("2021-03-12"), 916100, xfeh, acc, gst, none),
					Transaction.ReceivePayment("Mower parts", DateTime.Parse("2021-04-01"), 1476400, xfec, acc, gst, none),
					Transaction.ReceivePayment("Shed h/w repair and replacement", DateTime.Parse("2021-04-07"), 20000000, xfem, acc, gst, none),
					Transaction.ReceivePayment("Shed electrical repair", DateTime.Parse("2021-04-16"), 13400000, xfem, acc, gst, none),
					Transaction.ReceivePayment("Another loan", DateTime.Parse("2020-07-01"), 159536800, acc, lcl, none, none),
				 };

			tstore.Set(txs);
		}
	}
}
