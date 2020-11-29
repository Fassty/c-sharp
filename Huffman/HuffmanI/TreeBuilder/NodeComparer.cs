using System;
using System.Collections.Generic;

namespace HuffmanI.TreeBuilder
{
    /// <summary>
    /// Compares nodes by their frequency
    /// If the frequencies are equal, the following rules are applied:
    /// 1) Leaf nodes are lighter than inner nodes
    /// 2) Among leaf nodes, those with lower character value are prioritized
    /// 3) Among inner nodes, those that were created sooner by the algorithm are prioritized
    /// </summary>
    public class NodeComparer : IComparer<INode>
    {
        public int Compare(INode x, INode y)
        {
            if (x.frequency != y.frequency)
            {
                return x.frequency.CompareTo(y.frequency);
            }


            if (x.isLeaf && y.isLeaf)
            {
                return x.character.CompareTo(y.character);
            }

            // Leaf nodes are prioritized over non-leaf nodes
            if (x.isLeaf || y.isLeaf)
            {
                return x.isLeaf ? -1 : 1;
            }

            return x.creationId.CompareTo(y.creationId);
        }
    }
}