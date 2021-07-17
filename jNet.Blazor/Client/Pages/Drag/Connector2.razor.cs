using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	public partial class Connector2
	{
		private static double Tau = 2 * Math.PI;

		[Parameter] public Join Left { get; set; }
		[Parameter] public Join Right { get; set; }
		[Parameter] public Color Fill { get; set; }
	}
}
