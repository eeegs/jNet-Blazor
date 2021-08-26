using jNet.Accounts.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using jNet.Mapbox;

namespace jNet.Accounts.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped<Store.Accounts>();
			builder.Services.AddScoped<Store.Transactions>();
			builder.Services.AddScoped<Store.Balances>();
			builder.Services.AddScoped<Store.TaxEntries>();
			builder.Services.AddSingleton<AlertBarService>();
			builder.Services.AddSingleton<ToolBarService>();
			builder.Services.AddSingleton<MenuBarService>();
			builder.Services.AddSingleton<StatusBarService>();
			builder.Services.AddSingleton<PanelService>();
			builder.Services.AddSingleton<ThemeService>();
			builder.Services.ConfigureMapBoxService("pk.eyJ1IjoicGV0ZXJnaWxlcyIsImEiOiJja3J1OHl2N2oxaTl3Mm9wbzkzNWEwOHZvIn0.mgTkRmRr0w23h_aglnLzng");
			await builder.Build().RunAsync();
		}
	}
}
