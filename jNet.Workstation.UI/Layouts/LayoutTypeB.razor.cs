using jNet.Shared.Code;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace jNet.Workstation.UI.Layouts
{
	public partial class LayoutTypeB
	{
		[Parameter] public RenderFragment? Left { get; set; }
		[Parameter] public RenderFragment? Right { get; set; }
		[Parameter] public RenderFragment? Tool { get; set; }

		[Parameter] public Setting Settings { get; set; } = new();

		Setting.Split this[string key] { get => Settings[key]!; set => Settings[key] = value; }
	}
}
