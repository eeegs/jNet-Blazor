using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jNet.Network
{
	public enum FieldType
	{
		Text,
		Integer,
		Number,
		Boolean,
		Date
	}

	public struct Geo
	{
		public float Latitude { get; init; }
		public float Longitude { get; init; }
		public float? Height { get; init; }
	}

	[JsonObject(IsReference = false)]
	public struct Port : IHaveName
	{
		public string Name { get; init; }
		public string? MetricName { get; init; }
		public string? MetricUnit { get; init; }
		//public Port(string name)
		//{
		//	Name = name;
		//}
	}

	[JsonObject(IsReference = false)]
	public struct Field : IHaveName
	{
		public string Name { get; init; }
		public FieldType Type { get; init; }
		public bool Required { get; init; }
	}

	public class Equipment : Node
	{
		public Equipment(EquipmentType equipmentType, string? name = null) : base(name ?? $"{equipmentType.Path}-Instance")
		{
			EquipmentType = equipmentType;
		}
		public List<Port> Ports { get; } = new();
		public EquipmentType EquipmentType { get; init; }
	}

	public class Connector : Link<Connector>
	{
		public Connector(Equipment a, Equipment b, string? name = null) : base(a, b, name)
		{
		}
	}

	public class EquipmentType : InfoImp, IHaveInfo, IId //, IEnumerable<EquipmentType>
	{
		public EquipmentType? ParentType { get; init; }
		public List<Field> DefinitionFields { get; } = new();
		public List<Field> InstanceFields { get; } = new();
		public List<Port> Ports { get; } = new();

		public EquipmentType(string name, EquipmentType? parentType = null) : base(name, parentType is null)
		{
			ParentType = parentType;
		}

		[JsonIgnore]
		public string Path => ParentType == null ? Name : $"{ParentType.Path}.{Name}";
	}
}
