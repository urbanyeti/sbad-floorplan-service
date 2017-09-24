using System;

namespace SBad.Map
{
	public static class Common
    {
		public static bool IsNearby(this Point point, Point point2)
		{
			return (point2 == point.GetNorth()
				|| point2 == point.GetEast()
				|| point2 == point.GetSouth()
				|| point2 == point.GetWest());
		}

		public static Point GetNorth(this Point point)
		{
			return new Point(point.X, point.Y - 1);
		}

		public static Point GetEast(this Point point)
		{
			return new Point(point.X + 1, point.Y);
		}

		public static Point GetSouth(this Point point)
		{
			return new Point(point.X, point.Y + 1);
		}

		public static Point GetWest(this Point point)
		{
			return new Point(point.X - 1, point.Y);
		}
	}

	public enum Direction
	{
		Random = 0,
		North = 1,
		East = 2,
		South = 3,
		West = 4
	}

	public struct Point : IEquatable<Point>
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; }
		public int Y { get; }

		public bool Equals(Point other)
		{
			return (X == other.X && Y == other.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is Point)
			{
				return Equals((Point)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 23 + X.GetHashCode();
				hash = hash * 23 + Y.GetHashCode();
				return hash;
			}
		}

		public static bool operator ==(Point p1, Point p2)
		{
			return p1.Equals(p2);
		}

		public static bool operator !=(Point p1, Point p2)
		{
			return !p1.Equals(p2);
		}
	}
}
