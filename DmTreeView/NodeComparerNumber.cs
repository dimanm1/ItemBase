using System.Collections.Generic;

namespace DmTreeNode
{
    public class NodeComparerNumber : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x.Number < y.Number)
            {
                return -1;
            }

            if (x.Number > y.Number)
            {
                return 1;
            }

            return 0;
        }
    }
}
