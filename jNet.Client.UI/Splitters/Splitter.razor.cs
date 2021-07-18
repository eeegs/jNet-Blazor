using jNet.Client.UI.General;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Client.UI.Splitters
{
	public partial class Splitter
	{
		private double max, initialSplit;
		DragManager? DragManager;
		ElementReference Container;
		private double split = 50;

		[Inject] IJSRuntime? JS { get; set; }
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter] public bool IsVertical { get; set; } = false;
		[Parameter] public bool ShowThumb { get; set; } = true;
		[Parameter]
		public double Split
		{
			get => split;
			set
			{
				if (value == 0)
				{
					value = 1;
				}
				split = value;
			}
		}
		[Parameter] public int SplitWidth { get; set; } = 5;
		[Parameter] public Color SplitColor { get; set; } = Color.Transparent;
		[Parameter] public EventCallback<double> SplitChanged { get; set; }

		protected override Task OnInitializedAsync()
		{
			if (JS is not null)
			{
				DragManager = new DragManager(JS)
				{
					OnDragStart = async (pos, obj) =>
					{
						var rC = await JS!.GetSize(Container);
						max = !IsVertical ? rC.Width - SplitWidth : rC.Height - SplitWidth;
						initialSplit = Split;
					},
					OnDrag = (pos, obj) => DoDrag(pos.X, pos.Y),
					OnDragEnd = (pos, obj) => DoDrag(pos.X, pos.Y),
				};
			}
			return base.OnInitializedAsync();
		}

		private void DoDrag(double x, double y)
		{
			var temp = initialSplit + (IsVertical ? y : x);
			if (temp < 0) temp = 0;
			if (temp > max) temp = max;
			if (Split != temp)
			{
				Split = temp;
				SplitChanged.InvokeAsync(temp);
			}
		}
	}
}


