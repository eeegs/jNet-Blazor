using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public enum Status
	{
		Note,
		Warning,
		Error,
		Critical
	}

	public class AlertBarService
	{
		static long ids = 0;
		static EventArgs eventArgs = new();
		public event EventHandler? StateChanged;
		protected void RaiseStateChanged() => StateChanged?.Invoke(this, eventArgs);

		readonly Dictionary<long, (Status ststus, string msg, bool fade)> entries = new();

		public void Add(Status status, string message, int duration = 5000)
		{
			if (duration < 1000)
			{
				duration = 1000;
			}
			long id;
			lock (entries)
			{
				id = ++ids;
				entries[id] = (status, message, false);
			}
			Task.Run(async () =>
			{
				await Task.Delay(duration - 1000);
				lock (entries)
				{
					if (entries.TryGetValue(id, out var item))
					{
						entries[id] = (item.ststus, item.msg, true);
					}
				}
				RaiseStateChanged();
			});
			Task.Run(async () =>
			{
				await Task.Delay(duration);
				Remove(id);
			});
			RaiseStateChanged();
		}

		public void Remove(long id)
		{
			lock (entries)
			{
				entries.Remove(id);
			}
			RaiseStateChanged();
		}

		public IList<KeyValuePair<long, (Status status, string msg, bool fade)>> Entries()
		{
			lock (entries)
			{
				// remake the list so change don't cause threading issues on the rendering
				var sorted = entries.Select(q => KeyValuePair.Create(q.Key, q.Value)).ToList();
				return sorted;
			}
		}
	}
}
