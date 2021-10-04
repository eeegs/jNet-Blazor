using jNet.Accounts.Code;
using jNet.Accounts.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Store
{
	public class Balances : Store<Balance>
	{
		public Balances(HttpClient httpClient) : base(httpClient) { }

		public void CalculateForFY(int FY, IEnumerable<Transaction> transactions)
		{
			Delete(Where(q => q.FY == FY));

			var balances = from t in transactions
						   where t.FY == FY
						   from e in t.Entries
						   select new Balance { Key = e.AccountKey, Value = e.Amount * (int)e.Type, FY = FY };
			Set(balances);
		}

		public void CalculateAll(Transactions transactions)
		{
			var range = transactions.FYRange();
			for (int i = range.First; i <= range.Last; i++)
			{
				CalculateForFY(i, transactions);
			}
		}
	}
}
