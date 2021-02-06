using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace jNet.Autoform
{
	public partial class InputInternal<TField>
	{
		private static Dictionary<string, object> _attributes = new();
		private Dictionary<string, object> attributes = _attributes;
		protected override void OnParametersSet()
		{
			base.OnParametersSet();

			attributes = new Dictionary<string, object>(AdditionalAttributes ?? _attributes);

			attributes["disabled"] = Field.Disabled;
			attributes["class"] = "form-control " + CssClass;
			attributes["type"] = Field.TextType.ToString();
			attributes["id"] = Field.Id;
			attributes["placeholder"] = Field.Placeholder ?? "";
			attributes["title"] = Field.Description ?? "";
			attributes["name"] = Field.Name;
			attributes["readonly"] = Field.ReadOnly || ReadOnly;
			attributes["hidden"] = Field.Hidden;
			attributes["required"] = Field.Required;
		}

		private void Change(ChangeEventArgs e)
		{
			CurrentValueAsString = e.Value?.ToString();
		}

		private void Input(ChangeEventArgs e)
		{
			if (FireOnInput)
			{
				CurrentValueAsString = e.Value?.ToString();
			}
		}

		protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TField result, [NotNullWhen(false)] out string? validationErrorMessage)
		{
			if (Field!.TextType == InputType.Number)
			{
				value = new string((value ?? "").Where(q => Char.IsDigit(q) || q == '.' || q == ',').ToArray());
			}

			if (BindConverter.TryConvertTo<TField>(value, CultureInfo.InvariantCulture, out result))
			{
				validationErrorMessage = null;
				return true;
			}
			else
			{
				validationErrorMessage = string.Format(CultureInfo.InvariantCulture, $"Could not pass '{value}' into {{0}} of type {typeof(TField).FullName}.", DisplayName ?? FieldIdentifier.FieldName);
				return false;
			}
		}
		protected override string? FormatValueAsString(TField? value) => string.Format($"{{0:{Field!.Format}}}", value);
	}
}
