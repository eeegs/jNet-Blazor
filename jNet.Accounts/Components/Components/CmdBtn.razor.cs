
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace jNet.Accounts.Components
{
	public partial class CmdBtn
	{
		[Parameter] public ICommand Command { get; set; } = Components.Command.Noop;
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object>? AdditionalAttributes { get; set; }
		[Parameter] public Appearance? Appearance { get; set; }
		[Parameter] public bool? Autofocus { get; set; }
		[Parameter] public object? Context { get; set; }

		protected override Task OnParametersSetAsync()
		{
			Command.CanExecuteChanged -= Command_StateChanged;
			Command.CanExecuteChanged += Command_StateChanged;
			return base.OnParametersSetAsync();
		}

		private void Command_StateChanged(object? sender, EventArgs e) => StateHasChanged();

		void Clicked() => Command.Execute(Context);

		public void Dispose() => Command.CanExecuteChanged -= Command_StateChanged;
	}
}
