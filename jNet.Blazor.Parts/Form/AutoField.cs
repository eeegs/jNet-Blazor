using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace jNet.Blazor.Parts.Form
{
	public class AutoField : ComponentBase
	{
		private static readonly ConcurrentDictionary<Type, Dictionary<string, FieldData>> cache = new();
		private static MethodInfo? methodBC;

		[CascadingParameter] jNet.AspNetCore.Components.Forms.EditContext? CascadedEditContext { get; set; }

		protected override void OnInitialized()
		{
			if (CascadedEditContext == null)
			{
				throw new InvalidOperationException($"{nameof(AutoField)} requires a cascading " +
					$"parameter of type {nameof(EditContext)}. For example, you can use {nameof(AutoField)} " +
					$"inside an EditForm.");
			}
			base.OnInitialized();
		}

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			// This is used to store (cache) the information about each property in the model and figure out
			// the appropriate attibutes for the HTML fields that will be built.
			// ...but only if there is a model
			if (CascadedEditContext?.Model is not null)
			{
				var model = CascadedEditContext.Model;
				var modelType = model.GetType();

				// cheching the cache here means this only happens once per application instance
				if (!cache.ContainsKey(modelType))
				{
					// get the public instance properties, as these are all we will build fields for
					var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

					// call the methods to get FieldData entries
					// leave out the null ones as null field entries are for properties that don't want to be rendered
					var entries = properties.Select(BuildFieldDataEntry).Where(q => q != null).Select(q => q!);

					// put the FieldData entries in the cache
					cache[modelType] = entries.ToDictionary(q => q.Name, q => q);

					// since we are cahing stuff and this is only entered once...
					// grab the reflected ref to BuildComponent so we can make Generic versions later
					methodBC = this.GetType().GetMethod(nameof(BuildComponent), BindingFlags.NonPublic | BindingFlags.Instance)!;
				}
			}
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			base.BuildRenderTree(builder);
			// We use the cached FieldData information for this model to render components with all the details.
			// A surrogate item based on InputBase<T> is built into the Render Pipline in the following code.
			// This surrogate, PlaceHolderComponent<T>, then renders the detailed HTML
			// but only if there is a model...
			if (CascadedEditContext?.Model is not null)
			{
				var model = CascadedEditContext.Model;
				var type = model.GetType();
				// ...and the Model has been cached.
				if (cache.TryGetValue(type, out var fieldData))
				{
					foreach (var fd in fieldData.Values)
					{
						// for each cached, therefore to be built, field in the model get a generic method based on the property type
						var generic = methodBC!.MakeGenericMethod(fd.PropertyInfo.PropertyType);

						// invoke the method to build the surrogate component
						// == BuildComponent<PropertyType>(builder, model, fieldData)
						generic.Invoke(this, new object[] { builder, model, fd });
					}
				}
			}
		}

		static FieldData? BuildFieldDataEntry(PropertyInfo propertyInfo)
		{
			FieldData? fieldData = null;

			var scaffold = propertyInfo.GetCustomAttribute<ScaffoldColumnAttribute>();
			if (scaffold?.Scaffold ?? true)
			{
				var display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
				var datatype = propertyInfo.GetCustomAttribute<DataTypeAttribute>();
				var key = propertyInfo.GetCustomAttribute<KeyAttribute>();
				var range = propertyInfo.GetCustomAttribute<RangeAttribute>();
				var maxLen = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
				var minLen = propertyInfo.GetCustomAttribute<MinLengthAttribute>();
				var required = propertyInfo.GetCustomAttribute<RequiredAttribute>();
				var editable = propertyInfo.GetCustomAttribute<EditableAttribute>();
				var len = propertyInfo.GetCustomAttribute<StringLengthAttribute>();

				InputType textType = InputType.Text;
				if (datatype != null)
				{
					textType = MapDataType(datatype.DataType);
				}

				fieldData = new FieldData(propertyInfo.Name, display?.Name ?? propertyInfo.Name, propertyInfo)
				{
					PropertyInfo = propertyInfo,
					Description = display?.Description,
					DataType = datatype?.DataType ?? DataType.Custom,
					Format = datatype?.DisplayFormat?.DataFormatString,
					Range = (range?.Minimum.ToString() ?? "", range?.Maximum.ToString() ?? ""),
					Length = (minLen?.Length ?? len?.MaximumLength ?? 0, maxLen?.Length ?? len?.MaximumLength ?? int.MaxValue),
					Order = display?.GetOrder() ?? int.MaxValue,
					Placeholder = display?.Prompt,
					ReadOnly = !(editable?.AllowEdit ?? key == null),
					Required = required != null,
					TextType = textType,
				};
			}
			return fieldData;
		}

		private void BuildComponent<TValue>(RenderTreeBuilder builder, object model, FieldData fieldData)
		{
			// build up the access method
			var constant = Expression.Constant(model, model.GetType());
			var exp = Expression.Property(constant, fieldData.Name);
			var valueExpression = Expression.Lambda<Func<TValue>>(exp);

			// build the event callback
			var valueChanged =
				Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck(
					EventCallback.Factory.Create<TValue?>(
						this,
						EventCallback.Factory.CreateInferred(
							this,
							__value => fieldData.PropertyInfo.SetValue(model, __value),
							(TValue)fieldData.PropertyInfo.GetValue(model)
						)
					)
				);

			// put our surrogate component in the render pipeline
			// parsing in the needed variable that will be loading into it as params by the infrastructure (magic)
			var elementType = typeof(PlaceHolderComponent<TValue>);
			builder.OpenComponent(0, elementType);
			builder.AddAttribute(1, nameof(PlaceHolderComponent<TValue>.FieldData), fieldData);
			builder.AddAttribute(2, nameof(PlaceHolderComponent<TValue>.Value), fieldData.PropertyInfo.GetValue(model));
			builder.AddAttribute(3, nameof(PlaceHolderComponent<TValue>.ValueExpression), valueExpression);
			builder.AddAttribute(4, nameof(PlaceHolderComponent<TValue>.ValueChanged), valueChanged);
			builder.CloseComponent();
		}

		static InputType MapDataType(DataType type)
		{
			switch (type)
			{
				case DataType.Custom:
					return InputType.Text;
				case DataType.DateTime:
					return InputType.DatetimeLocal;
				case DataType.Date:
					return InputType.Date;
				case DataType.Time:
					return InputType.Time;
				case DataType.Duration:
					return InputType.Text;
				case DataType.PhoneNumber:
					return InputType.Tel;
				case DataType.Currency:
					return InputType.Number;
				case DataType.Text:
					return InputType.Text;
				case DataType.Html:
					return InputType.Text;
				case DataType.MultilineText:
					return InputType.Text;
				case DataType.EmailAddress:
					return InputType.Email;
				case DataType.Password:
					return InputType.Password;
				case DataType.Url:
					return InputType.Url;
				case DataType.ImageUrl:
					return InputType.Image;
				case DataType.CreditCard:
					return InputType.Text;
				case DataType.PostalCode:
					return InputType.Text;
				case DataType.Upload:
					return InputType.File;
				default:
					return InputType.Text;
			}
		}
	}
}
