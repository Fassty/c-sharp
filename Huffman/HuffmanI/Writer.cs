using System;
using System.IO;
using System.Text;
using HuffmanI.TreeBuilder;

namespace HuffmanI
{
    public class Writer : IWriter
    {
        private readonly Stream inputStream;
        private readonly BinaryWriter writer;
        private const ulong mask = 0x00ffffffffffffff;
        private ulong[] valueArray = new ulong[256];

        /// <summary>
        /// Table for fast byte flipping 
        /// </summary>
        private byte[] magicRevertingTable =
        {
            0x00, 0x80, 0x40, 0xC0, 0x20, 0xA0, 0x60, 0xE0, 0x10, 0x90, 0x50, 0xD0, 0x30, 0xB0, 0x70, 0xF0,
            0x08, 0x88, 0x48, 0xC8, 0x28, 0xA8, 0x68, 0xE8, 0x18, 0x98, 0x58, 0xD8, 0x38, 0xB8, 0x78, 0xF8,
            0x04, 0x84, 0x44, 0xC4, 0x24, 0xA4, 0x64, 0xE4, 0x14, 0x94, 0x54, 0xD4, 0x34, 0xB4, 0x74, 0xF4,
            0x0C, 0x8C, 0x4C, 0xCC, 0x2C, 0xAC, 0x6C, 0xEC, 0x1C, 0x9C, 0x5C, 0xDC, 0x3C, 0xBC, 0x7C, 0xFC,
            0x02, 0x82, 0x42, 0xC2, 0x22, 0xA2, 0x62, 0xE2, 0x12, 0x92, 0x52, 0xD2, 0x32, 0xB2, 0x72, 0xF2,
            0x0A, 0x8A, 0x4A, 0xCA, 0x2A, 0xAA, 0x6A, 0xEA, 0x1A, 0x9A, 0x5A, 0xDA, 0x3A, 0xBA, 0x7A, 0xFA,
            0x06, 0x86, 0x46, 0xC6, 0x26, 0xA6, 0x66, 0xE6, 0x16, 0x96, 0x56, 0xD6, 0x36, 0xB6, 0x76, 0xF6,
            0x0E, 0x8E, 0x4E, 0xCE, 0x2E, 0xAE, 0x6E, 0xEE, 0x1E, 0x9E, 0x5E, 0xDE, 0x3E, 0xBE, 0x7E, 0xFE,
            0x01, 0x81, 0x41, 0xC1, 0x21, 0xA1, 0x61, 0xE1, 0x11, 0x91, 0x51, 0xD1, 0x31, 0xB1, 0x71, 0xF1,
            0x09, 0x89, 0x49, 0xC9, 0x29, 0xA9, 0x69, 0xE9, 0x19, 0x99, 0x59, 0xD9, 0x39, 0xB9, 0x79, 0xF9,
            0x05, 0x85, 0x45, 0xC5, 0x25, 0xA5, 0x65, 0xE5, 0x15, 0x95, 0x55, 0xD5, 0x35, 0xB5, 0x75, 0xF5,
            0x0D, 0x8D, 0x4D, 0xCD, 0x2D, 0xAD, 0x6D, 0xED, 0x1D, 0x9D, 0x5D, 0xDD, 0x3D, 0xBD, 0x7D, 0xFD,
            0x03, 0x83, 0x43, 0xC3, 0x23, 0xA3, 0x63, 0xE3, 0x13, 0x93, 0x53, 0xD3, 0x33, 0xB3, 0x73, 0xF3,
            0x0B, 0x8B, 0x4B, 0xCB, 0x2B, 0xAB, 0x6B, 0xEB, 0x1B, 0x9B, 0x5B, 0xDB, 0x3B, 0xBB, 0x7B, 0xFB,
            0x07, 0x87, 0x47, 0xC7, 0x27, 0xA7, 0x67, 0xE7, 0x17, 0x97, 0x57, 0xD7, 0x37, 0xB7, 0x77, 0xF7,
            0x0F, 0x8F, 0x4F, 0xCF, 0x2F, 0xAF, 0x6F, 0xEF, 0x1F, 0x9F, 0x5F, 0xDF, 0x3F, 0xBF, 0x7F, 0xFF
        };

        public Writer(Stream stream)
        {
            this.inputStream = stream;
        }

        public Writer(Stream inputStream, BinaryWriter writer)
        {
            this.inputStream = inputStream;
            this.writer = writer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryArray"></param>
        /// <param name="root"></param>
        public void EncodeAndPrint(string[] binaryArray, INode root)
        {
            AssignValues(binaryArray);
            WriteHeader();
            PrintCodedTree(root);
            Write64bitZero();
            FileToHuffman(binaryArray);

            writer.Close();
        }

        /// <summary>
        /// Assigns a decadic value to every character for faster encoding
        /// </summary>
        /// <param name="binaryArray"></param>
        private void AssignValues(string[] binaryArray)
        {
            for (int i = 0; i < 256; i++)
            {
                if (binaryArray[i] != null)
                {
                    valueArray[i] = Convert.ToUInt64(binaryArray[i], 2);
                }
            }
        }

        /// <summary>
        /// Magical header to identify files encoded with Huffman's coding
        /// </summary>
        private void WriteHeader()
        {
            writer.Write((byte)0x7B);
            writer.Write((byte)0x68);
            writer.Write((byte)0x75);
            writer.Write((byte)0x7C);
            writer.Write((byte)0x6D);
            writer.Write((byte)0x7D);
            writer.Write((byte)0x66);
            writer.Write((byte)0x66);
        }

        private void Write64bitZero()
        {
            ulong zero = 0;
            writer.Write(zero);
        }

        /// <summary>
        /// Recursively prints the Huffman's tree in binary representation
        /// </summary>
        /// <param name="node"></param>
        private void PrintCodedTree(INode node)
        {
            ulong code = 0;
            if (!node.isLeaf)
            {
                code = (ulong) node.frequency << 1;
                code = code & mask;
                writer.Write(code);
            }
            else
            {
                code = 1;
                code = code | ((ulong) node.frequency << 1);
                code = code & mask;
                code = code | ((ulong) node.character << 56);
                writer.Write(code);
                return;
            }

            PrintCodedTree(node.left);
            PrintCodedTree(node.right);
        }

        /// <summary>
        /// Encodes the file with Huffman coding using generated tree
        /// </summary>
        /// <param name="binaryArray">Huffman's tree binary value for every character</param>
        private void FileToHuffman(string[] binaryArray)
        {
            ulong result = 0;
            int currentLength = 0;

            byte[] buffer = new byte[2048];
            int bytesRead;

            while ((bytesRead = inputStream.Read(buffer,0,buffer.Length)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    result <<= binaryArray[buffer[i]].Length;
                    result |= valueArray[buffer[i]];
                    currentLength += binaryArray[buffer[i]].Length;

                    while (currentLength >= 8)
                    {
                        ulong newByte = result;
                        newByte >>= (currentLength - 8);
                        writer.Write(magicRevertingTable[(byte)newByte]);
                        currentLength -= 8;
                    }
                }
            }

            inputStream.Close();
            
            // Flush remaining bytes into a file
            if (currentLength > 0)
            {
                StringBuilder flush = new StringBuilder();
                for (int i = 0; i < currentLength; i++)
                {
                    flush.Append('1');
                }

                result &= Convert.ToByte(flush.ToString(), 2);
                result <<= (8 - currentLength); 
                writer.Write(magicRevertingTable[(byte) result]);
            }
        }

        /// <summary>
        /// Prints a tree recursively in prefix notation
        /// Initialized with the root node
        /// </summary>
        /// <param name="node"></param>
        public void PrintTree(INode node)
        {
            TextWriter writer = new StreamWriter(inputStream);
            if (!node.isLeaf)
            {
                writer.Write("{0} ", node.frequency);
                PrintTree(node.left);
                writer.Write(" ");
                PrintTree(node.right);
            }
            else
            {
                writer.Write("*{0}:{1}", node.character, node.frequency);
            }
        }

    }
}