using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Accounts.Components
{
    public partial class MenuBar
    {
        private string opened = "";

        protected override Task OnInitializedAsync()
        {
            MenuBarService!.StateChanged += StateChanged;
            return base.OnInitializedAsync();
        }

        void SetOpened(string menu)
        {
            if (opened != menu)
            {
                opened = menu;
                StateHasChanged();
            }
        }

        private void StateChanged(object? sender, EventArgs e) => StateHasChanged();

        public void Dispose()
        {
            MenuBarService!.StateChanged -= StateChanged;
        }

        void OnDown(PointerEventArgs args, string menu)
        {
            if (opened != menu)
            {
                SetOpened(menu);
            }
            else
            {
                if (opened != "")
                {
                    SetOpened("");
                };
            }
        }

        void OnOver(PointerEventArgs args, string menu)
        {
            if (opened != "" && opened != menu)
            {
                SetOpened(menu);
            }
        }

        void OnLostfocus(FocusEventArgs args)
        {
            if (opened != null)
            {
                SetOpened("");
            }
        }
    }
}
