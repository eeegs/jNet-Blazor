using jNet.Accounts.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages
{
	public partial class Icons
	{
		static Dictionary<string, I> data;

		static Icons()
		{
			data = Enum.GetValues<I>().ToDictionary(q => q.ToString());
		}

		IEnumerable<I> list;

		public Icons()
		{
			list = data.Values.ToList();
		}

		Task SearchChanged(ChangeEventArgs e)
		{
			var txt = e.Value?.ToString() ?? "";
			return Task.Run(() =>
			{
				var qry = data.Values.AsEnumerable();
				if(!string.IsNullOrWhiteSpace(txt))
				{
					qry = data.Where(q => q.Key.Contains(txt)).Select(q=>q.Value);
					list = qry.ToList();
					return;
				}
				list = qry.ToList();
				StateHasChanged();
			});
		}
	}
}
