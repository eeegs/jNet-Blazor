using jNet.Blazor2.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Blazor2.Components
{
	public class InspectorData
	{
		public InspectorData(IList<Field> fields, IDictionary<string, object?> store)
		{
			Fields = fields;
			Store = store;
		}

		public IList<Field> Fields { get; }
		public IDictionary<string, object?> Store { get; }
	}

	public partial class Inspector
	{
		[Parameter] public InspectorData? Data { get; set; }
		[Parameter] public string? Heading { get; set; }

		[Parameter] public double LabelWidth { get; set; }

		public Vector2 Position { get; set; }

		protected void OnChange(string fieldName, ChangeEventArgs args)
		{
			Data!.Store[fieldName] = args.Value;
		}
	}
}
