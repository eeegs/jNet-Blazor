using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public enum Position
	{
		Left,
		Centre,
		Right
	}

	public class StatusStrip : ComponentBase, IDisposable
	{
		static long ids = 0;

		[Inject] public StatusBarService? StatusBarService { get; set; }
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter] public Position Position { get; set; }

		readonly long id = ++ids;

		public void Dispose()
		{
			StatusBarService?.Remove(id);
		}

		protected override Task OnParametersSetAsync()
		{
			if(ChildContent is not null)
			{
				StatusBarService?.Add(id, ChildContent, Position);
			}
			return base.OnParametersSetAsync();
		}
	}
}
