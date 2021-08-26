using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public class PanelService
	{
		static EventArgs eventArgs = new();
		public event EventHandler? StateChanged;
		protected void RaiseStateChanged() => StateChanged?.Invoke(this, eventArgs);
		public void Add(long id, RenderFragment fragment, Panel.Side location)
		{
			lastID = id;
			Content = fragment;
			Location = location;
			IsOpen = true;
			RaiseStateChanged();
		}
		public void Remove(long id)
		{
			if (id == lastID)
			{
				Content = null;
				IsOpen = false;
				RaiseStateChanged();
			}
		}

		long lastID;
		public RenderFragment? Content { get; private set; }
		public Panel.Side Location { get; private set; }
		public bool IsOpen { get; private set; }

		public void Open()
		{
			if (!IsOpen)
			{
				IsOpen = true;
				RaiseStateChanged();
			}
		}

		public void Close()
		{
			if (IsOpen)
			{
				IsOpen = false;
				RaiseStateChanged();
			}
		}
	}
}
