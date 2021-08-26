using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Components
{
    public class ToolBarService 
	{
		static EventArgs eventArgs = new();
		public event EventHandler? StateChanged;
		protected void RaiseStateChanged() => StateChanged?.Invoke(this, eventArgs);

		private record ToolStripGroup(RenderFragment Fragment, int Position);

		readonly Dictionary<long, ToolStripGroup> entries = new();

		public void Add(long id, RenderFragment fragment, int position)
		{
			entries[id] = new(fragment, position);
			RaiseStateChanged();
		}

		public void Remove(long id)
		{
			entries.Remove(id);
			RaiseStateChanged();
		}

		public IEnumerable<KeyValuePair<long, RenderFragment>> Entries ()
		{
			var sorted = entries.OrderBy(q => q.Value.Position).Select(q => KeyValuePair.Create(q.Key, q.Value.Fragment));
			return sorted;
		}
	}
}
