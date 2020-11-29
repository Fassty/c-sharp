using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HuffmanI.TreeBuilder
{
    public class TreeBuilder:ITreeBuilder
    {
        public SortedSet<INode> tree { get; set; }
        private BinaryReader fileReader;
        int creationId = Int32.MaxValue;

        public TreeBuilder(SortedSet<INode> leafTree, BinaryReader fileReader)
        {
            this.tree = leafTree;
            this.fileReader = fileReader;
        }

        /// <summary>
        /// Creates a Huffman's tree
        /// the tree is complete after exactly number of leaves - 1 iterations
        /// </summary>
        public void BuildTree()
        {
            var iter = tree.Count;
            for (var i = 0; i < iter-1; i++)
            {
                // NullPointerException cannot not occur, 
                // Takes the first two elements of SortedSet and adds them as children of a newly created node
                var left = tree.First();
                tree.Remove(left);
                var right = tree.First();
                tree.Remove(right);

                INode node = new InnerNode(left , right , i+1);
                tree.Add(node);
            }
        }

        public bool BuildTreeFromFile()
        {
            tree = new SortedSet<INode>();
            INode currentNode;
            ulong binaryNode;

            if (!CheckHeader())
            {
                return false;
            }

            binaryNode = fileReader.ReadUInt64(); 
            currentNode = BinaryNodeToNode(binaryNode, null, null); // Root
            if (currentNode is InnerNode)
            {
                currentNode.left = AssignChildren();
                currentNode.right = AssignChildren();
            }

            tree.Add(currentNode);

            // Tree definition does not end with 0
            binaryNode = fileReader.ReadUInt64();
            if (binaryNode != 0)
            {
                return false;
            }
            return true;
        }

        private INode BinaryNodeToNode(ulong code, INode left, INode right)
        {
            ulong nodeMask = 0x0000000000000001;
            ulong frequencyMask = 0x00fffffffffffffe;
            INode newNode;

            int type = (byte)code & (byte)nodeMask;
            ulong frequency = code & frequencyMask;
            frequency >>= 1;
            code >>= 56;
            int character = (byte) code;

            if (type == 0)
            {
                newNode = new InnerNode(left,right,creationId--);    
            }
            else
            {
                newNode = new Leaf((byte)character,(long)frequency);
            }

            return newNode;
        }

        private INode AssignChildren()
        {
            ulong code = fileReader.ReadUInt64();
            INode node = BinaryNodeToNode(code, null, null);
            if (node is InnerNode)
            {
                node.left = AssignChildren();
                node.right = AssignChildren();
            }
            return node;
        }

        private bool CheckHeader()
        {
            ulong header = 7378722948686112891;
            return fileReader.ReadUInt64() == header;
        }

        
    }
}