namespace SBad.FloorPlan
{
	public static class Common
    {
    }

	public enum Direction
	{
		Random = 0,
		North = 1,
		East = 2,
		South = 3,
		West = 4
	}

	public struct Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; }
		public int Y { get; }
	}
}
