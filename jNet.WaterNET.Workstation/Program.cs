using jNet.Client.Code;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace jNet.WaterNET.Workstation
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			//builder.Services.AddScoped<AuthenticationStateProvider, FakeAuth>();
			//builder.Services.AddScoped<SignOutSessionStateManager>();
			//builder.Services.AddScoped<IRemoteAuthenticationService,  >();
			builder.Services.AddScoped<Store>();
			builder.Services.AddOptions();
			builder.Services.AddAuthorizationCore();
			await builder.Build().RunAsync();
		}
	}
}
