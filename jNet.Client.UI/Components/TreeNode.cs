using System;
using System.Collections.Generic;
using System.Linq;

namespace jNet.Client.UI
{
    public class TreeNode
    {
        public Action OnSelected { get; set; } = () => { };
        public string Text { get; set; } = "Bad Node";
        public virtual IEnumerable<TreeNode> Children { get; set; } = Enumerable.Empty<TreeNode>();
    }
}
