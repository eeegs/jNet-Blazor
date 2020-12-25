using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace jNet.Blazor.Parts.Form
{
	internal class PlaceHolderComponent<TValue> : ZZZZZ<TValue>
	{
		public PlaceHolderComponent()
		{
		}

		[Parameter] public FieldData? FieldData { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			base.BuildRenderTree(builder);
			if (FieldData != null)
			{
				int i = 0;

				builder.OpenElement(i++, "label");
				builder.AddAttribute(i++, "for", FieldData.Id);
				builder.AddContent(i++, FieldData.Label);
				builder.CloseElement();
				i = 0;
				builder.OpenElement(i++, "input");
				//builder.AddAttribute(i++, "disabled", FieldData.Disabled);
				builder.AddMultipleAttributes(i++, AdditionalAttributes);
				//builder.AddAttribute(i++, "class", CssClass);
				//builder.AddAttribute(i++, "type", FieldData.TextType.ToString());
				builder.AddAttribute(i++, "id", FieldData.Id);
				//builder.AddAttribute(i++, "placeholder", FieldData.Placeholder);
				//builder.AddAttribute(i++, "title", FieldData.Description);
				builder.AddAttribute(i++, "name", FieldData.Name);
				//builder.AddAttribute(i++, "readonly", FieldData.ReadOnly);
				//builder.AddAttribute(i++, "required", FieldData.Required);
				builder.AddAttribute(i++, "value", BindConverter.FormatValue(CurrentValueAsString));
				builder.AddAttribute(i++, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
				builder.CloseElement();
			}
		}

		protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
		{
			if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.InvariantCulture, out result))
			{
				validationErrorMessage = null;
				return true;
			}
			else
			{
				validationErrorMessage = string.Format(CultureInfo.InvariantCulture, $"Could not pass '{value}' into {{0}} of type {typeof(TValue).FullName}.", DisplayName ?? FieldIdentifier.FieldName);
				return false;
			}
		}
	}
}
