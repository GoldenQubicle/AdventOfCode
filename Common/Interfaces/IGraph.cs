namespace Common.Interfaces;

public interface IGraph
{
    INode this[int x, int y]
    {
        get;
    }

    IEnumerable<INode> GetNeighbors(INode node, Func<INode, bool> query);
}
