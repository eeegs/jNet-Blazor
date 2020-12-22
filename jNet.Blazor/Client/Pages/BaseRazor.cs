using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace jNet.Blazor.Client.Pages
{
	
	[Authorize]
	public class BaseRazor: ComponentBase
	{
		[Inject]
		protected HttpClient Http { get; private set; }

		[Inject]
		protected NavigationManager NavigationManager { get; private set; }

		[Inject]
		protected IJSRuntime JSRuntime { get; private set; }
	}

	public class BaseRazor<T> : BaseRazor, IDisposable
		where T : new()
	{
		private bool disposedValue;

		protected T ViewModel { get; }

		public BaseRazor()
		{
			ViewModel = new T();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~BaseRazor()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
