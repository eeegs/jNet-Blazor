using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Blazor2.Shared
{
	public enum FieldType
	{
		Text,
		Integer,
		Number,
		Boolean,
		Date
	}

	public record Geo
	{
		public float Latitude { get; init; }
		public float Longitude { get; init; }
		public float? Height { get; init; }
	}

	public record Port : IHaveName
	{
		public string Name { get; init; } = "";
		public string? MetricName { get; init; }
		public string? MetricUnit { get; init; }
	}

	public record Field : IHaveName
	{
		public string Name { get; init; } = "";
		public FieldType Type { get; init; }
		public bool Required { get; init; }
	}

	public class Instance : Node
	{
		public Instance() : base()
		{

		}

		public Instance(Definition definition, string? name = null)
		{
			Name = name ?? $"{definition.Name}-Instance";
			DefinitionKey = definition.Key;
		}
		public Guid DefinitionKey { get; init; }
	}

	public class Connector : Link
	{
		public Connector() : base()
		{
		}

		public Connector(Node a, Node b) : base(a, b)
		{
		}
	}

	public class Definition : IHaveName, IHaveKey
	{
		public Guid Key { get; init; } = Guid.NewGuid();
		public List<Field> DefinitionFields { get; init; } = new();
		public List<Field> InstanceFields { get; init; } = new();
		public List<Port> Ports { get; init; } = new();
		public Guid? ParentKey { get; init; }
		public string Name { get; set; } = "";

		public Definition(Definition? parentDefinition = null)
		{
			ParentKey = parentDefinition?.Key;
		}
	}
}
