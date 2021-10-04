using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class ItemList2<T> : KeyboardSpt
		where T : class
	{
		readonly Dictionary<T, ElementReference> refs = new();
		List<T> items = new();
		protected override ElementReference KeyTarget => keyTarget;
		T? selected;
		T? focused;
		ElementReference keyTarget;
		int focusedIndex = -1;

		[Parameter] public RenderFragment<T?> ChildContent { get; set; } = default!;

		[Parameter] public EventCallback<T?> SelectedChanged { get; set; }

		[Parameter]
		public T? Selected
		{
			get => selected;
			set
			{
				if (selected != value)
				{
					SetFocused(value);
					selected = value;
				}
			}
		}

		async void SetSelected(T? item)
		{
			if(selected != item)
			{
				selected = item;
				SetFocused(item);
				await SelectedChanged.InvokeAsync(item);
			}
		}

		async void SetFocused(T? item)
		{
			if(item != null)
			{
				focusedIndex = items.IndexOf(item);
				focused = item;
				await refs[item].FocusAsync();
			}
		}

		[Parameter]
		public IEnumerable<T>? Items
		{
			get => items;
			set
			{
				items = value?.ToList() ?? new();
			}
		}

		protected override bool OnKeyDown(string key, Modifiers modifiers)
		{
			return false;
		}

		void OnFocus(FocusEventArgs args)
		{
			if (args.Type=="focus" && focused == null && items != null && items.Count > 0)
			{
				SetFocused(items[0]);
			}
		}
	}
}
