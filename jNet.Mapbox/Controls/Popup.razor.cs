using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public partial class Popup<T> : ComponentBase, IAsyncDisposable
	{
		[Parameter] public RenderFragment<T?>? ChildContent { get; set; }
		[CascadingParameter] public Map Map { get; set; } = default!;
		[Parameter] public T? Item { get; set; }
		[Parameter] public bool AutoClose { get; set; } = true;
		[Parameter] public bool ShowCloseButton { get; set; } = true;

		[Inject] MapBoxService MapBoxService { get; set; } = default!;
		ElementReference popupRef;

		string Id => GetHashCode().ToString();
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await MapBoxService.ExecuteAsync("CreatePopup", Id, new { closeButton = ShowCloseButton, closeOnClick = AutoClose });
			}
			await MapBoxService.ExecuteAsync("UpdatePopup", Id, popupRef);

			await base.OnAfterRenderAsync(firstRender);
		}

		public ValueTask Show(double[] lngLat) => MapBoxService.ExecuteAsync("ShowPopup", Id, Map.Id, lngLat);

		public ValueTask Hide() => MapBoxService.ExecuteAsync("HidePopup", Id);

		ValueTask IAsyncDisposable.DisposeAsync() => MapBoxService.ExecuteAsync("DeletePopup", Id);
	}
}
