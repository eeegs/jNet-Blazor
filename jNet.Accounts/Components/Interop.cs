using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public static class Interop
	{
		static IJSObjectReference? module;
		static string scriptFileName;

		static Interop()
		{
			scriptFileName = $"./_content/{typeof(Interop).Assembly.GetName().Name}/{nameof(Interop)}.js";
		}

		static async ValueTask ExecuteAsync(this IJSRuntime js, string function, params object[] args)
		{
			if (module is null)
			{
				module = await js.InvokeAsync<IJSObjectReference>("import", scriptFileName);
			}
			await module.InvokeVoidAsync(function, args);
		}

		static async ValueTask<T> ExecuteAsync<T>(this IJSRuntime js, string function, params object[] args)
		{
			if (module is null)
			{
				module = await js.InvokeAsync<IJSObjectReference>("import", scriptFileName);
			}
			return await module.InvokeAsync<T>(function, args);
		}


		public record Rect(double Top, double Height, double Left, double Width)
		{
			public double Right => Left + Width;
			public double Bottom => Top + Height;
		}

		public static ValueTask<Rect> StartCapture(this IJSRuntime js, ElementReference element, long pointerId)
		{
			return js.ExecuteAsync<Rect>("startCapture", element, pointerId);
		}

		public static ValueTask StopCapture(this IJSRuntime js, ElementReference element, long pointerId)
		{
			return js.ExecuteAsync("stopCapture", element, pointerId);
		}

		public static ValueTask<Rect> GetParentSize(this IJSRuntime js, ElementReference childElement)
		{
			return js.ExecuteAsync<Rect>("getParentSize", childElement);
		}

		public static ValueTask<Rect> GetSize(this IJSRuntime js, ElementReference element)
		{
			return js.ExecuteAsync<Rect>("getSize", element);
		}
	}
}
