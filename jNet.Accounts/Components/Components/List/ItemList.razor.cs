using jNet.Accounts.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class ItemList<T> : KeyboardSpt
		where T : class
	{
		readonly Dictionary<T, ElementReference> refs = new();
		List<T> items = new();
		int focusedIndex = -1;
		T? selected;
		T? focused;
		ElementReference keyTarget;
		protected override ElementReference KeyTarget => keyTarget;

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
					focusedIndex = items.IndexOf(selected!);
					selected = value;
				}
			}
		}


		[Parameter]
		public IEnumerable<T>? Items
		{
			get => items;
			set
			{
				items = value?.ToList() ?? new();
				focusedIndex = adjustFocusIndex(focusedIndex);
			}
		}

		async Task SetFocus(T item, bool? aligntoTop = null)
		{
			focused = item;
			focusedIndex = items.IndexOf(item);
			await ScrollIntoView(refs[item], aligntoTop);
			StateHasChanged();
		}

		int adjustFocusIndex(int newIndex)
		{
			if (newIndex < 0) newIndex = 0;
			if (newIndex >= items.Count) newIndex = items.Count - 1;
			return newIndex;
		}

		Task OnSelect(T selected)
		{
			if (selected != Selected)
			{
				Selected = selected;
				focusedIndex = items.IndexOf(selected);
				return SelectedChanged.InvokeAsync(selected);
			}
			return Task.CompletedTask;
		}

		async Task OnEnter(T item)
		{
			await SetFocus(item);
		}

		async Task OnLeave(T item)
		{
			await SetFocus(item);
		}

		protected override bool OnKeyDown(string key, Modifiers modifiers)
		{
			bool aligntTopTop = false;
			var oldIndex = focusedIndex;
			switch (key)
			{
				case "ArrowUp":
					focusedIndex--;
					aligntTopTop = true;
					break;
				case "ArrowDown":
					focusedIndex++;
					break;
				case "End":
					focusedIndex = items.Count - 1;
					break;
				case "Home":
					aligntTopTop = true;
					focusedIndex = 0;
					break;
				case "PageUp":
					aligntTopTop = true;
					focusedIndex -= 6;
					break;
				case "PageDown":
					focusedIndex += 6;
					break;
				case " ":
				case "SpaceBar":
				case "Enter":
					Selected = items[focusedIndex];
					SelectedChanged.InvokeAsync(Selected);
					return true;
			}

			focusedIndex = adjustFocusIndex(focusedIndex);

			if (oldIndex != focusedIndex)
			{
				if (focusedIndex >= 0) SetFocus(items[focusedIndex], aligntTopTop);
				return true;
			}
			return false;
		}
	}
}
