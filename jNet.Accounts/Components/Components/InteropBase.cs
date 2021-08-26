using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public abstract class InteropBase : ComponentBase
	{
		static readonly SemaphoreSlim semaphoreSlim = new(1, 1);
		static readonly Dictionary<string, IJSObjectReference> modules = new();
		static readonly string scriptFilePath;
		readonly string scriptFileName;
		[Inject] IJSRuntime Js { get; set; } = default!;

		static InteropBase()
		{
			scriptFilePath = $"./_content/{typeof(InteropBase).Assembly.GetName().Name}";
		}

		public InteropBase(string fileName)
		{
			scriptFileName = fileName;
		}

		private async ValueTask<IJSObjectReference> GetModule()
		{
			await semaphoreSlim.WaitAsync();
			try
			{
				if (!modules.TryGetValue(scriptFileName, out var module))
				{
					module = await Js.InvokeAsync<IJSObjectReference>("import", $"{scriptFilePath}/{scriptFileName}.js");
				}
				return module;
			}
			finally
			{
				semaphoreSlim.Release();
			}
		}

		protected async ValueTask ExecuteAsync(string function, params object[] args)
		{
			var module = await GetModule();
			await module.InvokeVoidAsync(function, args);
		}

		protected async ValueTask<T> ExecuteAsync<T>(string function, params object[] args)
		{
			var module = await GetModule();
			return await module.InvokeAsync<T>(function, args);
		}
	}
}
