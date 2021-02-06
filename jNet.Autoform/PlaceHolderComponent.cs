using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace jNet.Autoform
{
	internal class PlaceHolderComponent<TValue> : InputBase<TValue>
	{
		[Parameter] public FieldData? FieldData { get; set; }
		[Parameter] public bool ReadOnly { get; set; } = default;
		[Parameter] public bool UseValidationMessages { get; set; } = true;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			base.BuildRenderTree(builder);

			if (FieldData == null) return;


			int i = 0;
			var postLable = FieldData.TextType == InputType.Radio || FieldData.TextType == InputType.Checkbox;

			Action doLable = () =>
			{
				builder.OpenElement(i++, "label");
				builder.AddAttribute(i++, "for", FieldData.Id);
				builder.AddContent(i++, FieldData.Label);
				builder.CloseElement();
			};

			if (!FieldData.Hidden)
			{
				builder.OpenElement(i++, "div");
				builder.AddAttribute(i++, "class", $"form-group {string.Join(' ', FieldData.CSS)}");
				if (!postLable)
				{
					doLable();
				}
			}

			builder.OpenElement(i++, "input");
			builder.AddAttribute(i++, "disabled", FieldData.Disabled);
			builder.AddMultipleAttributes(i++, AdditionalAttributes);
			builder.AddAttribute(i++, "class", "form-control " + CssClass);
			builder.AddAttribute(i++, "type", FieldData.TextType.ToString());
			builder.AddAttribute(i++, "id", FieldData.Id);
			builder.AddAttribute(i++, "placeholder", FieldData.Placeholder);
			builder.AddAttribute(i++, "title", FieldData.Description);
			builder.AddAttribute(i++, "name", FieldData.Name);
			builder.AddAttribute(i++, "readonly", FieldData.ReadOnly || ReadOnly);
			builder.AddAttribute(i++, "hidden", FieldData.Hidden);
			builder.AddAttribute(i++, "required", FieldData.Required);
			builder.AddAttribute(i++, "value", BindConverter.FormatValue(CurrentValueAsString));
			builder.AddAttribute(i++, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
			builder.CloseElement();
			if (postLable)
			{
				doLable();
			}

			if (!FieldData.Hidden)
			{
				if (UseValidationMessages)
				{
					builder.OpenComponent(i++, typeof(ValidationMessage<TValue>));
					builder.AddAttribute(i++, nameof(ValidationMessage<TValue>.For), ValueExpression);
					builder.CloseComponent();
				}
				builder.CloseElement();
			}
		}

		protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
		{
			if (FieldData!.TextType == InputType.Number)
			{
				value = new string((value ?? "").Where(q => Char.IsDigit(q) || q == '.' || q == ',').ToArray());
			}

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

		protected override string? FormatValueAsString(TValue? value) => string.Format($"{{0:{FieldData!.Format}}}", value);
	}
}
