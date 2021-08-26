using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public static class MapBoxServiceExtentions
	{
		public static async void ConfigureMapBoxService(this IServiceCollection services, string apiKey)
		{
			services.AddSingleton<MapBoxService>();
			await services.BuildServiceProvider().GetRequiredService<MapBoxService>().SetApiToken(apiKey);
		}

		public static string toHex(this Color color) => $"#{color.R:x2}{color.G:x2}{color.B:x2}";
	}
}
