namespace SBad.Nav
{
    public interface IDoorTile : ITile
    {
        ITile ExitTile { get; }

        new ITile Shift(Location origin);
    }
}