// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace jNet.Blazor.Parts.Form
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldText - Copy.razor"
using System.Diagnostics.CodeAnalysis;

#line default
#line hidden
#nullable disable
    public partial class FieldText___Copy : MyInputBase<string?>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Form\FieldText - Copy.razor"
       
    // placeholder="@Placeholder" title="@Title" readonly="@ReadOnly" required="@Required" disabled="@IsAKey"

    protected override bool TryParseValueFromString(string? value, out string? result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value;
        validationErrorMessage = null;
        return true;
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591