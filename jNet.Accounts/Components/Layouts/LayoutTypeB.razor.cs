using jNet.Accounts.Shared;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace jNet.Accounts.Components
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
