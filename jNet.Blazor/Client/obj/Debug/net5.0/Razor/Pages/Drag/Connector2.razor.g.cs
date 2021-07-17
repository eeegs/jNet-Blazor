#pragma checksum "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "63fe67b660e7b56cf4f34e5c42153a2ab11d0f6e"
// <auto-generated/>
#pragma warning disable 1591
namespace jNet.Blazor.Client.Pages
{
    #line hidden
    using System;
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
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
using System.Drawing;

#line default
#line hidden
#nullable disable
    public partial class Connector2 : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
  
    // in screen space y is upside down so its negated before use

    var rise = -Right.Coord.Location.Y + Left.Coord.Location.Y;
    var run = Right.Coord.Location.X - Left.Coord.Location.X;

    var angle = Math.Atan2(rise, run) / Tau;

    angle = Math.Round(angle * 8) / 8;
    var angleL = angle * Tau;
    var angleR = (0.5 + angle) * Tau;

    var dist = Math.Sqrt(rise * rise + run * run);

    var acl = Math.Cos(angleL);
    var asl = Math.Sin(angleL);
    var acr = Math.Cos(angleR);
    var asr = Math.Sin(angleR);

    var x1 = Left.Coord.Location.X + Left.Coord.Radius * acl;
    var y1 = Left.Coord.Location.Y - Left.Coord.Radius * asl;
    var x2 = Right.Coord.Location.X + Right.Coord.Radius * acr;
    var y2 = Right.Coord.Location.Y - Right.Coord.Radius * asr;
    var x1a = Left.Coord.Location.X + dist / 3 * acl;
    var y1a = Left.Coord.Location.Y - dist / 3 * asl;
    var x2a = Right.Coord.Location.X + dist / 3 * acr;
    var y2a = Right.Coord.Location.Y - dist / 3 * asr;


#line default
#line hidden
#nullable disable
            __builder.OpenElement(0, "circle");
            __builder.AddAttribute(1, "cx", 
#nullable restore
#line 31 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                 x1

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(2, "cy", 
#nullable restore
#line 31 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                          y1

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(3, "r", "3");
            __builder.AddAttribute(4, "fill", "gray");
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\r\n    ");
            __builder.OpenElement(6, "circle");
            __builder.AddAttribute(7, "cx", 
#nullable restore
#line 32 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                 x2

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(8, "cy", 
#nullable restore
#line 32 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                          y2

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(9, "r", "3");
            __builder.AddAttribute(10, "fill", "gray");
            __builder.CloseElement();
            __builder.OpenElement(11, "path");
            __builder.AddAttribute(12, "stroke-width", "1");
            __builder.AddAttribute(13, "stroke", "gray");
            __builder.AddAttribute(14, "fill", "none");
            __builder.AddAttribute(15, "d", "M" + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                           x1

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                               y1

#line default
#line hidden
#nullable disable
            ) + " C" + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                                     x1a

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                                          y1a

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                                               x2a

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                                                    y2a

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                                                         x2

#line default
#line hidden
#nullable disable
            ) + " " + (
#nullable restore
#line 34 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Drag\Connector2.razor"
                                                                                             y2

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
