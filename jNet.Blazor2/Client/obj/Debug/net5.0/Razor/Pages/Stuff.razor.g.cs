#pragma checksum "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c1dd45531e521957a815b25b836bffd8202694c"
// <auto-generated/>
#pragma warning disable 1591
namespace jNet.Blazor2.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using jNet.Blazor2.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using jNet.Blazor2.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\_Imports.razor"
using Microsoft.Fast.Components.FluentUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
using jNet.Blazor2.Components.Layouts;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/stuff")]
    public partial class Stuff : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "b-i4n026phif");
            __builder.OpenComponent<jNet.Blazor2.Components.Layouts.MultiSplit>(2);
            __builder.AddAttribute(3, "IsVertical", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                             Direction

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "SplitColor", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                    System.Drawing.Color.Red

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "Splits", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double[]>(
#nullable restore
#line 5 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                                             Splits

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(6, "SplitsChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Double[]>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Double[]>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => Splits = __value, Splits))));
            __builder.AddAttribute(7, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(8, "\r\n        hello\r\n    ");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n");
            __builder.OpenElement(10, "button");
            __builder.AddAttribute(11, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 9 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                  ()=>Flip=!Flip

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "b-i4n026phif");
            __builder.AddContent(13, "Flip");
            __builder.CloseElement();
            __builder.AddMarkupContent(14, "\r\n");
            __builder.OpenElement(15, "button");
            __builder.AddAttribute(16, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 10 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                  ()=>Direction=!Direction

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "b-i4n026phif");
            __builder.AddContent(18, "Direction");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n");
            __builder.OpenElement(20, "button");
            __builder.AddAttribute(21, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 11 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                  ()=>CanClose=!CanClose

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(22, "b-i4n026phif");
            __builder.AddContent(23, "CanClose");
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n");
            __builder.OpenElement(25, "button");
            __builder.AddAttribute(26, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                   Save

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "b-i4n026phif");
            __builder.AddContent(28, "Save");
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n");
            __builder.OpenElement(30, "p");
            __builder.AddAttribute(31, "b-i4n026phif");
            __builder.AddContent(32, "Splits: { ");
            __builder.AddContent(33, 
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
              Splits[0]

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(34, ", ");
            __builder.AddContent(35, 
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                          Splits[1]

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(36, ", ");
            __builder.AddContent(37, 
#nullable restore
#line 13 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                      Splits[2]

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(38, " }");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n");
            __builder.OpenElement(40, "div");
            __builder.AddAttribute(41, "style", "background-color:azure");
            __builder.AddAttribute(42, "b-i4n026phif");
            __builder.OpenComponent<jNet.Blazor2.Components.Layouts.SplitPanel>(43);
            __builder.AddAttribute(44, "IsVertical", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 15 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                    Direction

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(45, "SplitColor", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 15 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                                           System.Drawing.Color.White

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(46, "Split", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 15 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                              this["A"].SplitPosition

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(47, "SplitChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Double>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Double>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => this["A"].SplitPosition = __value, this["A"].SplitPosition))));
            __builder.AddAttribute(48, "PanelA", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<jNet.Blazor2.Components.Layouts.SplitPanel>(49);
                __builder2.AddAttribute(50, "IsVertical", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                                 !Direction

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(51, "SplitColor", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                                                          System.Drawing.Color.White

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(52, "Split", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 17 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                          this["B"].SplitPosition

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(53, "SplitChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Double>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Double>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => this["B"].SplitPosition = __value, this["B"].SplitPosition))));
                __builder2.AddAttribute(54, "PanelA", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddContent(55, "A");
                }
                ));
                __builder2.AddAttribute(56, "PanelB", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddContent(57, "B");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.AddAttribute(58, "PanelB", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<jNet.Blazor2.Components.Layouts.SplitPanel>(59);
                __builder2.AddAttribute(60, "IsVertical", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 23 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                                     !Direction

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(61, "SplitColor", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Drawing.Color>(
#nullable restore
#line 23 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                                                                                              System.Drawing.Color.White

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(62, "Split", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Double>(
#nullable restore
#line 23 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor2\Client\Pages\Stuff.razor"
                                          this["Right"].SplitPosition

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(63, "SplitChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Double>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Double>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => this["Right"].SplitPosition = __value, this["Right"].SplitPosition))));
                __builder2.AddAttribute(64, "PanelA", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddContent(65, "C");
                }
                ));
                __builder2.AddAttribute(66, "PanelB", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddContent(67, "D");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
