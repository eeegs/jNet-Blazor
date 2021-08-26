using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class Tree<T> : KeyboardSpt
		where T : class
	{
		readonly internal HashSet<T> Expanded = new();
		ElementReference keyTarget;
		protected override ElementReference KeyTarget => keyTarget;

		[Parameter] public Func<T?, IEnumerable<T>> GetChildren { get; set; } = _ => Enumerable.Empty<T>();
		[Parameter] public Func<T, bool> HasChildren { get; set; } = _ => false;
		[Parameter] public Func<T, T?> GetParent { get; set; } = _ => default;
		[Parameter] public RenderFragment<T> ChildContent { get; set; } = default!;
		[Parameter] public RenderFragment<T>? NodeContent { get; set; }
		[Parameter] public EventCallback<T> SelectedChanged { get; set; }
		[Parameter] public T? Selected { get; set; }

		internal TreeNode<T>? Focused { get; private set; } = null;

		internal void SetFocused(TreeNode<T> treeNode)
		{
			Focused = treeNode;
			StateHasChanged();
		}

		protected override bool OnKeyDown(string key, Modifiers modifiers)
		{
			if (Focused == null)
			{
				return false;
			}
			switch (key)
			{
				case "ArrowUp":
					Focused.MoveFocusUp();
					break;
				case "ArrowDown":
					Focused.MoveFocusDown(false);
					break;
				case "ArrowRight":
					if (HasChildren(Focused.Value))
					{
						if (!Expanded.Contains(Focused.Value))
						{
							Focused.Expand();
						}
						else
						{
							Focused.MoveFocusDown(false);
						}
					}
					break;
				case "ArrowLeft":
					if (Focused.Children.Count > 0)
					{
						Focused.Collapse();
					}
					else
					{
						if (Focused.Parent?.Value != default)
						{
							Focused.Parent?.SetFocus();
						}
					}
					break;
				case " ":
				case "SpaceBar":
				case "Enter":
					Selected = Focused.Value;
					SelectedChanged.InvokeAsync(Selected);
					break;
				default:
					return false;
			}
			return true;
		}

		internal async Task SetSelected(T newSelection)
		{
			if (newSelection != Selected)
			{
				Selected = newSelection;
				await SelectedChanged.InvokeAsync(newSelection);
				StateHasChanged();
			}
		}

		protected override Task OnParametersSetAsync()
		{
			var cur = Selected;
			while (cur != null)
			{
				cur = GetParent(cur);
				if (cur is not null)
				{
					Expanded.Add(cur);
				}
			}
			return base.OnParametersSetAsync();
		}
	}
}
