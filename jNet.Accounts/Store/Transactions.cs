using jNet.Accounts.Code;
using jNet.Accounts.Shared.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace jNet.Accounts.Store
{
	public class Transactions : Store<Transaction, Guid>
	{
		public Transactions(HttpClient httpClient) : base(httpClient)
		{
		}

		public (int First, int Last) FYRange()
		{
			var res = this.HiLo(q => q.FY);
			return (res.Min, res.Max);
		}

		public int GetFY() => GetFY(DateTimeOffset.Now);
		public int GetFY(DateTimeOffset date) => date.LocalDateTime.AddMonths(6).Year;

		public Dictionary<Guid, decimal> GetBalances()
		{
			var q1 = from t in this
					 from e in t.Entries
					 group e by e.AccountKey into gp
					 select new { gp.Key, Sum = gp.Sum(q => q.Amount * (int)q.Type) };

			var result = q1.ToDictionary(q => q.Key, q => q.Sum);

			return result;
		}
	}
}
