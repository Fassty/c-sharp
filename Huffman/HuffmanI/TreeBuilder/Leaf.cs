using System;
using System.Runtime.Remoting.Messaging;

namespace HuffmanI.TreeBuilder
{
    public class Leaf : INode
    {
        // Left and right children will be null
        public INode left
        {
            get { return null; } set => throw new NotImplementedException(); }
        public INode right { get { return null; } set => throw new NotImplementedException(); }
        public byte character { get; set; }
        public long frequency { get; set; }
        public int creationId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool isLeaf { get; set; }

        /// <summary>
        /// Leaf constructor
        /// </summary>
        /// <param name="character">Unique byte from input file</param>
        /// <param name="frequency">Character frequency</param>
        public Leaf(byte character, long frequency)
        {
            this.isLeaf = true;
            this.character = character;
            this.frequency = frequency;
        }
    }
}