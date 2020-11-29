using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HuffmanI;
using HuffmanI.TreeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuffmanITests
{
    [TestClass]
    public class TreeBuilderTests
    {

        #region BuildTreeTests
        [TestMethod]
        public void BuildTree_OneNode()
        {
            //Arrange
            var leafTree = new SortedSet<INode>(new NodeComparer());
            var node = new Leaf(Convert.ToByte(0x1f), 4);
            leafTree.Add(node);
            var builder = new TreeBuilder(leafTree);

            //Act
            builder.BuildTree();

            //Assert
            Assert.AreEqual(builder.tree.Count,1);
            Assert.AreEqual(builder.tree.First(),node);
        }

        // Checks tree for input "aaabbc" 
        [TestMethod]
        public void BuildTree_SimpleTree()
        {
            //Arrange
            var processor = new InputProcessorMockup();
            var leafTree = processor.BuildSimpleTree();
            var builder = new TreeBuilder(leafTree);

            //Act
            builder.BuildTree();

            //Assert
            Assert.AreEqual(builder.tree.Count, 1);
            var root = builder.tree.First();

            Assert.IsTrue(root.GetType() != typeof(Leaf));
            Assert.AreEqual(root.frequency,6);
            Assert.AreEqual(root.creationId,2);

            Assert.IsTrue(root.left.GetType() == typeof(Leaf));
            Assert.AreEqual(root.left.character,Convert.ToByte('a'));
            Assert.AreEqual(root.left.frequency,3);

            Assert.IsTrue(root.right.GetType() != typeof(Leaf));
            Assert.AreEqual(root.right.frequency,3);
            Assert.AreEqual(root.right.creationId,1);

            Assert.IsTrue(root.right.left.GetType() == typeof(Leaf));
            Assert.AreEqual(root.right.left.character,Convert.ToByte('c'));
            Assert.AreEqual(root.right.left.frequency,1);

            Assert.IsTrue(root.right.right.GetType() == typeof(Leaf));
            Assert.AreEqual(root.right.right.character,Convert.ToByte('b'));
            Assert.AreEqual(root.right.right.frequency,2);
        }
        #endregion

        #region PrintTreeTests
        [TestMethod]
        public void PrintTree_PrintSingleNode()
        {
            //Arrange
            var node = new Leaf(Convert.ToByte('a'),10);
            var builder = new TreeBuilder(new SortedSet<INode>());

            using (StringWriter stringWriter = new StringWriter())
            {
                //Act
                Console.SetOut(stringWriter);
                var writer = new Writer(Console.OpenStandardOutput());
                writer.PrintTree(node);

                //Assert
                Assert.AreEqual("*97:10",stringWriter.ToString());
            }

        }

        [TestMethod]
        public void PrintTree_PrintSimpleTree()
        {
            //Arrange
            var processor = new InputProcessorMockup();
            var leafTree = processor.BuildSimpleTree();
            var builder = new TreeBuilder(leafTree);

            using (StringWriter stringWriter = new StringWriter())
            {
                //Act
                builder.BuildTree();
                Console.SetOut(stringWriter);
                builder.PrintTree(builder.tree.First());

                //Assert
                Assert.AreEqual("6 *97:3 3 *99:1 *98:2", stringWriter.ToString());
            }
        }
        #endregion
    }
}
