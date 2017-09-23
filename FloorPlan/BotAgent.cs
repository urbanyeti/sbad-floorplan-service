using System;
using System.Collections.Generic;

namespace SBad.FloorPlan
{
    public class BotAgent
    {
		public string Name { get; set; }
		public Point Point { get; set; }
		public Point? OldPoint { get; set; }
		public int X { get { return Point.X; } }
		public int Y { get { return Point.Y; } }
		public string Notes { get; set; }
		public List<Point> Path { get; private set; }

		public void SetPath(List<Point> path)
		{
			Path = path;
		}

		public bool FollowPath()
		{
			int currentIndex = Path.FindIndex(p => p.X == X && p.Y == Y);
			if (currentIndex < Path.Count - 1)
			{
				Point nextPoint = Path[currentIndex + 1];
				OldPoint = Point;
				Point = nextPoint;
				return true;
			}
			else
			{
				return false;
			}
		}
	}


}
