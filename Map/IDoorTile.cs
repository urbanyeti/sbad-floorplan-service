namespace SBad.Map
{
    public interface IDoorTile : ITile
    {
        ITile ExitTile { get; }
        ITile Shift(Location origin);
    }
}