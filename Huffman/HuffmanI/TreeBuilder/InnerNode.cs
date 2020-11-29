using System;

namespace HuffmanI.TreeBuilder
{
    public class InnerNode: INode
    {
        public INode left { get; set; }
        public INode right { get; set; }
        public byte character { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long frequency { get; set; }
        public int creationId { get; set; }
        public bool isLeaf { get; set; }

        /// <summary>
        /// Inner node constructor
        /// </summary>
        /// <param name="left">Right child</param>
        /// <param name="right">Left child</param>
        /// <param name="creationId">Order of creation by the algorithm</param>
        public InnerNode(INode left, INode right, int creationId)
        {
            this.isLeaf = false;
            this.left = left;
            this.right = right;
            this.creationId = creationId;
            //this.frequency = left.frequency + right.frequency;
        }
    }
}