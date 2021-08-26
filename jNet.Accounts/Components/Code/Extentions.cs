using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public static class Extentions
	{
		public static string toHex(this Color me)
		{
			return $"#{me.R:x2}{me.G:x2}{me.B:x2}{me.A:x2}";
		}

		public static Tv? Try<Tk, Tv>(this IDictionary<Tk, Tv> me, Tk key, Tv? or = default)
		{
			var value = or;
			me.TryGetValue(key, out value);
			return value;
		}
	}
}
