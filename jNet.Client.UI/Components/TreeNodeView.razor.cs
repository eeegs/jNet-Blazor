using Microsoft.AspNetCore.Components;
using System;
using System.Text;
using System.Threading.Tasks;

namespace jNet.Client.UI
{


    public partial class TreeNodeView
    {
        [Parameter]
        public TreeNode Node { get; set; } = new ();
    }
}
