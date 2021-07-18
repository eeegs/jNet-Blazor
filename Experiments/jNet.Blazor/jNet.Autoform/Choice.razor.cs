using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Autoform
{
	public partial class Choice<TField>
	//where TField: struct, System.Enum  - can't do this the infrastructure won't build a constrained generic
	{
		private bool HasFlags = false;

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		protected override void OnParametersSet()
		{
			if (!typeof(TField).IsEnum)
			{
				throw new InvalidOperationException("TField must be an enum type");
			}

			HasFlags = typeof(TField).GetCustomAttributes(typeof(FlagsAttribute), false).FirstOrDefault() != null;
			base.OnParametersSet();
		}

		private void RadioChange(ChangeEventArgs e)
		{
			CurrentValue = (TField)System.Enum.Parse(typeof(TField), e.Value.ToString());
		}

		private void CheckChange(ChangeEventArgs e, long value)
		{
			var curVal = MakeLong(CurrentValue);
			if ((bool?)e.Value == true)
			{
				curVal |= value;
			}
			else
			{
				curVal &= ~value;
			}
			CurrentValue = (TField)System.Enum.Parse(typeof(TField), curVal.ToString());
		}

		private static T[] ConvertX<T>(IEnumerable<object> array)
		{
			var x = array.Cast<T>().ToArray();
			return x;
		}


		protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TField result, [NotNullWhen(false)] out string? validationErrorMessage)
		{
			validationErrorMessage = null;
			if (typeof(TField).IsEnum)
			{
				if (!System.Enum.TryParse(typeof(TField), value, out var res))
				{
					validationErrorMessage = $"{value} is not a valid member of the the enum: {typeof(TField).FullName}";
					result = default;
					return false;
				}
				else
				{
					result = (TField)res!;
					return true;
				}
			}
			else
			{
				validationErrorMessage = $"{typeof(TField).FullName} must be an enum.";
				result = default;
				return false;
			}
		}

		private static long MakeLong(TField? value)
		{
			var temp = Convert.ChangeType(value, System.Enum.GetUnderlyingType(typeof(TField)));
			var result = temp switch
			{
				sbyte v => v,
				byte v => v,
				short v => v,
				ushort v => v,
				int v => v,
				uint v => v,
				long v => v,
				ulong v => (long)v,
				_ => 0,
			};
			return result;
		}
	}
}
