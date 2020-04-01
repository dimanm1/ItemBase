using System.Collections.Generic;

namespace DmTreeNode
{
    public class NodeComparerName : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            return string.Compare(x.Name, y.Name, false);
        }
    }
}
