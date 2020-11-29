using System;
using System.IO;
using System.Text;
using HuffmanI.TreeBuilder;
using HuffmanI.TreeDecoder;

namespace HuffmanI
{
    class Program
    {
        static void ReportArgumentError() {
            Console.WriteLine("Argument Error");
        }

        static void ReportFileError() {
            Console.WriteLine("File Error");
        }

        /// <summary>
        /// Validate input and pass the input file
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    if (!args[0].Contains(".huff"))
                    {
                        ReportArgumentError();
                        return;
                    }

                    if (args[0].Length==5)
                    {
                        ReportArgumentError();
                        return;
                    }
                    Stream reader = new FileStream(args[0], FileMode.Open);
                    reader.Close();
                    Stream writer = new FileStream(args[0].Substring(0,args[0].Length-5),FileMode.OpenOrCreate);
                    writer.Close();
                }
                else
                {
                    ReportArgumentError();
                    return;
                }
            }
            catch (Exception e) 
            {
                if (e is FileLoadException || e is FileNotFoundException)
                {
                    ReportFileError();
                    return;
                }

                throw e;
            }

            string outputFile = args[0].Substring(0, args[0].Length - 5);

            DecodeHuffmanEncodedFile(args[0],outputFile);

            //ProduceHuffmanTree(args[0]);
        }

        static void DecodeHuffmanEncodedFile(string inputFile, string outputFile)
        {
            BinaryReader reader = new BinaryReader(new FileStream(inputFile, FileMode.Open));
            ITreeBuilder builder = new TreeBuilder.TreeBuilder(null,reader);
            if (!builder.BuildTreeFromFile())
            {
                ReportFileError();
                return;
            }

            BinaryWriter writer = new BinaryWriter(new FileStream(outputFile,FileMode.OpenOrCreate));
            IDecoder decoder = new TreeDecoder.Decoder(builder.tree,reader,writer);
            decoder.DecodeTree();

            writer.Close();
            reader.Close();
        }

        /// <summary>
        /// Main program method...Processes input file into a tree and prints the tree in prefix notation
        /// </summary>
        /// <param name="inputFile">Input File</param>
        static void ProduceHuffmanTree(string inputFile)
        {
            IInputProcessor processor = new InputProcessor.InputProcessor(new FileStream(inputFile, FileMode.Open));
            var leafTree = processor.ProcessInput();
            
            ITreeBuilder builder = new TreeBuilder.TreeBuilder(leafTree, new BinaryReader(Stream.Null));
            if (leafTree.Count == 0)
            {
                // TODO: When empty do something(I guess nothing)
                return;
            }
            builder.BuildTree();
            INode root = builder.tree.Min;
            
            IEncoder encoder = new TreeEncoder.Encoder();
            string[] binaryArray = encoder.EncodeNodesWithBinary(root, new StringBuilder(), new string[256]);

            IWriter writer = new Writer(new FileStream(inputFile, FileMode.Open), new BinaryWriter(File.Open(inputFile + ".huff",FileMode.OpenOrCreate)));
            writer.EncodeAndPrint(binaryArray,root);
            
        }
    }
}
