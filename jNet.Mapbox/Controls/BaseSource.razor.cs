using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public partial class BaseSource : ComponentBase, IAsyncDisposable
	{
		public record PointData(string Id, double[] LngLat, Color Color);
		[Parameter] public RenderFragment? ChildContent { get; set; }
		[CascadingParameter] public Map Map { get; set; } = default!;
		[Inject] protected MapBoxService MapBoxService { get; set; } = default!;
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender)
			{ 
				var data = new
				{
					type = "geojson",
					promoteId = "feature-id",
					data = new { }
				};
				await Map.Loaded;
				await MapBoxService.ExecuteAsync("CreateSource", Map.Id, Id, data);
			}
		}

		protected internal string Id => GetHashCode().ToString();

		ValueTask IAsyncDisposable.DisposeAsync()
		{
			return MapBoxService.ExecuteAsync("DeleteSource", Map.Id, Id);
		}
	}
}
