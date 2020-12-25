// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace jNet.Blazor.Parts.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
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
#line 1 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
    public partial class YearPlanner : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 47 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
       
    private int year;
    private int month;
    private int startday;
    private int months = 12;
    private DayOfWeek we1;
    private DayOfWeek we2;
    private bool dateset = false;

    [Parameter]
    public DateTime StartMonth
    {
        get => new DateTime(year, month, 1);
        set
        {
            year = value.Year;
            month = value.Month;
            dateset = true;
            SetStartDay();
        }
    }

    [Parameter]
    public int Months
    {
        get => months;
        set
        {
            months = value; SetStartDay();
        }
    }

    private void SetStartDay()
    {
        if (months > 0)
        {
            startday = Enumerable.Range(0, months).Min(q => (int)(new DateTime(year, q + 1, 1).DayOfWeek));
        }
    }

    public YearPlanner()
    {
        var a = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        we1 = (DayOfWeek)((a + 5) % 7);
        we2 = (DayOfWeek)((a + 6) % 7);
    }



    protected override void OnParametersSet()
    {
        if (!dateset)
        {
            StartMonth = DateTime.Now;
        }
        base.OnParametersSet();
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
