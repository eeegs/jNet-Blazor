using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class Panel : ComponentBase, IDisposable
	{
		public enum Side
		{
			Centre,
			Left,
			Top,
			Right,
			Bottom
		};

		static Dictionary<Side, string> flex_direction = new()
		{
			[Side.Bottom] = "column",
			[Side.Top] = "column-reverse",
			[Side.Right] = "row",
			[Side.Left] = "row-reverse",
		};

		[Inject]
		PanelService? PanelService { get; set; }

		protected override Task OnInitializedAsync()
		{
			PanelService!.StateChanged += StateChanged;
			return base.OnInitializedAsync();
		}

		private void StateChanged(object? sender, EventArgs e) => StateHasChanged();

		public void Dispose()
		{
			PanelService!.StateChanged -= StateChanged;
		}

		void Close(FocusEventArgs args)
		{
			PanelService?.Close();
		}
	}
}
