using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public enum MapStyle
	{
		Streets,
		Outdoors,
		Light,
		Dark,
		Satellite,
		SatelliteStreets,
		NavigationDay,
		NavigationNight
	};

	public partial class Map : ComponentBase, IAsyncDisposable
	{
		static readonly Dictionary<MapStyle, string> styleDict = new()
		{
			[MapStyle.Streets] = "mapbox://styles/mapbox/streets-v11",
			[MapStyle.Outdoors] = "mapbox://styles/mapbox/outdoors-v11",
			[MapStyle.Light] = "mapbox://styles/mapbox/light-v10",
			[MapStyle.Dark] = "mapbox://styles/mapbox/dark-v10",
			[MapStyle.Satellite] = "mapbox://styles/mapbox/satellite-v9",
			[MapStyle.SatelliteStreets] = "mapbox://styles/mapbox/satellite-streets-v11",
			[MapStyle.NavigationDay] = "mapbox://styles/mapbox/navigation-day-v1",
			[MapStyle.NavigationNight] = "mapbox://styles/mapbox/navigation-night-v1",
		};

		readonly DotNetObjectReference<Map> callbackRef;
		readonly TaskCompletionSource TCSMapLoaded = new();

		[Inject] MapBoxService MapBoxService { get; set; } = default!;
		ElementReference MapDiv;

		[Parameter] public RenderFragment? ChildContent { get; set; }
		[Parameter] public double[] Center { get; set; }
		[Parameter] public MapStyle MapStyle { get; set; }
		[Parameter] public int Zoom { get; set; } = 9;

		public Map()
		{
			callbackRef = DotNetObjectReference.Create(this);
			Center = new double[] { 148.946902, -35.087927 };
		}

		internal string Id => GetHashCode().ToString();

		public Task Loaded => TCSMapLoaded.Task;

		[JSInvokable]
		public Task MapLoadedCallback()
		{
			TCSMapLoaded.SetResult();
			return Task.CompletedTask;
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await MapBoxService.ExecuteAsync("CreateMap", Id, MapDiv, Center, Zoom, styleDict[MapStyle], callbackRef);
			}
			await base.OnAfterRenderAsync(firstRender);
		}

		ValueTask IAsyncDisposable.DisposeAsync()
		{
			callbackRef.Dispose();
			return MapBoxService.ExecuteAsync("DeleteMap", Id);
		}

		// MAP API
		public ValueTask<double[]> toXY(double[] lngLat)
		{
			return MapBoxService.ExecuteAsync<double[]>("MapProject", Id, lngLat);
		}

		public ValueTask<double[]> toLngLat(double[] points)
		{
			return MapBoxService.ExecuteAsync<double[]>("MapUnproject", Id, points);
		}
	}
}
