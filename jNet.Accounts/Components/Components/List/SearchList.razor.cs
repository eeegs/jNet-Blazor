using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class SearchList<T> : ComponentBase
		where T : class
	{
		private IEnumerable<T> items = Enumerable.Empty<T>();
		[Parameter] public IEnumerable<T>? Items { get => items; set => items = value ?? Enumerable.Empty<T>(); }
		[Parameter] public T? Selected { get; set; }
		[Parameter] public EventCallback<T> SelectedChanged { get; set; }
		[Parameter] public Func<string, IEnumerable<T>> FindItems { get; set; } = _ => Enumerable.Empty<T>();
		[Parameter] public RenderFragment<T?> ChildContent { get; set; } = default!;

		string text = "";
		IEnumerable<T> results = Enumerable.Empty<T>();
		bool popup = false;

		T? picked
		{
			get => Selected;
			set
			{
				if (picked != Selected)
				{
					Selected = picked;
					SelectedChanged.InvokeAsync(picked);
				}
			}
		}

		void OnTyping(ChangeEventArgs args)
		{
			text = args.Value?.ToString() ?? "";
			popup = false;
			if (text.Length >= 3)
			{
				results = FindItems(text);
				popup = true;
			}
		}

		void Close()
		{
			popup = false;
		}
	}
}
