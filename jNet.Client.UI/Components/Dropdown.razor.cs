using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Client.UI.Components
{
	public partial class Dropdown<T>
	{
		private bool isopen = false;
		[Parameter] public RenderFragment Editor { get; set; } = emptyFrag;
		[Parameter] public RenderFragment ButtonOpen { get; set; } = down;
		[Parameter] public RenderFragment ButtonClose { get; set; } = up;
		[Parameter] public RenderFragment DropDown { get; set; } = emptyFrag;

		void OnButton(PointerEventArgs args)
		{
			isopen = !isopen;
		}

		void Close(FocusEventArgs args)
		{
			isopen = false;
		}

		protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out T result, [NotNullWhen(false)] out string? validationErrorMessage)
		{
			throw new NotImplementedException();
		}
	}
}