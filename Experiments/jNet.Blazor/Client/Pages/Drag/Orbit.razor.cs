using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	public partial class Orbit : BaseDraggable
	{
		protected static double Tau = 2 * Math.PI;
		private double dX;
		private double dY;
		protected override double X => Position.X + Distance * Math.Cos(Tau * Angle);
		protected override double Y => Position.Y - Distance * Math.Sin(Tau * Angle);


		[Parameter] public double Distance { get; set; }
		[Parameter] public double Angle { get; set; }
		[Parameter] public EventCallback<double> AngleChanged { get; set; }

		protected override Task DragStart(PointerEventArgs e)
		{
			dX = e.OffsetX - X;
			dY = e.OffsetY - Y;
			return Task.CompletedTask;
		}

		protected override Task Drag(PointerEventArgs e)
		{
			var angle = Math.Atan2(-e.OffsetY + Position.Y + dY, e.OffsetX - Position.X - dX) / Tau;
			return AngleChanged.InvokeAsync(angle);
		}
	}
}
