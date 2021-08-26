using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{

	public enum LayerType
	{
		fill,
		line,
		symbol,
		circle,
		heatmap,
		[Description("fill-extrusion")]
		fillExtrusion,
		raster,
		hillshade,
		background,
		sky
	}

	public partial class Layer<T, TKey> : ComponentBase, IAsyncDisposable
		where T : IHaveId<TKey>
	{
		public record ClickData(TKey Id, double[] LngLat);
		[Parameter] public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
		[Parameter] public T? Selected { get; set; }
		[Parameter] public EventCallback<T?> SelectedChanged { get; set; }
		[CascadingParameter] public Map Map { get; set; } = default!;
		[CascadingParameter] public BaseSource Source { get; set; } = default!;
		[Inject] MapBoxService MapBoxService { get; set; } = default!;
		[Parameter] public Color Color { get; set; } = Color.FromArgb(123, 255, 20);
		[Parameter] public Color OtherColor { get; set; } = Color.Empty;
		[Parameter] public LayerType Type { get; set; } = LayerType.circle;
		[Parameter] public EventCallback<ClickData> OnClicked { get; set; }

		DotNetObjectReference<Layer<T, TKey>>? callbackRef;

		internal string Id => GetHashCode().ToString();

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				var data = new
				{
					id = Id,
					source = Source.Id,
					type = Type == LayerType.fillExtrusion ? "fill-extrusion" : Type.ToString(),
					paint = new Dictionary<string, object>()
				};

				var colorname = FillColorName(Type);
				if(colorname != null)
				{
					data.paint[$"{colorname}-color"] = new object[] { "case", new object[] { "has", $"{colorname}-color" }, new object[] { "get", $"{colorname}-color" }, Color.toHex() };
				}

				var linecolorname = LineColorName(Type);
				if (linecolorname != null)
				{
					data.paint[$"{linecolorname}-color"] = new object[] { "case", new object[] { "has", $"{linecolorname}-color" }, new object[] { "get", $"{linecolorname}-color" }, OtherColor.toHex() };
				}


				await Map.Loaded;
				await MapBoxService.ExecuteAsync("CreateLayer", Map.Id, data);
				if (OnClicked.HasDelegate)
				{
					callbackRef = DotNetObjectReference.Create(this);
					await MapBoxService.ExecuteAsync("RegisterLayerClicked", Map.Id, Id, callbackRef);
				}
			}
			await base.OnAfterRenderAsync(firstRender);
		}

		[JSInvokable]
		public async Task LayerClickedCallback(ClickData data)
		{
			var x = Items.SingleOrDefault(q => q.Id.Equals(data.Id));
			if(!EqualityComparer<T>.Default.Equals(Selected))
			{
				Selected = x;
				await SelectedChanged.InvokeAsync(x);
			}
			await OnClicked.InvokeAsync(data);
		}

		ValueTask IAsyncDisposable.DisposeAsync()
		{
			callbackRef?.Dispose();
			return MapBoxService.ExecuteAsync("DeleteLayer", Map.Id, Id);
		}

		static string? FillColorName(LayerType layerType)
		{
			var res = layerType switch
			{
				LayerType.circle => "circle",
				LayerType.fill => "fill",
				LayerType.line => "line",
				LayerType.symbol => "icon",
				LayerType.heatmap => "heatmap",
				LayerType.fillExtrusion => "fill-extrusion",
				LayerType.background => "background",
				LayerType.sky => "sky-atmosphere",
				LayerType.hillshade => "hillshade-accent",
				_ => null,
			};
			return res;
		}
		static string? LineColorName(LayerType layerType)
		{
			var res = layerType switch
			{
				LayerType.circle => "circle-stroke",
				LayerType.fill => "fill-outline",
				LayerType.symbol => "text",
				LayerType.hillshade => "hillshade-highlight",
				LayerType.sky => "sky-atmosphere-halo",
				_ => null,
			};
			return res;
		}
	}
}
