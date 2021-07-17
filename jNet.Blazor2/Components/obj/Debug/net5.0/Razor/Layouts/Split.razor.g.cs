#pragma checksum "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59c31d0eb8e6ab59e28d3bdfc05b61a6230de2fd"
// <auto-generated/>
#pragma warning disable 1591
namespace jNet.Blazor2.Components.Layouts
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
using System.Drawing;

#line default
#line hidden
#nullable disable
    public partial class Split : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "divider" + " " + (
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                      IsVertical?"vertical":"horizontal"

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "style", (
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
              IsVertical?"height":"width"

#line default
#line hidden
#nullable disable
            ) + ":" + " " + (
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                                              SplitWidth

#line default
#line hidden
#nullable disable
            ) + "px;" + " " + (
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                                                               IsVertical?"top":"left"

#line default
#line hidden
#nullable disable
            ) + ":" + " " + (
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                                                                                           Position

#line default
#line hidden
#nullable disable
            ) + "px;" + " background-color:" + " " + (
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                                                                                                                           SplitColorHex

#line default
#line hidden
#nullable disable
            ) + ";");
            __builder.AddAttribute(3, "onpointerdown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.PointerEventArgs>(this, 
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                       e=>DragManager!.OnPointerDown(Divider, e, Index)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "onpointerup", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.PointerEventArgs>(this, 
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                     e=>DragManager!.OnPointerUp(Divider, e, Index)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "onpointermove", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.PointerEventArgs>(this, 
#nullable restore
#line 7 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
                       e=>DragManager!.OnPointerMove(Divider, e, Index)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "b-1sn2rs8w49");
            __builder.AddElementReferenceCapture(7, (__value) => {
#nullable restore
#line 4 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
            Divider = __value;

#line default
#line hidden
#nullable disable
            }
            );
#nullable restore
#line 8 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
     if (ShowThumb)
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(8, "<div b-1sn2rs8w49></div>");
#nullable restore
#line 11 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 15 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Components\Layouts\Split.razor"
 
    ElementReference Divider;
    [Parameter] public int Index { get; set; } = 0;
    [Parameter] public bool IsVertical { get; set; } = false;
    [Parameter] public bool ShowThumb { get; set; } = true;
    [Parameter] public double Position { get; set; } = 50;
    [Parameter] public int SplitWidth { get; set; } = 5;
    [Parameter] public Color SplitColor { get; set; } = Color.Transparent;
    //[Parameter] public EventCallback<double> SplitChanged { get; set; }
    [Parameter] public DragManager? DragManager { get; set; }
    string SplitColorHex => $"#{SplitColor.R:x2}{SplitColor.G:x2}{SplitColor.B:x2}{SplitColor.A:x2}";

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
