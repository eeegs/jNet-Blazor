using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Components
{
	public class StatusBarService
	{
		static EventArgs eventArgs = new();
		public event EventHandler? StateChanged;
		protected void RaiseStateChanged() => StateChanged?.Invoke(this, eventArgs);

		private record StatusStripGroup(RenderFragment Fragment, Position Position);

		readonly Dictionary<long, StatusStripGroup> leftEntries = new();
		readonly Dictionary<long, StatusStripGroup> centreEntries = new();
		readonly Dictionary<long, StatusStripGroup> rightEntries = new();

		public void Add(long id, RenderFragment fragment, Position position)
		{
			var entries = GetEntries(position);

			entries[id] = new(fragment, position);
			RaiseStateChanged();
		}

		public void Remove(long id)
		{
			leftEntries.Remove(id);
			centreEntries.Remove(id);
			rightEntries.Remove(id);
			RaiseStateChanged();
		}

		public IEnumerable<KeyValuePair<long, RenderFragment>> Entries(Position position)
		{
			Dictionary<long, StatusStripGroup> entries = GetEntries(position);

			var sorted = entries.Select(q => KeyValuePair.Create(q.Key, q.Value.Fragment));
			return sorted;
		}

		private Dictionary<long, StatusStripGroup> GetEntries(Position position)
		{
			return position switch
			{
				Position.Left => leftEntries,
				Position.Centre => centreEntries,
				Position.Right => rightEntries,
				_ => throw new InvalidOperationException("How did you get past the enum?")
			};
		}
	}
}
