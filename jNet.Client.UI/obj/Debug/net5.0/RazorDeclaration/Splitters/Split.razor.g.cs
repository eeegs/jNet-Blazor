// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace jNet.Client.UI.Splitters
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.Client.UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.Client.UI\Splitters\Split.razor"
using System.Drawing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.Client.UI\Splitters\Split.razor"
using jNet.Client.UI.General;

#line default
#line hidden
#nullable disable
    public partial class Split : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 17 "C:\Users\PeterGiles\source\github\jNet-Blazor\jNet.Client.UI\Splitters\Split.razor"
 
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
