using System.Text;
using HuffmanI.TreeBuilder;

namespace HuffmanI.TreeEncoder
{
    public class Encoder : IEncoder
    {
        public string[] EncodeNodesWithBinary(INode node, StringBuilder sb, string[] binaryArray)
        {
            if (node.GetType() == typeof(Leaf))
            {
                binaryArray[node.character] = sb.ToString();
            }

            if (node.left != null)
            {
                StringBuilder left = new StringBuilder();
                left.Append(sb);
                EncodeNodesWithBinary(node.left, left.Append('0'), binaryArray);
            }

            if (node.right != null)
            {
                StringBuilder right = new StringBuilder();
                right.Append(sb);
                EncodeNodesWithBinary(node.right, right.Append('1'), binaryArray);
            }

            return binaryArray;
        }
    }
}