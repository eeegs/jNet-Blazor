using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace jNet.Autoform
{
	public class FieldData
	{
		public FieldData(PropertyInfo propertyInfo, string label)
		{
			PropertyInfo = propertyInfo;
			Label = label;
			Name = propertyInfo.Name;
			Id = $"id{GetHashCode()}";
		}

		public string Id { get; }
		public string Name { get; }
		public int Order { get; set; }
		public string? Description { get; set; }
		public string? Format { get; set; }
		public string? Placeholder { get; set; }
		public string Label { get; }
		public bool Required { get; set; }
		public bool Disabled { get; set; }
		public bool ReadOnly { get; set; }
		public (int min, int max) Length { get; set; }
		public (string min, string max) Range { get; set; }
		public InputType TextType { get; set; }
		public DataType DataType { get; set; }
		public ComponentBase? Component { get; set; }
		public PropertyInfo PropertyInfo { get; }
		public bool Hidden { get; set; }
		public string[] CSS { get; set; } = Array.Empty<string>();
		public bool Scaffold { get; set; }
		public object? DefaultValue { get; set; }
	}
}
