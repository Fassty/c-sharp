using System.Collections.Generic;
using System.IO;
using HuffmanI.TreeBuilder;

namespace HuffmanI.TreeDecoder
{
    public class Decoder:IDecoder
    {
        private SortedSet<INode> tree;
        private BinaryReader fileReader;
        private BinaryWriter writer;

        public Decoder(SortedSet<INode> tree, BinaryReader fileReader, BinaryWriter writer)
        {
            this.tree = tree;
            this.fileReader = fileReader;
            this.writer = writer;
        }

        public void DecodeTree()
        {
            INode root = tree.Min;
            INode currentNode = root;
            INode tmpNode = root;

            byte[] buffer = new byte[2048];
            int bytesRead = 0;
            ulong temp = 0;
            byte chr;

            while ((bytesRead = fileReader.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        temp = buffer[i] & (ulong)1;
                        buffer[i] >>= 1;
                        if (temp == 0)
                        {
                            tmpNode = currentNode;
                            currentNode = tmpNode?.left;
                            if (currentNode is Leaf)
                            {
                                if (currentNode.frequency > 0)
                                {
                                    writer.Write(currentNode.character);
                                    currentNode.frequency--;
                                }

                                currentNode = root;
                            }
                        }else if (temp == 1)
                        {
                            tmpNode = currentNode;
                            currentNode = tmpNode?.right;
                            if (currentNode is Leaf)
                            {
                                if (currentNode.frequency > 0)
                                {
                                    writer.Write(currentNode.character);
                                    currentNode.frequency--;
                                }

                                currentNode = root;
                            }
                        }
                    }
                }
            }
        }
    }
}