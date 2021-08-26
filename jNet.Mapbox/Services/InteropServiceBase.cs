using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace jNet.Mapbox
{
	public class InteropServiceBase
	{
		readonly string scriptFilePath;
		readonly string scriptFileName;
		readonly IJSRuntime Js;
		IJSObjectReference? module;

		public InteropServiceBase(string fileName, IJSRuntime jSRuntime)
		{
			scriptFilePath = $"./_content/{typeof(InteropServiceBase).Assembly.GetName().Name}";
			scriptFileName = fileName;
			Js = jSRuntime;
		}

		private async ValueTask<IJSObjectReference> GetModule()
		{
			module ??= await Js.InvokeAsync<IJSObjectReference>("import", $"{scriptFilePath}/{scriptFileName}.js");
			return module;
		}

		public async ValueTask ExecuteAsync(string function, params object[] args)
		{
			var module = await GetModule();
			await module.InvokeVoidAsync(function, args);
		}

		public async ValueTask<T> ExecuteAsync<T>(string function, params object[] args)
		{
			var module = await GetModule();
			return await module.InvokeAsync<T>(function, args);
		}
	}
}
