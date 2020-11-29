using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using HuffmanI.TreeBuilder;

namespace HuffmanI.InputProcessor
{
    public class InputProcessor : IInputProcessor
    {
        private Stream fileReader;

        public InputProcessor(Stream fileReader)
        {
            this.fileReader = fileReader;
        }

        /// <summary>
        /// Main input processing method
        /// </summary>
        /// <returns>Initialized set of one node trees</returns>
        public SortedSet<INode> ProcessInput()
        {
            var dict = GetUniqueBytes();
            var tree = InitializeTree(dict);

            return tree;
        }

        /// <summary>
        /// Reads the given file byte by byte and stores unique occurrences and their frequencies in an array
        /// </summary>
        /// <returns>Array of unique bytes and their frequencies</returns>
        public long[] GetUniqueBytes()
        {
            var freqArray = new long[256];

            byte[] buffer = new byte[2048];
            int bytesRead;
            while ((bytesRead = fileReader.Read(buffer,0,buffer.Length))>0)
            {
                for(int i = 0;i<bytesRead;i++)
                {
                    freqArray[buffer[i]]++;
                }
            }

            fileReader.Close();
            return freqArray;
        }

        /// <summary>
        /// Creates a node for every unique byte
        /// </summary>
        /// <param name="freqArray">Dictionary of unique bytes</param>
        /// <returns>Initialized set of leaves</returns>
        public SortedSet<INode> InitializeTree(long[] freqArray)
        {
            var tree = new SortedSet<INode>(new NodeComparer());

            for (int i = 0; i < 256; i++)
            {
                if (freqArray[i] != 0)
                {
                    INode node = new Leaf((byte) i, freqArray[i]);
                    tree.Add(node);
                }
            }

            return tree;
        }
    }
}