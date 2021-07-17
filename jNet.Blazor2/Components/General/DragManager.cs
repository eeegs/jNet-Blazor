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

namespace jNet.Blazor2.Components
{
	public class DragManager
	{
		public record DragArgs((double X, double Y) Position, object? ContextData = null);

		readonly IJSRuntime js;
		//private ElementReference dragTarget;

		bool captured;
		double X, Y;
		(double X, double Y) dragOffset;

		public DragManager(IJSRuntime js)
		{
			this.js = js;
		}

		public (double X, double Y) DragOffset => dragOffset;

		public delegate void DragEventHandler(DragManager sender, DragArgs args);
		public event DragEventHandler? DragStartEvent;
		public event DragEventHandler? DragEvent;
		public event DragEventHandler? DragEndEvent;

		public Action<(double X, double Y), object?> OnDragStart { get; set; } = (_, _) => { };
		public Action<(double X, double Y), object?> OnDrag { get; set; } = (_, _) => { };
		public Action<(double X, double Y), object?> OnDragEnd { get; set; } = (_, _) => { };

		protected virtual Task DragStart((double X, double Y) initial, PointerEventArgs e, object? data) => Task.CompletedTask;
		protected virtual Task<(double X, double Y)> Drag((double X, double Y) offset, PointerEventArgs e, object? data) => Task.FromResult(offset);
		protected virtual Task<(double X, double Y)> DragEnd((double X, double Y) offset, PointerEventArgs e, object? data) => Task.FromResult(offset);

		//public void SetDragTarget(ElementReference element) => dragTarget = element;

		public async Task OnPointerDown(ElementReference target, PointerEventArgs e, object? data = null)
		{
			if (e.Button == 0)
			{
				X = e.ClientX;
				Y = e.ClientY;
				captured = true;
				await js.StartCapture(target, e.PointerId);
				await DragStart((X, Y), e, data);
				DragStartEvent?.Invoke(this, new((X, Y), data));
				OnDragStart?.Invoke((X, Y), data);
			}
		}

		public async Task OnPointerMove(ElementReference target, PointerEventArgs e, object? data = null)
		{
			if (captured)
			{
				dragOffset = await Drag((e.ClientX - X, e.ClientY - Y), e, data);
				DragEvent?.Invoke(this, new(dragOffset, data));
				OnDrag?.Invoke(dragOffset, data);
			}
		}

		public async Task OnPointerUp(ElementReference target, PointerEventArgs e, object? data = null)
		{
			if (e.Button == 0 && captured)
			{
				dragOffset = await DragEnd((e.ClientX - X, e.ClientY - Y), e, data);
				DragEndEvent?.Invoke(this, new(dragOffset, data));
				OnDragEnd?.Invoke(dragOffset, data);
				await js.StopCapture(target, e.PointerId);
				captured = false;
			}
		}
	}
}
