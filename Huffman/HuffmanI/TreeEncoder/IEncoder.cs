using System.Text;
using HuffmanI.TreeBuilder;

namespace HuffmanI.TreeEncoder
{
    public interface IEncoder
    {
        string[] EncodeNodesWithBinary(INode node, StringBuilder sb, string[] binaryArray);
    }
}