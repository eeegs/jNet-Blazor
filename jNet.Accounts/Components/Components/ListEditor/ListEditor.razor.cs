using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public partial class ListEditor<T>
	{
		[Parameter] public ICollection<T> Items { get; set; } = default!;
		[Parameter] public EventCallback<IEnumerable<T>> ItemsChanged { get; set; }
		[CascadingParameter] public EditContext EditContext { get; set; } = default!;
		[Parameter] public RenderFragment<T> RowTemplate { get; set; } = default!;
		[Parameter] public RenderFragment? HeaderTemplate { get; set; }

		readonly HashSet<T> Selected = new();

		void CheckOnChange(ChangeEventArgs e, T item)
		{
			if ((bool)(e.Value ?? false))
			{
				Selected.Add(item);
			}
			else
			{
				Selected.Remove(item);
			}
		}

		void OnChange(FocusEventArgs e)
		{
		}
	}



	public class InvalidList<T>
	{
		public string? ValidationMessage { get; set; }
		public ICollection<T> InvalidItems { get; } = new HashSet<T>();
	}
	public interface IValidate<T>
	{
		InvalidList<T> IsValid(IEnumerable<T> values);
		string? IsValid(T value);
	}

	public class Validator<T> : IValidate<T>
	{
		public static Validator<T> Default = new();
		private Validator() { }
		public InvalidList<T> IsValid(IEnumerable<T> values) => new();
		public string? IsValid(T value) => null;
	}

}
