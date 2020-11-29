using System.Collections.Generic;
using HuffmanI.TreeBuilder;

namespace HuffmanI.InputProcessor
{
    interface IInputProcessor
    {
        SortedSet<INode> ProcessInput();
        long[] GetUniqueBytes();
        SortedSet<INode> InitializeTree(long[] freqArray);
    }
}