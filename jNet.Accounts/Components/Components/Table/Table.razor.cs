using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class Table<T> : ComponentBase
	{
		private double[] widths = new double[0];
		private double[] splits = new double[0];
		private readonly HashSet<T> Expanded = new();
		[Parameter] public Func<T?, IEnumerable<T>> GetItems { get; set; } = _ => Enumerable.Empty<T>();
		[Parameter] public Func<T, string> GetItemKey { get; set; } = _ => "";

		[Parameter]
		public double[] Widths
		{
			get => widths;
			set
			{
				widths = value;
				splits = new double[widths.Length];
				if (splits.Length > 0)
				{
					for (int i = widths.Length - 1; i > 0; i--)
					{
						splits[i] = widths[i] - widths[i - 1];
					}
					splits[0] = widths[0];
				}
			}
		}

		[Parameter] public string CssClass { get; set; } = "";
		[Parameter] public RenderFragment? HeaderTemplate { get; set; }
		[Parameter] public bool ExpandAll { get; set; }
		[Parameter] public RenderFragment<Row> RowTemplate { get; set; } = default!;
		[Parameter] public RenderFragment<Row>? OpenRowTemplate { get; set; }
		[Parameter] public RenderFragment<Row>? ClosedRowTemplate { get; set; }
		[Parameter] public RenderFragment? FooterTemplate { get; set; }
		[Parameter] public EventCallback<double[]> WidthsChanged { get; set; }
		[Parameter] public T? Selected { get; set; }
		[Parameter] public EventCallback<T?> SelectedChanged { get; set; }

		Task SplitsChanged(double[] widths)
		{
			return WidthsChanged.InvokeAsync(widths);
		}

		IEnumerable<(T Item, int Depth)> GetChildren(T? parent, int depth = 0)
		{
			var items = GetItems(parent);
			foreach (var i in items)
			{
				yield return (i, depth);
				if (ExpandAll || Expanded.Contains(i))
				{
					foreach (var c in GetChildren(i, depth + 1))
					{
						yield return c;
					}
				}
			}
		}

		public void Toggle(T item)
		{
			if (!Expanded.Remove(item))
			{
				Expanded.Add(item);
			}
			StateHasChanged();
		}

		Task SetSelected(T newSelection)
		{
			if (!EqualityComparer<T>.Default.Equals(Selected, newSelection))
			{
				Selected = newSelection;
				StateHasChanged();
				return SelectedChanged.InvokeAsync(newSelection);
			}
			return Task.CompletedTask;
		}

		public record Row(T Item, bool IsOpen, Action<MouseEventArgs> Toggle, int Depth);
	}
}
