using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Client.UI.General
{
	public partial class AsyncWarpper<T> : ComponentBase
	//where T: IHaveKey
	{
		[Parameter]
		public RenderFragment<T?>? ChildContent { get; set; }

		[Parameter]
		public Task<T?>? DataPromise { get; set; }

		[Parameter]
		public T? Data { get; set; }

		protected override Task OnParametersSetAsync()
		{
			if (DataPromise is not null)
			{
				if (DataPromise.Status != TaskStatus.RanToCompletion)
				{
					DataPromise.ContinueWith(x =>
					{
						Data = x.Result;
						StateHasChanged();
					});
				}
				else
				{
					Data = DataPromise.Result;
				}
			}
			return base.OnParametersSetAsync();
		}
	}
}
