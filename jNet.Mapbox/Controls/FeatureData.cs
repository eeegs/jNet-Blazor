using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public record FeatureData<T>(string Id, T[] LngLat, Color Color, Color? EdgeColor = null)
	{
		public Dictionary<string, object?> Properties { get; init; } = new();
	}
	public record PointData(string Id, double[][] LngLat, Color Color, Color? EdgeColor = null);
	public record AreaData(string Id, double[][][] LngLat, Color Color, Color? EdgeColor = null);
}
