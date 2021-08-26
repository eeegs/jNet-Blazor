using System;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Store
{
	public static class Extentions
	{
		public static (T Max, T Min) HiLo<Ts, T>(this IEnumerable<Ts> me, Func<Ts, T> selector)
			where T : notnull, IComparable<T>
		{
			if (!me.Any()) return (default!, default!);
			var min = selector(me.First());
			var max = selector(me.First());
			foreach (var i in me.Select(q => selector(q)))
			{
				if (min.CompareTo(i) > 0) min = i;
				if (max.CompareTo(i) < 0) max = i;
			}
			return (max, min);
		}
	}
}
