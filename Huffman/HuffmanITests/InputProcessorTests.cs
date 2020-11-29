using System;
using System.Collections.Generic;
using System.IO;
using HuffmanI;
using HuffmanI.InputProcessor;
using HuffmanI.TreeBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuffmanITests
{
    [TestClass]
    public class InputProcessorTests
    {
        #region GetUniqueBytesTests
        [TestMethod]
        public void GetUniqueBytes_EmptyStream()
        {
            //Arrange
            var stream = new MemoryStream();
            var processor = new InputProcessor(stream);

            //Act
            var dict = processor.GetUniqueBytes();

            //Assert
            Assert.AreEqual(dict.Count,0);
        }

        [TestMethod]
        public void GetUniqueBytes_ValidInput_AllChars()
        {
            //Arrange
            string s = "aabbccddee";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            var processor = new InputProcessor(stream);

            //Act
            var dict = processor.GetUniqueBytes();

            //Assert
            Assert.AreEqual(dict.Count,5);

            foreach (var b in dict)
            {
                Assert.AreEqual(b.Value,2);
            }

            Assert.IsTrue(dict.ContainsKey((byte)'a'));
            Assert.IsTrue(dict.ContainsKey((byte)'b'));
            Assert.IsTrue(dict.ContainsKey((byte)'c'));
            Assert.IsTrue(dict.ContainsKey((byte)'d'));
            Assert.IsTrue(dict.ContainsKey((byte)'e'));
        }

        [TestMethod]
        public void GetUniqueBytes_ValidInput_Binary()
        {
            //Arrange
            byte[] buffer = new[] { Convert.ToByte(0x1f),
                                    Convert.ToByte(0x1a),
                                    Convert.ToByte(0x5f),
                                    Convert.ToByte(0x40) };
            var stream = new MemoryStream(buffer);

            //Act
            var processor = new InputProcessor(stream);
            var dict = processor.GetUniqueBytes();

            //Assert
            Assert.AreEqual(dict.Count,4);

            foreach (var b in dict)
            {
                Assert.AreEqual(b.Value,1);
            }

            Assert.IsTrue(dict.ContainsKey(Convert.ToByte(0x1f)));
            Assert.IsTrue(dict.ContainsKey(Convert.ToByte(0x1a)));
            Assert.IsTrue(dict.ContainsKey(Convert.ToByte(0x5f)));
            Assert.IsTrue(dict.ContainsKey(Convert.ToByte(0x40)));
        }
        #endregion

        #region InitializeTreeTests
        [TestMethod]
        public void InitializeTree_EmptyDictionary()
        {
            //Arrange
            var processor = new InputProcessor(null);
            var dict = new Dictionary<byte, int>();

            //Act
            var leafTree = processor.InitializeTree(dict);

            //Assert
            Assert.AreEqual(leafTree.Count,0);
        }

        [TestMethod]
        public void InitializeTree_ValidDictionary()
        {
            //Arrange
            var processor = new InputProcessorMockup();
            var dict = processor.GetUniqueBytes();

            //Act
            var leafTree = processor.InitializeTree(dict);

            //Assert
            Assert.AreEqual(leafTree.Count,10);

            int c = (byte) 'a';
            int i = 0;
            foreach (var node in leafTree)
            {
                Assert.AreEqual(node.character,Convert.ToByte(c + i));
                Assert.AreEqual(node.frequency,4);
                Assert.IsTrue(node.GetType() == typeof(Leaf));
                i++;
            }
        }
        #endregion
    }
}
