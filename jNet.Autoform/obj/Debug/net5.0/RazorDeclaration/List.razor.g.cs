// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace jNet.Autoform
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Autoform\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Autoform\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Autoform\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
    public partial class List<TKey> : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 48 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Autoform\List.razor"
 
    [Parameter] public FieldData? Key { get; set; }
    [Parameter] public IEnumerable<FieldData>? Fields { get; set; }
    [Parameter] public IEnumerable<object>? Data { get; set; }
    [Parameter] public Func<TKey?, string>? ID2URI { get; set; }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
