using jNet.Accounts.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class TreeNode<T>
		where T : class
	{
		[Parameter] public TreeNode<T>? Parent { get; set; } = null;
		[CascadingParameter] public Tree<T> Tree { get; set; } = default!;
		[Parameter] public T Value { get; set; } = default!;
		[Parameter] public int Depth { get; set; }
		[Parameter] public T? Selected { get; set; } = default!;

		ElementReference NodeElement;

		internal readonly List<TreeNode<T>> Children = new();

		internal void Expand()
		{
			Tree.Expanded.Add(Value);
			StateHasChanged();
		}

		internal void Collapse()
		{
			Tree.Expanded.Remove(Value);
			Children.Clear();
			StateHasChanged();
		}

		Task Select(MouseEventArgs args) => Tree.SetSelected(Value);

		protected override Task OnParametersSetAsync()
		{
			if (Tree == default)
			{
				throw new InvalidOperationException("TreeNodes can only be used in a tree");
			}

			return base.OnParametersSetAsync();
		}

		protected override Task OnInitializedAsync()
		{
			if (Parent is not null)
			{
				if (!Parent.Children.Contains(this))
				{
					Parent.Children.Add(this);
				}
			}
			return base.OnInitializedAsync();
		}


		internal void SetFocus() => Tree.SetFocused(this);

		internal void MoveFocusDown(bool stepOverChildren)
		{
			if (!stepOverChildren && Children.Count > 0)
			{
				// I have childern so move to the first of them
				Children[0].SetFocus();
			}
			else
			{
				if (Parent != null)
				{
					var i = Parent.Children.IndexOf(this) + 1;
					if (i >= Parent.Children.Count)
					{
						// at the bottom so move to me parnet next sibling
						Parent.MoveFocusDown(true);
					}
					else
					{
						Parent.Children[i].SetFocus();
					}
				}
			}
		}

		internal void MoveFocusUp()
		{
			if (Parent != null)
			{
				var i = Parent.Children.IndexOf(this) - 1;
				if (i < 0)
				{
					// at top so move to parent
					if(Parent.Value != default)
					{
						Parent.SetFocus();
					}
				}
				else
				{
					var tgt = Parent.Children[i];
					while (tgt.Children.Count > 0)
					{
						tgt = tgt.Children[^1];
					}
					tgt.SetFocus();
				}
			}
		}

		bool _isOpen(T item)
		{
			var x = Tree.Expanded.Contains(item);
			return x;
		}

		bool _hasChildren(T item)
		{
			var x = Tree.HasChildren(item);
			return x;
		}

		bool _isSelected(T item)
		{
			var x = Selected == item;
			return x;
		}

		IEnumerable<T> _getChildren(T item)
		{
			var x = Tree.GetChildren(item);
			return x;
		}
	}
}

