using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
    public partial class StatusBar
    {
        protected override Task OnInitializedAsync()
        {
            StatusBarService!.StateChanged += StateChanged;
            return base.OnInitializedAsync();
        }

        private void StateChanged(object? sender, EventArgs e) => StateHasChanged();

        public void Dispose()
        {
            StatusBarService!.StateChanged -= StateChanged;
        }
    }
}
