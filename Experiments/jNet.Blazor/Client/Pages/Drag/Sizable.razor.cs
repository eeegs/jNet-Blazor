using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	public partial class Sizable : BaseDraggable
	{
		[Parameter] public EventCallback<double> RadiusChanged { get; set; }

		protected override Task Drag(PointerEventArgs e)
		{
			var r = Math.Sqrt((e.OffsetX - Position.X) * (e.OffsetX - Position.X) + (e.OffsetY - Position.Y) * (e.OffsetY - Position.Y));
			return RadiusChanged.InvokeAsync(r);
		}
	}
}
