using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace jNet.Autoform
{
	public class EntityCache<TModel> : ComponentBase, IEnumerable<FieldData>
	{
		private static readonly IEnumerable<FieldData> Empty = Enumerable.Empty<FieldData>();
		private static readonly ConcurrentDictionary<Type, IEnumerable<FieldData>> cache = new();
		public FieldData? this[string fieldName] => cache[typeof(TModel)].SingleOrDefault(q => q.Name == fieldName);
		public IEnumerable<string> FieldNames => cache[typeof(TModel)].Select(q=>q.Name);

		public EntityCache()
		{
			// This is used to store (cache) the information about each property in the model and figure out
			// the appropriate attibutes for the HTML fields that will be built.

			// cheching the cache here means this only happens once per application instance
			lock (cache)
			{
				if (!cache.ContainsKey(typeof(TModel)))
				{
					// get the public instance properties, as these are all we will build fields for
					var properties = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

					// call the methods to get FieldData entries
					// leave out the null ones as null field entries are for properties that don't want to be rendered
					var entries = properties.Select(BuildFieldDataEntry).OrderBy(q => q.Order);

					// put the FieldData entries in the cache
					cache[typeof(TModel)] = entries.AsEnumerable();
				}
			}
		}

		static FieldData BuildFieldDataEntry(PropertyInfo propertyInfo)
		{
			var scaffold = propertyInfo.GetCustomAttribute<ScaffoldColumnAttribute>();
			var display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
			var datatype = propertyInfo.GetCustomAttribute<DataTypeAttribute>();
			var key = propertyInfo.GetCustomAttribute<KeyAttribute>();
			var range = propertyInfo.GetCustomAttribute<RangeAttribute>();
			var maxLen = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
			var minLen = propertyInfo.GetCustomAttribute<MinLengthAttribute>();
			var required = propertyInfo.GetCustomAttribute<RequiredAttribute>();
			var editable = propertyInfo.GetCustomAttribute<EditableAttribute>();
			var len = propertyInfo.GetCustomAttribute<StringLengthAttribute>();
			var description = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
			var displayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
			var readOnly = propertyInfo.GetCustomAttribute<ReadOnlyAttribute>();
			var defaultValue = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();

			InputType textType = InputType.Text;
			if (datatype != null)
			{
				textType = MapDataType(datatype.DataType);
			}

			var label = displayName?.DisplayName ?? display?.Name ?? propertyInfo.Name;
			var fieldData = new FieldData(propertyInfo, label)
			{
				Scaffold = scaffold?.Scaffold ?? true,
				CSS = new[] { "col-sm-12", "col-md-6", "col-lg-4", "col-xl-3" },
				Description = description?.Description ?? display?.Description,
				DataType = datatype?.DataType ?? DataType.Custom,
				Format = datatype?.DisplayFormat?.DataFormatString,
				Range = (range?.Minimum.ToString() ?? "", range?.Maximum.ToString() ?? ""),
				Length = (minLen?.Length ?? len?.MaximumLength ?? 0, maxLen?.Length ?? len?.MaximumLength ?? int.MaxValue),
				Order = display?.GetOrder() ?? int.MaxValue,
				Placeholder = display?.Prompt,
				ReadOnly = readOnly?.IsReadOnly ?? !(editable?.AllowEdit ?? true),
				Hidden = key != null,
				Required = required != null,
				TextType = textType,
				DefaultValue = defaultValue?.Value ?? null
			};

			fieldData.Format = textType switch
			{
				InputType.Date => "yyyy-MM-dd",
				InputType.Month => "yyyy-MM",
				InputType.DatetimeLocal => "yyyy-MM-dd HH:mm:ss",
				_ => fieldData.Format
			};
			return fieldData;
		}

		static InputType MapDataType(DataType type)
		{
			return type switch
			{
				DataType.Custom => InputType.Text,
				DataType.DateTime => InputType.DatetimeLocal,
				DataType.Date => InputType.Date,
				DataType.Time => InputType.Time,
				DataType.Duration => InputType.Text,
				DataType.PhoneNumber => InputType.Tel,
				DataType.Currency => InputType.Number,
				DataType.Text => InputType.Text,
				DataType.Html => InputType.Text,
				DataType.MultilineText => InputType.Text,
				DataType.EmailAddress => InputType.Email,
				DataType.Password => InputType.Password,
				DataType.Url => InputType.Url,
				DataType.ImageUrl => InputType.Image,
				DataType.CreditCard => InputType.Text,
				DataType.PostalCode => InputType.Text,
				DataType.Upload => InputType.File,
				_ => InputType.Text,
			};
		}

		public IEnumerator<FieldData> GetEnumerator()
		{
			if(cache.TryGetValue(typeof(TModel), out var res))
			{
				return res.GetEnumerator();
			}
			return Empty.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
