using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public class MenuStrip : ComponentBase, IDisposable
	{
		static long ids = 0;
		static string defaultMenu = "Main";

		[Inject] public MenuBarService? MenuBarService { get; set; }
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter] public int Priority { get; set; }
		[Parameter] public string Menu { get; set; } = defaultMenu;

		readonly long id = ++ids;

		public void Dispose()
		{
			MenuBarService?.Remove(id, Menu);
		}

		protected override Task OnParametersSetAsync()
		{
			if(ChildContent is not null)
			{
				MenuBarService?.Add(id, Menu, ChildContent, Priority);
			}
			return base.OnParametersSetAsync();
		}
	}
}
