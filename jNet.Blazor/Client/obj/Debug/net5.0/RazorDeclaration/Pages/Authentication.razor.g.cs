// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace jNet.Blazor.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Authentication.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Authentication.razor"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/authentication/{action}")]
    public partial class Authentication : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 6 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor\Client\Pages\Authentication.razor"
      
    [Parameter] public string Action { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState> AuthenticationState { get; set; }


    public async void OnLogInSucceeded()
    {
        var user = (await AuthenticationState).User;

        if (user.Identity.IsAuthenticated)
        {
            // Do some stuff
        }
    }

    public void OnLogOutSucceeded()
    {
        // Do some stuff
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
