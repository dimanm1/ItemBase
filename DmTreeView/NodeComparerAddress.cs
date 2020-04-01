using System.Collections.Generic;

namespace DmTreeNode
{
    public class NodeComparerAddress : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            return string.Compare(x.AddressAsString, y.AddressAsString, false);
        }
    }
}
