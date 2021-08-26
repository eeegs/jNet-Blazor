using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class Dropdown
	{
		private bool isopen = false;
		[Parameter] public RenderFragment Editor { get; set; } = emptyFrag;
		[Parameter] public RenderFragment ButtonOpen { get; set; } = down;
		[Parameter] public RenderFragment ButtonClose { get; set; } = up;
		[Parameter] public RenderFragment Picker { get; set; } = emptyFrag;

		void OnButton(PointerEventArgs args)
		{
			isopen = !isopen;
		}

		public void Close()
		{
			isopen = false;
		}

		public void Open()
		{
			isopen = true;
		}
	}
}