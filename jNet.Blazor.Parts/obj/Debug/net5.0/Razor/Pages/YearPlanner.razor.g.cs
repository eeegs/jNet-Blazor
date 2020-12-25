#pragma checksum "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c266aa1a67fd7e4338d2b4955be56310f25a075b"
// <auto-generated/>
#pragma warning disable 1591
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
            __builder.AddMarkupContent(0, "<h3 b-n5lcsj7omt>YearPlanner</h3>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "planner-year");
            __builder.AddAttribute(3, "b-n5lcsj7omt");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "planner-month heading");
            __builder.AddAttribute(6, "b-n5lcsj7omt");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "planner-day heading");
            __builder.AddAttribute(9, "b-n5lcsj7omt");
            __builder.AddContent(10, 
#nullable restore
#line 8 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
                                           $"{year:0000}"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 9 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
         for (int i = 0; i < 31; i++)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(11, "div");
            __builder.AddAttribute(12, "class", "planner-day");
            __builder.AddAttribute(13, "b-n5lcsj7omt");
            __builder.AddContent(14, 
#nullable restore
#line 11 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
                                       i+1

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 12 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 14 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
     for (int m = 0; m < months; m++)
    {
        var mnth = new DateTime(year, m + 1, 1);
        var oset = (int)mnth.DayOfWeek - startday;
        var dim = DateTime.DaysInMonth(year, m + 1);


#line default
#line hidden
#nullable disable
            __builder.OpenElement(15, "div");
            __builder.AddAttribute(16, "class", "planner-month");
            __builder.AddAttribute(17, "b-n5lcsj7omt");
            __builder.OpenElement(18, "div");
            __builder.AddAttribute(19, "class", "planner-day heading");
            __builder.AddAttribute(20, "b-n5lcsj7omt");
            __builder.AddContent(21, 
#nullable restore
#line 21 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
                                      mnth.ToString("yyyy-MMM")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 22 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
     for (int d = 0; d < dim; d++)
    {
        var day = mnth.AddDays(d);
        var i = d + oset;
        if (day.DayOfWeek == we1 || day.DayOfWeek == we2)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(22, "div");
            __builder.AddAttribute(23, "class", "planner-day wend");
            __builder.AddAttribute(24, "b-n5lcsj7omt");
            __builder.OpenElement(25, "span");
            __builder.AddAttribute(26, "b-n5lcsj7omt");
            __builder.AddContent(27, 
#nullable restore
#line 30 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
                     day.DayOfWeek.ToString()[0..1]

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 33 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
        }
        else
        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(28, "div");
            __builder.AddAttribute(29, "class", "planner-day");
            __builder.AddAttribute(30, "b-n5lcsj7omt");
            __builder.OpenElement(31, "span");
            __builder.AddAttribute(32, "b-n5lcsj7omt");
            __builder.AddContent(33, 
#nullable restore
#line 38 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
                     day.DayOfWeek.ToString()[0..1]

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 41 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
        }
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 44 "C:\Users\sscot\OneDrive\Development Files\Projects\jNet\jNet.Blazor.Parts\Pages\YearPlanner.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
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