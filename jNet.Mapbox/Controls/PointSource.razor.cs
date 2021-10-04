using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public partial class PointSource<T> : BaseSource<T, double[]>
		where T: IHaveId
	{
		public enum Type
		{
			Point,
			Line
		}

		protected override string TypeName => SourceType switch
		{
			PointSource<T>.Type.Point => "MultiPoint",
			PointSource<T>.Type.Line => "LineString",
			_ => "MultiPoint"
		};

		[Parameter] public Type SourceType { get; set; }
	}
}
