using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class TestList<T> : KeyboardSpt
	{
		ElementReference keyTarget;
		Dictionary<int, ElementReference> itemRefs = new();
		int FocusIndex = -1;
		int SelectedIndex = -1;
		string Selected = "";

		protected override ElementReference KeyTarget => keyTarget;

		bool isOpen => FocusIndex != -1;
		bool navigating;

		protected override bool OnKeyDown(string key, Modifiers modifiers)
		{
			navigating = false;
			if (key == "SpaceBar" || key == " ")
			{
				if (isOpen)
				{
					SetFocused(-1);
				}
				else
				{
					var s = SelectedIndex < 0 ? 0 : SelectedIndex;
					SetSelected(s);
				}
				StateHasChanged();
				return true;
			}

			if (key == "ArrowDown" && isOpen)
			{
				var s = (SelectedIndex >= itemRefs.Count - 1) ? SelectedIndex : SelectedIndex + 1;
				navigating = true;
				SetSelected(s);
				StateHasChanged();
				return true;
			}

			if (key == "ArrowUp" && isOpen)
			{
				var s = (SelectedIndex > 0) ? SelectedIndex - 1 : 0;
				navigating = true;
				SetSelected(s);
				StateHasChanged();
				return true;
			}

			return false;
		}

		void SetFocused(int index)
		{
			FocusIndex = index;
		}

		void SetSelected(int index)
		{
			FocusIndex = SelectedIndex = index;
			Selected = $"{'A' + index}";
			SetFocused(index);
		}

		void LostFocus(FocusEventArgs args)
		{
			if (!navigating)
			{
				SetFocused(-1);
			}
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (FocusIndex >= 0)
			{
				if (itemRefs.TryGetValue(FocusIndex, out var item))
				{
					await item.FocusAsync();
				}
			}
			await base.OnAfterRenderAsync(firstRender);
		}
	}
}
