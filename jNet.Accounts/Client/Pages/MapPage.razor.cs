using jNet.Accounts.Components;
using jNet.Mapbox;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace jNet.Accounts.Client.Pages
{
	public partial class MapPage : ComponentBase
	{
		Map? Map;
		Popup<Data?>? Popup;

		protected override Task OnInitializedAsync()
		{
			return base.OnInitializedAsync();
		}

		public class Data: IHaveId<string>
		{

			public double Lat;
			public double Lng;
			public Color Color;
			public string Id { get; }

			public Data(string id)
			{
				Id = id;
			}
		}

		static Data[] data = new Data[]
		{
			new Data("A") { Color = Color.Red, Lng = 148.946902, Lat=-35.087927 },
			new Data("B") { Color = Color.Blue, Lng = 148.956902, Lat=-35.081927 },
			new Data("C") { Color = Color.Green, Lng = 148.936902, Lat=-35.077927 },
			new Data("D") { Color = Color.Yellow, Lng = 148.926902, Lat=-35.097927 },
		};

		Func<Data, BaseSource.PointData> Data2PointData => d => new BaseSource.PointData(d.Id, new[] { d.Lng, d.Lat }, d.Color);
	}
}
