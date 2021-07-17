using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	public partial class Strength : Orbit
	{
		[Parameter] public double Scale { get; set; }
		[Parameter] public EventCallback<double> ScaleChanged { get; set; }

		private double A => Position.X + Scale * Distance * Math.Cos(Tau * Angle);
		private double B => Position.Y - Scale * Distance * Math.Sin(Tau * Angle);

		private bool Dragging = false;

		protected async override Task DragStart(PointerEventArgs e)
		{
			Dragging = true;
			await ScaleChanged.InvokeAsync(1);
			await base.DragStart(e);
		}

		protected async override Task Drag(PointerEventArgs e)
		{
			var r = Math.Sqrt((e.OffsetX - Position.X) * (e.OffsetX - Position.X) + (e.OffsetY - Position.Y) * (e.OffsetY - Position.Y));
			await ScaleChanged.InvokeAsync(r / Distance);
			await base.Drag(e);
		}

		protected override Task DragEnd(PointerEventArgs e)
		{
			Dragging = false;
			return base.DragEnd(e);
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
				var i = 0;
				builder.OpenElement(i++, "line");
				builder.AddAttribute(i++, "x1", X);
				builder.AddAttribute(i++, "y1", Y);
				builder.AddAttribute(i++, "x2", A);
				builder.AddAttribute(i++, "y2", B);
				builder.AddAttribute(i++, "stroke", Dragging?"gray":"none");
				builder.CloseElement();
			base.BuildRenderTree(builder);
		}
	}
}
