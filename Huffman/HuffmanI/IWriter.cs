using HuffmanI.TreeBuilder;

namespace HuffmanI
{
    public interface IWriter
    {
        void PrintTree(INode node);
        void EncodeAndPrint(string[] binaryArray, INode root);

    }
}