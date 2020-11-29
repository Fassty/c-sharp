using System.Collections;
using System.Collections.Generic;

namespace HuffmanI.TreeBuilder
{
    interface ITreeBuilder
    {
        SortedSet<INode> tree { get; set; }
        void BuildTree();
        bool BuildTreeFromFile();
    }
}