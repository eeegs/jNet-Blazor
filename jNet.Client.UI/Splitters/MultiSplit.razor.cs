using jNet.Client.UI;
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
	public partial class MultiSplit
	{
		private double max, initialSplit;
		DragManager DragManager;
		ElementReference Container;

		[Inject] IJSRuntime JS { get; set; }
		[Parameter] public RenderFragment ChildContent { get; set; }
		[Parameter] public bool IsVertical { get; set; } = false;
		[Parameter] public bool ShowThumb { get; set; } = true;
		[Parameter] public double[] Splits { get; set; } = { 50, 100, 150 };
		[Parameter] public int SplitWidth { get; set; } = 5;
		[Parameter] public Color SplitColor { get; set; } = Color.Transparent;
		[Parameter] public EventCallback<double[]> SplitsChanged { get; set; }

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
						initialSplit = Splits[(int)(obj ?? 0)];
					},
					OnDrag = (pos, obj) => DoDrag(pos.X, pos.Y, (int)(obj ?? 0)),
					OnDragEnd = (pos, obj) => DoDrag(pos.X, pos.Y, (int)(obj ?? 0)),
				};
			}
			return base.OnInitializedAsync();
		}

		private void DoDrag(double x, double y, int index)
		{
			var lower = index > 0 ? Splits[index - 1] + SplitWidth : 0;
			var upper = index < Splits.Length - 1 ? Splits[index + 1] - SplitWidth : max;

			var temp = initialSplit + (IsVertical ? y : x);
			if (temp < lower) temp = lower;
			if (temp > upper) temp = upper;
			if (Splits[index] != temp)
			{
				Splits[index] = temp;
				SplitsChanged.InvokeAsync(Splits);
				//StateHasChanged();
			}
		}
	}
}


