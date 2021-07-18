using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
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
	public abstract partial class BaseDraggable
	{
		[Inject] private IJSRuntime JS { get; set; }
		private bool captured;

		protected ElementReference myTarget;

		[Parameter]
		public string Text { get; set; }

		[Parameter]
		public Color Fill { get; set; }

		[Parameter]
		public Color Stroke { get; set; }

		[Parameter]
		public double StrokeWidth { get; set; }

		[Parameter]
		public double Radius { get; set; }

		[Parameter]
		public Vector2 Position { get; set; }

		[Parameter]
		public EventCallback<bool> FocusChanged { get; set; }

		protected virtual double X => Position.X;
		protected virtual double Y => Position.Y;

		protected virtual Task DragStart(PointerEventArgs e) => Task.CompletedTask;
		protected virtual Task Drag(PointerEventArgs e) => Task.CompletedTask;
		protected virtual Task DragEnd(PointerEventArgs e) => Drag(e);

		protected async Task OnMouseDown(PointerEventArgs e)
		{
			captured = true;
			await JS.InvokeVoidAsync("jNet.startCapture", myTarget, e.PointerId);
			await DragStart(e);
		}

		protected async Task OnMouseMove(PointerEventArgs e)
		{
			if (captured)
			{
				await Drag(e);
			}
		}

		protected async Task OnMouseUp(PointerEventArgs e)
		{
			if (captured)
			{
				captured = false;
				await JS.InvokeVoidAsync("jNet.stopCapture", myTarget, e.PointerId);
				await DragEnd(e);
			}
		}

		private void Focus(bool gotFocus)
		{
			FocusChanged.InvokeAsync(gotFocus);
		}
	}
}
