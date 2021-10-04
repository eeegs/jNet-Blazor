using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public interface IHaveId
	{
		string Id { get; }
	}

	public abstract class BaseSource<T, TType> : BaseSource
		where T: IHaveId
	{
		[Parameter] public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
		[Parameter] public T? Selected { get; set; }
		[Parameter] public EventCallback<T?> SelectedChanged { get; set; }
		[Parameter] public Func<T, FeatureData<TType>>? Convertor { get; set; }

		protected abstract string TypeName { get; }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender && Convertor != null)
			{
				var q2 = from d in Items.Select(Convertor)
						 select new
						 {
							 type = "Feature",
							 id = d.Id,         // there is an issue with mapbox - this id is not carried around unless it looks like an integer
							 geometry = new
							 {
								 type = TypeName,
								 coordinates = d.LngLat
							 },
							 properties = new Dictionary<string, object?>(d.Properties)
							 {
								 ["color"] = d.Color.toHex(),
								 ["edgecolor"] = d.EdgeColor?.toHex(),
								 ["feature-id"] = d.Id      // put the id here and in BaseSource tell mapbox to use it 'promoteId'. - this is the 'official' workaround
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

		internal override async Task<object?> SetSelected(Layer.ClickData data)
		{
			var x = Items.SingleOrDefault(q => q.Id == data.Id);
			Selected = x;
			await SelectedChanged.InvokeAsync(x);
			return x;
		}
	}
}
