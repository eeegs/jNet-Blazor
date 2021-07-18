using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	public partial class Draggable: BaseDraggable
	{
		private double dX;
		private double dY;

		[Parameter]
		public EventCallback<Vector2> PositionChanged { get; set; }

		protected override Task DragStart(PointerEventArgs e)
		{
			dX = e.ClientX - Position.X;
			dY = e.ClientY - Position.Y;
			return Task.CompletedTask;
		}

		protected override Task Drag(PointerEventArgs e)
		{
			return PositionChanged.InvokeAsync(new Vector2((float)(e.ClientX - dX), (float)(e.ClientY - dY)));
		}

		protected override Task DragEnd(PointerEventArgs e) => Drag(e);
	}
}
