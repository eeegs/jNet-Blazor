using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public interface IHaveId<TKey>
	{
		TKey Id { get; }
	}


	public partial class PointSource<T, TKey> : BaseSource
		where T: IHaveId<TKey>
	{
		[Parameter] public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
		[Parameter] public Func<T, PointData>? Convertor { get; set; }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender && Convertor != null)
			{
				var q2 = from d in Items.Select(Convertor)
						 select new
						 {
							 type = "Feature",
							 id = d.Id,			// there is an issue with mapbox - this id is not carried around unless it looks like an integer
							 geometry = new
							 {
								 type = "Point",
								 coordinates = d.LngLat
							 },
							 properties = new Dictionary<string, object>
							 {
								 ["circle-color"] = d.Color.toHex(),
								 ["feature-id"] = d.Id		// put the id here and in BaseSource tell mapbox to use it 'promoteId'. - this is the 'official' workaround
							 }
						 };

				var data = new
				{
					type = "FeatureCollection",
					features = q2.ToArray()
				};
				await Map.Loaded;
				await MapBoxService.ExecuteAsync("SetSource", Map.Id, Id, data);
			}
		}
	}
}
