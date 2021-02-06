#pragma checksum "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f3fbd6ccbdcbd9071a9d881b3d09b64fddc9ce66"
// <auto-generated/>
#pragma warning disable 1591
namespace jNet.Blazor.Parts
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
using System.Diagnostics.CodeAnalysis;

#line default
#line hidden
#nullable disable
    public partial class FieldFormated<TValue> : MyInputBase<TValue>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "label");
            __builder.AddAttribute(1, "for", "Id");
            __builder.AddContent(2, 
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
                 Label

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(3, "\r\n");
            __builder.OpenElement(4, "input");
            __builder.AddAttribute(5, "type", "text");
            __builder.AddMultipleAttributes(6, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, object>>>(
#nullable restore
#line 7 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
                               Attributes

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "class", 
#nullable restore
#line 7 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
                                                 CssClass

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(8, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 7 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
                                                                CurrentValueAsString

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(9, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 8 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldFormated.razor"
       
    // id="@GetHashCode()" placeholder="@Placeholder" title="@Title" readonly="@ReadOnly" required="@Required" disabled="@IsAKey"

    [Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field must be a number.";
    [Parameter] public string? FormatString { get; set; } = null;

    //private readonly static string _stepAttributeValue; // Null by default, so only allows whole numbers as per HTML spec

    static FieldFormated()
    {

        // Unwrap Nullable<T>, because InputBase already deals with the Nullable aspect
        // of it for us. We will only get asked to parse the T for nonempty inputs.
        var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        if (targetType == typeof(int) ||
            targetType == typeof(long) ||
            targetType == typeof(decimal) ||
            targetType == typeof(double) ||
            targetType == typeof(float) ||
            targetType == typeof(short) ||
            targetType == typeof(byte))
        {
            //_stepAttributeValue = "any";
        }
        else
        {
            throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
        }
    }


    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        var fixedVal = new string((value ?? "").Where(Char.IsDigit).ToArray());

        if (BindConverter.TryConvertTo<TValue>(fixedVal, CultureInfo.InvariantCulture, out result))
        {
            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayName ?? FieldIdentifier.FieldName);
            return false;
        }
    }

    protected override string? FormatValueAsString(TValue? value) => string.Format($"{{0:{FormatString}}}", value);

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
