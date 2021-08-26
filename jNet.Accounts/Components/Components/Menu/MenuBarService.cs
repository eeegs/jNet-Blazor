using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Components
{
	public class MenuBarService
	{
		static EventArgs eventArgs = new();
		public event EventHandler? StateChanged;
		protected void RaiseStateChanged() => StateChanged?.Invoke(this, eventArgs);
		private record ToolStripGroup(RenderFragment Fragment, int Position);
		readonly Dictionary<string, Dictionary<long, ToolStripGroup>> menus = new();

		public void Add(long id, string menu, RenderFragment fragment, int position)
		{
			var entries = GetEntries(menu);
			entries[id] = new(fragment, position);
			RaiseStateChanged();
		}

		public void Remove(long id, string menu)
		{
			var entries = GetEntries(menu);
			entries.Remove(id);
			if (entries.Count == 0)
			{
				menus.Remove(menu);
			}
			RaiseStateChanged();
		}

		public IEnumerable<string> Menus()
		{
			return menus.Keys;  //Todo: figure out how to order the menus
		}

		public IEnumerable<KeyValuePair<long, RenderFragment>> Entries(string menu)
		{
			var entries = GetEntries(menu);
			var sorted = entries.OrderBy(q => q.Value.Position).Select(q => KeyValuePair.Create(q.Key, q.Value.Fragment));
			return sorted;
		}

		private Dictionary<long, ToolStripGroup> GetEntries(string menu)
		{
			if (!menus.TryGetValue(menu, out var entries))
			{
				entries = new();
				menus[menu] = entries;
			}
			return entries;
		}
	}
}
