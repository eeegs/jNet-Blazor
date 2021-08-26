using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;


namespace jNet.Accounts.Components
{
	public partial class AlertBar
	{
		[Inject]
		public AlertBarService? AlertBarService { get; set; }

		protected override Task OnInitializedAsync()
		{
			AlertBarService!.StateChanged += StateChanged;
			return base.OnInitializedAsync();
		}

		private void StateChanged(object? sender, EventArgs e) => StateHasChanged();

		public void Dispose()
		{
			AlertBarService!.StateChanged -= StateChanged;
		}
	}
}
