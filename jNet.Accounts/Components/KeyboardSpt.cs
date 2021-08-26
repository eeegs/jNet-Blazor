using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
	public abstract class KeyboardSpt : InteropBase, IDisposable
	{
		[Flags]
		public enum Modifiers
		{
			None = 0,
			AltKey = 1,
			ShiftKey = 2,
			CtrlKey = 4,
			MetaKey = 8
		}

		readonly DotNetObjectReference<KeyboardSpt> CallBack;
		protected abstract ElementReference KeyTarget { get; }

		protected KeyboardSpt() : base("keyboardspt")
		{
			CallBack = DotNetObjectReference.Create(this);
		}

		[JSInvokable]
		public bool OnKeyDownInt(string key, Modifiers modifiers) => OnKeyDown(key, modifiers);
		protected abstract bool OnKeyDown(string key, Modifiers modifiers);

		protected async override Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender && !string.IsNullOrEmpty(KeyTarget.Id))
			{
				await ExecuteAsync("addKeyHandler", KeyTarget, CallBack);
			}
		}

		protected ValueTask ScrollIntoView(ElementReference element, bool? alignToTop = null)
		{
			if (alignToTop == null)
			{
				return ExecuteAsync("scrollIntoView", element);
			}
			else
			{
				return ExecuteAsync("scrollIntoView", element, alignToTop);
			}
		}

		void IDisposable.Dispose()
		{
			CallBack.Dispose();
		}
	}
}
