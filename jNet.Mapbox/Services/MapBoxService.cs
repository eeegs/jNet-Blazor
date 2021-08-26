using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public class MapBoxService : InteropServiceBase
	{

		public record LayerClickedEventArgs(string Layer, double[] LngLat, string ItemKey);


		public MapBoxService(IJSRuntime jSRuntime) : base("mapboxinterop", jSRuntime)
		{
		}

		public event EventHandler<LayerClickedEventArgs>? LayerClicked;

		[JSInvokable]
		public Task LayerClickedCallback(string layerName, double[] lngLat, string key)
		{
			LayerClicked?.Invoke(this, new(layerName, lngLat, key));
			return Task.CompletedTask;
		}

		// api from here
		public ValueTask SetApiToken(string apiToken) => ExecuteAsync("setToken", apiToken);
	}
}
