namespace Assets.Template.Hex
{
    public interface IPathable
    {
        IHex GetCurrentTile();
        int GetTileTraversalCost(IHex source, IHex goal);
    }
}
