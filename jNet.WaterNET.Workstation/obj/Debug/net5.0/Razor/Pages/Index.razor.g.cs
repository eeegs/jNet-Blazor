#pragma checksum "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be9194df7fa8d9600f96b183c87b5d69f545664c"
// <auto-generated/>
#pragma warning disable 1591
namespace jNet.WaterNET.Workstation.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using jNet.Client.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using jNet.WaterNET.Workstation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using jNet.WaterNET.Workstation.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\_Imports.razor"
using jNet.WaterNET.Workstation.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
using jNet.Workstation.UI.Layouts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
using jNet.Workstation.UI.General;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/main")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<jNet.Workstation.UI.Layouts.LayoutTypeB>(0);
            __builder.AddAttribute(1, "Settings", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<jNet.Client.Code.Setting>(
#nullable restore
#line 5 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                        settings

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "Tool", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenElement(3, "button");
                __builder2.AddAttribute(4, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 7 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                           Save

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddContent(5, "Save");
                __builder2.CloseElement();
            }
            ));
            __builder.AddAttribute(6, "Left", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<jNet.Workstation.UI.General.Window>(7);
                __builder2.AddAttribute(8, "Title", "Left");
                __builder2.AddAttribute(9, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __Blazor.jNet.WaterNET.Workstation.Pages.Index.TypeInference.CreatejNet_Client_UI_General_AsyncWarpper_0(__builder3, 10, 11, 
#nullable restore
#line 11 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                                                                                 Nodes()

#line default
#line hidden
#nullable disable
                    , 12, (Context) => (__builder4) => {
#nullable restore
#line 12 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                 if (Context is not null)
                {

#line default
#line hidden
#nullable disable
                        __builder4.OpenComponent<jNet.Client.UI.Tree>(13);
                        __builder4.AddAttribute(14, "Nodes", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.IEnumerable<jNet.Client.UI.TreeNode>>(
#nullable restore
#line 13 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                               Context

#line default
#line hidden
#nullable disable
                        ));
                        __builder4.CloseComponent();
#nullable restore
#line 13 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                                                }
                else
                {

#line default
#line hidden
#nullable disable
                        __builder4.AddMarkupContent(15, "<p>Wait for a tree</p>");
#nullable restore
#line 15 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                                        }

#line default
#line hidden
#nullable disable
                    }
                    );
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.AddAttribute(16, "Right", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<jNet.Workstation.UI.General.Window>(17);
                __builder2.AddAttribute(18, "Title", "Right");
                __builder2.AddAttribute(19, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.OpenElement(20, "p");
                    __builder3.AddContent(21, 
#nullable restore
#line 22 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.WaterNET.Workstation\Pages\Index.razor"
                SelectedDefinition?.Name

#line default
#line hidden
#nullable disable
                    );
                    __builder3.CloseElement();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
namespace __Blazor.jNet.WaterNET.Workstation.Pages.Index
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreatejNet_Client_UI_General_AsyncWarpper_0<T>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Threading.Tasks.Task<T> __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment<T> __arg1)
        {
        __builder.OpenComponent<global::jNet.Client.UI.General.AsyncWarpper<T>>(seq);
        __builder.AddAttribute(__seq0, "DataPromise", __arg0);
        __builder.AddAttribute(__seq1, "ChildContent", __arg1);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
