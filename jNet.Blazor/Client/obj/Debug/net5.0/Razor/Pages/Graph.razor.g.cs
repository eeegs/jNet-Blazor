#pragma checksum "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a012a8606dc88a8f11e25a5a99244a6db434ee0d"
// <auto-generated/>
#pragma warning disable 1591
namespace jNet.Blazor.Client.Pages
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using jNet.Blazor.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using jNet.Blazor.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using jNet.Blazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using jNet.Blazor.Parts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using jNet.Blazor.Parts.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\_Imports.razor"
using jNet.Autoform;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
using System.Drawing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
using System;

#line default
#line hidden
#nullable disable
    public partial class Graph : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "p");
            __builder.OpenElement(1, "button");
            __builder.AddAttribute(2, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 4 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                       ()=> { Run(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(3, "Run");
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\r\n");
            __builder.OpenElement(5, "button");
            __builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                    ()=> { RunAnimation = false; }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(7, "Pause");
            __builder.CloseElement();
            __builder.AddMarkupContent(8, "\r\n");
            __builder.AddContent(9, 
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
 count

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(10, "\r\n\r\n");
            __builder.OpenElement(11, "svg");
            __builder.AddAttribute(12, "width", "100%");
            __builder.AddAttribute(13, "height", "100%");
#nullable restore
#line 9 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
     foreach (var c in circles)
    {
        var l = c;

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<jNet.Blazor.Client.Pages.Sizable>(14);
            __builder.AddAttribute(15, "Stroke", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                   c.Color

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(16, "StrokeWidth", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                                         6

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "Position", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Numerics.Vector2>(
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                                                       c.Location

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(18, "Radius", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                c.Radius

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "RadiusChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Double>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Double>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => c.Radius = __value, c.Radius))));
            __builder.CloseComponent();
            __builder.AddMarkupContent(20, "\r\n        ");
            __builder.OpenComponent<jNet.Blazor.Client.Pages.Draggable>(21);
            __builder.AddAttribute(22, "Radius", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                             c.Radius-3

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(23, "Fill", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                 c.Color

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "Text", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                                                              c.Text

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(25, "FocusChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Boolean>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Boolean>(this, 
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                                                                                    x=>Focused(x , l)

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(26, "Position", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Numerics.Vector2>(
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                                           c.Location

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "PositionChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Numerics.Vector2>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Numerics.Vector2>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => c.Location = __value, c.Location))));
            __builder.CloseComponent();
#nullable restore
#line 14 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
     foreach (var j in joins)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<jNet.Blazor.Client.Pages.Connector2>(28);
            __builder.AddAttribute(29, "Left", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<jNet.Blazor.Client.Pages.Join>(
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                           j.Left

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(30, "Right", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<jNet.Blazor.Client.Pages.Join>(
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                           j.Right

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(31, "Fill", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
                                                           Color.DarkSlateBlue

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
#nullable restore
#line 18 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Graph.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
