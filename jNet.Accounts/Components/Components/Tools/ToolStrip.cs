using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public class ToolStrip : ComponentBase, IDisposable
	{
		static long ids = 0;

		[Inject] public ToolBarService? ToolBarService { get; set; }
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter] public int Priority { get; set; }

		readonly long id = ++ids;

		public void Dispose()
		{
			ToolBarService?.Remove(id);
		}

		protected override Task OnParametersSetAsync()
		{
			if(ChildContent is not null)
			{
				ToolBarService?.Add(id, ChildContent, Priority);
			}
			return base.OnParametersSetAsync();
		}
	}
}
