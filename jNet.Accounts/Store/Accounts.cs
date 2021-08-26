using jNet.Accounts.Code;
using jNet.Accounts.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace jNet.Accounts.Store
{
	public class Accounts : Store<Account, Guid>
	{
		public Accounts(HttpClient httpClient) : base(httpClient) { }

		public record Summary(Account Account, DateTime? LastEntry, decimal Value);

		public IEnumerable<Account> ChildrenOf(Account? parent = null)
		{
			var parentKey = parent?.Key ?? Guid.Empty;
			var result = Where(q => q.ParentKey == parentKey).ToList();
			return result;
		}

		public bool AnyChildren(Account? parent = null)
		{
			var parentKey = parent?.Key ?? Guid.Empty;
			var result = Any(q => q.ParentKey == parentKey);
			return result;
		}

		public Account? ParentOf(Account child)
		{
			if (child.ParentKey == Guid.Empty)
			{
				return default;
			}
			return this[child.ParentKey];
		}

		public IEnumerable<(Account Account, int Depth)> GetFlat(Guid? parentKey = null, int depth = 0)
		{
			parentKey ??= Guid.Empty;
			foreach (var a in Where(q => q.ParentKey == parentKey).OrderBy(q => q.AccountNumber))
			{
				yield return (a, depth);
				foreach (var c in GetFlat(a.Key, depth + 1))
				{
					yield return c;
				}
			}
		}
	}
}

/*
			result = SumChildren(result, null).ToList();
			return result;

			IEnumerable<Summary> SumChildren(IEnumerable<Summary> summaries, Summary? current)
			{
				foreach (var child in summaries.Where(q => q.Account.ParentKey == (current?.Account.Key ?? Guid.Empty)))
				{
					decimal sum = 0;
					foreach (var s in SumChildren(summaries, child))
					{
						sum += s.Value;
						yield return s;
					}
					yield return child with { Value = child.Value + sum };
				}
			}
		}
*/