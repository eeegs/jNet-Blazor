using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace jNet.Blazor.Parts.Form
{
	internal class FieldData
	{
		public FieldData(string name, string label, PropertyInfo propertyInfo)
		{
			Name = name;
			Label = label;
			PropertyInfo = propertyInfo;
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
		public PropertyInfo PropertyInfo { get; set; }
	}
}
