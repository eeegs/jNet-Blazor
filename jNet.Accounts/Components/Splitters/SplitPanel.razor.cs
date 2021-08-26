using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class SplitPanel
	{
		[Flags]
		public enum Panel
		{
			Neither = 0,
			PanelA = 1,
			PanelB = 2,
			Both = 3
		}

		string BackGroundColorHex => $"#{BackGroundColor.R:x2}{BackGroundColor.G:x2}{BackGroundColor.B:x2}{BackGroundColor.A:x2}";

		[Parameter] public RenderFragment? PanelA { get; set; }
		[Parameter] public RenderFragment? PanelB { get; set; }
		[Parameter] public bool IsVertical { get; set; } = false;
		[Parameter] public Panel IsClosed { get; set; } = Panel.Neither;
		[Parameter] public Panel CanClose { get; set; } = Panel.Both;
		[Parameter] public double Split { get; set; } = 200;
		[Parameter] public int PanelAMin { get; set; } = 0;
		[Parameter] public int PanelBMin { get; set; } = 0;
		[Parameter] public int PanelAMax { get; set; } = int.MaxValue;
		[Parameter] public int SplitWidth { get; set; } = 5;
		[Parameter] public Color SplitColor { get; set; } = Color.Transparent;
		[Parameter] public Color BackGroundColor { get; set; } = Color.Transparent;
		[Parameter] public EventCallback<double> SplitChanged { get; set; }
		[Parameter] public EventCallback<Panel> IsClosedChanged { get; set; }

		double BoundSplit
		{
			get => Split;
			set
			{
				var (pos, close) = value switch
				{
					<= 0 => (0, Panel.PanelA),
					var x when x < PanelAMin => (PanelAMin, Panel.Neither),
					var x when x > PanelAMax => (PanelAMax, Panel.Neither),
					_ => (value, Panel.Neither)
				};

				Split = pos;
				SplitChanged.InvokeAsync(pos);
			}
		}
	}
}
