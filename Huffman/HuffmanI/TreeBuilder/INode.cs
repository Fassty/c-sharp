namespace HuffmanI.TreeBuilder
{
    public interface INode
    {
        INode left { get; set; }
        INode right { get; set; }
        byte character { get; set; }
        long frequency { get; set; }
        int creationId { get; set; }
        bool isLeaf { get; set; }
    }
}