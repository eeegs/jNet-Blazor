using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Workstation.UI.General
{
	public partial class Window
	{
		[Parameter] public string Title { get; set; } = "Window";
		[Parameter] public RenderFragment ChildContent { get; set; }
	}
}
