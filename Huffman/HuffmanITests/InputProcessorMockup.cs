using System;
using System.Collections.Generic;
using System.IO;
using HuffmanI;
using HuffmanI.InputProcessor;
using HuffmanI.TreeBuilder;

namespace HuffmanITests
{
    public class InputProcessorMockup:IInputProcessor
    {
        public SortedSet<INode> ProcessInput()
        {
            var dict = GetUniqueBytes();
            var leafTree = InitializeTree(dict);
            return leafTree;
        }

        long[] IInputProcessor.GetUniqueBytes()
        {
            throw new NotImplementedException();
        }

        public SortedSet<INode> InitializeTree(long[] freqArray)
        {
            throw new NotImplementedException();
        }

        public Dictionary<byte, int> GetUniqueBytes()
        {
            var dict = new Dictionary<byte, int>();

            int c = (byte) 'a';

            for (var i = 0; i < 10; i++)
            {
                dict.Add(Convert.ToByte(c+i),4);
            }

            return dict;
        }
        
        // The exact same method as in InputProcessor, but doesn't require StreamReader
        public SortedSet<INode> InitializeTree(Dictionary<byte, int> dict)
        {
            var tree = new SortedSet<INode>(new NodeComparer());
            foreach (var element in dict)
            {
                INode node = new Leaf(element.Key, element.Value);
                tree.Add(node);
            }

            dict.Clear();
            return tree;
        }

        public SortedSet<INode> BuildSimpleTree()
        {
            var dict = new Dictionary<byte, int>();
            dict.Add(Convert.ToByte('a'),3);
            dict.Add(Convert.ToByte('b'), 2);
            dict.Add(Convert.ToByte('c'), 1);

            var tree = InitializeTree(dict);
            return tree;
        }
    }
}