using System;

namespace SBad.Map
{
	public static class Common
    {
		public static bool IsNearby(this Location point, Location point2)
		{
			return (point2 == point.GetNorth()
				|| point2 == point.GetEast()
				|| point2 == point.GetSouth()
				|| point2 == point.GetWest());
		}

		public static Location GetNorth(this Location point)
		{
			return new Location(point.X, point.Y - 1);
		}

		public static Location GetEast(this Location point)
		{
			return new Location(point.X + 1, point.Y);
		}

		public static Location GetSouth(this Location point)
		{
			return new Location(point.X, point.Y + 1);
		}

		public static Location GetWest(this Location point)
		{
			return new Location(point.X - 1, point.Y);
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

	public struct Location : IEquatable<Location>
	{
		public Location(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; }
		public int Y { get; }

		public bool Equals(Location other)
		{
			return (X == other.X && Y == other.Y);
		}

		public override bool Equals(object obj)
		{
			if (obj is Location)
			{
				return Equals((Location)obj);
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

		public static bool operator ==(Location p1, Location p2)
		{
			return p1.Equals(p2);
		}

		public static bool operator !=(Location p1, Location p2)
		{
			return !p1.Equals(p2);
		}

		public override string ToString()
		{
			return $"({X},{Y})";
		}
	}
}
