using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public class PanelContent : ComponentBase, IDisposable
	{
		static long ids = 0;

		[Inject] public PanelService? PanelService { get; set; }
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter] public Panel.Side Location { get; set; }

		readonly long id = ++ids;

		public void Dispose()
		{
			PanelService?.Remove(id);
		}

		protected override Task OnParametersSetAsync()
		{
			if (ChildContent is not null)
			{
				PanelService?.Add(id, ChildContent, Location);
			}
			return base.OnParametersSetAsync();
		}

		public void Open() => PanelService?.Open();
		public void Close() => PanelService?.Close();
	}
}
