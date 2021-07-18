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

namespace jNet.Autoform
{
	public class AutoField<TModel> : ComponentBase
	{
		//protected internal const string AutoFormId = "1CC141A3-BF9F-4B21-832A-DC7A1B1FE667";
		private EntityCache<TModel> entityModel = new EntityCache<TModel>();
		private static MethodInfo? methodBC;

		[Parameter] public TModel? Model { get; set; }
		[Parameter] public EventCallback<TModel> ModelChanged { get; set; }
		[Parameter] public bool? ReadOnly { get; set; } = default;
		[Parameter] public bool? UseValidationMessages { get; set; } = true;

		[CascadingParameter] EditContext? CascadedEditContext { get; set; }

		protected override void OnInitialized()
		{
			if (CascadedEditContext == null)
			{
				throw new InvalidOperationException($"{nameof(AutoField<TModel>)} requires a cascading " +
					$"parameter of type {nameof(EditContext)}. For example, you can use {nameof(AutoField<TModel>)} " +
					$"inside an EditForm.");
			}
			if (CascadedEditContext.Model != (object?)Model)
			{
				throw new InvalidOperationException($"{nameof(AutoField<TModel>)} requires a parent AutoForm " +
					$"not the default {nameof(EditForm)}. Change the {nameof(EditForm)} to a AutoForm " +
					$"and bind to the {nameof(EditForm.Model)} (eg: @bind-{nameof(EditForm.Model)}=\"YouModelName\")");
			}
			base.OnInitialized();
		}

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			methodBC = this.GetType().GetMethod(nameof(BuildComponent), BindingFlags.NonPublic | BindingFlags.Instance)!;
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			//base.BuildRenderTree(builder);

			// We use the cached FieldData information for this model to render components with all the details.
			// A surrogate item based on InputBase<T> is built into the Render Pipline in the following code.
			// This surrogate, PlaceHolderComponent<T>, then renders the detailed HTML
			// but only if there is a model...
			if (Model is not null)
			{
					foreach (var fd in entityModel.Where(q=>q.Scaffold == true))
					{
						// for each cached, therefore to be built, field in the model get a generic method based on the property type
						var generic = methodBC!.MakeGenericMethod(fd.PropertyInfo.PropertyType);

						// invoke the method to build the surrogate component
						// == BuildComponent<PropertyType>(builder, model, fieldData)
						generic.Invoke(this, new object[] { builder, fd });
					}
			}
		}

		private void BuildComponent<FValue>(RenderTreeBuilder builder, FieldData fieldData)
		{
			FValue value = (FValue)fieldData.PropertyInfo.GetValue(Model);
			
			// build up the access method
			var constant = Expression.Constant(Model, Model!.GetType());
			var exp = Expression.Property(constant, fieldData.Name);
			var valueExpression = Expression.Lambda<Func<FValue>>(exp);

			// build the event callback, include a notification that the field has updated as well as the model
			var valueChanged =
				Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck(
					EventCallback.Factory.Create<FValue?>(
						this,
						EventCallback.Factory.CreateInferred(
							this,
							__value =>
							{
								fieldData.PropertyInfo.SetValue(Model, __value);
								ModelChanged.InvokeAsync(Model);
							},
							value
						)
					)
				);

			// put our surrogate component in the render pipeline
			// parsing in the needed variable that will be loading into it as params by the infrastructure (magic)
			var elementType = typeof(PlaceHolderComponent<FValue>);
			builder.OpenComponent(0, elementType);
			builder.AddAttribute(1, nameof(PlaceHolderComponent<FValue>.FieldData), fieldData);
			builder.AddAttribute(2, nameof(PlaceHolderComponent<FValue>.Value), value);
			builder.AddAttribute(3, nameof(PlaceHolderComponent<FValue>.ValueExpression), valueExpression);
			builder.AddAttribute(4, nameof(PlaceHolderComponent<FValue>.ValueChanged), valueChanged);
			builder.AddAttribute(5, nameof(PlaceHolderComponent<FValue>.ReadOnly), ReadOnly);
			builder.AddAttribute(6, nameof(PlaceHolderComponent<FValue>.UseValidationMessages), UseValidationMessages);
			builder.CloseComponent();
		}
	}
}
