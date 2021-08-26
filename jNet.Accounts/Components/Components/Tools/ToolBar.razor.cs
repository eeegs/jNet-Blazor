using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
    public partial class ToolBar
    {
        [Inject] ToolBarService? ToolBarService { get; set; }

        protected override Task OnInitializedAsync()
        {
            ToolBarService!.StateChanged += StateChanged;
            return base.OnInitializedAsync();
        }

        private void StateChanged(object? sender, EventArgs e) => StateHasChanged();

        public void Dispose()
        {
            ToolBarService!.StateChanged -= StateChanged;
        }
    }
}
