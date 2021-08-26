using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Accounts.Components
{
	public class ThemeService
	{
		Dictionary<string, string?> values = new()
		{
			["--brand-blue"] = "#5bc2e7",
			["--brand-green"] = "#97d600",
			["--brand-white"] = "white",
			["--brand-black"] = "black",
			["--text-primary-color"] = "rgba(0,0,0,.9)",
			["--text-selected-color"] = "HighlightText",
			["--menubar-height"] = "2.5rem",
			["--alert-height"] = "2rem",
			["--statusbar-height"] = "1.5rem",
			["--menubar-colour"] = "#f6f7fc",
			["--menubar-font-colour"] = "#333333",
			["--windowGray"] = "#dbe3eb",
			["--windowGrayDark"] = "#d7dee5",
			["--windowGrayLight"] = "#f0f4f8",
			["--messageError"] = "#ffc5c5",
			["--messageNote"] = "#aed6ff",
			["--messageCritical"] = "#dd9144",
			["--messageWarning"] = "#fee5aa",
			["--left-aside-width"] = "260px",
			["--std-back-color"] = "white",
			["--std-back-color-hover"] = "#f2f2f2",
			["--row-alt-color"] = "#fdffd8",
			["--row-alt-color-hover"] = "#f2f3d5",
			["--std-selected-color"] = "Highlight",
			["--std-selected-color-hover"] = "rgb(3105186)",
			["--money-cell-minwidth"] = "6rem",
		};

		public IEnumerable<string> Entries => values.Keys;

		public string? this[string key]
		{
			get => values.Try(key);
			set => values[key] = value;
		}
	}
}
