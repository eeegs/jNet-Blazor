using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace jNet.Autoform
{
	public partial class InputGeneric: ComponentBase
	{
		private static object locker = new object();
		private static MethodInfo? methodBC;
		private MethodInfo? methodGeneric;

		[Parameter] public bool ReadOnly { get; set; } = default;
		[Parameter] public FieldData? Field { get; set; }
		[CascadingParameter] EditContext? CascadedEditContext { get; set; }
		[Parameter] public bool UseValidationMessages { get; set; } = true;

		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			lock (locker)
			{
				methodBC ??= this.GetType().GetMethod(nameof(BuildComponent), BindingFlags.NonPublic | BindingFlags.Instance)!;
			}
			if (Field != null)
			{
				// For the Field in the model get a generic method based on the property type
				methodGeneric ??= methodBC!.MakeGenericMethod(Field.PropertyInfo.PropertyType);
			}
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder) => methodGeneric?.Invoke(this, new object[] { builder });

		private void BuildComponent<FValue>(RenderTreeBuilder builder)
		{
			var model = CascadedEditContext!.Model;

			// build up the access method
			FValue value = (FValue)Field!.PropertyInfo.GetValue(model);

			var constant = Expression.Constant(model, model.GetType());
			var exp = Expression.Property(constant, Field!.Name);
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
								Field.PropertyInfo.SetValue(model, __value);
							},
							value
						)
					)
				);

			builder.OpenComponent(0, typeof(InputInternal<FValue>));
			builder.AddAttribute(1, nameof(InputInternal<FValue>.Field), Field);
			builder.AddAttribute(2, nameof(InputInternal<FValue>.ReadOnly), ReadOnly);
			builder.AddAttribute(3, nameof(InputInternal<FValue>.Value), value);
			builder.AddAttribute(4, nameof(InputInternal<FValue>.ValueExpression), valueExpression);
			builder.AddAttribute(5, nameof(InputInternal<FValue>.ValueChanged), valueChanged);
			builder.AddAttribute(6, nameof(InputInternal<FValue>.UseValidationMessages), UseValidationMessages);
			builder.CloseComponent();
		}
	}
}
