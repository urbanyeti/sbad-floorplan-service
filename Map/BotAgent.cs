﻿using System;
using System.Collections.Generic;

namespace SBad.Map
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

		public bool FollowPath(FloorPlan plan)
		{
			int currentIndex = Path.FindIndex(p => p.X == X && p.Y == Y);
			if (currentIndex < Path.Count - 1)
			{
				Point nextPoint = Path[currentIndex + 1];
				var tile = plan.GetFloorTile(nextPoint);
				if (tile.Cost > 1)
				{
					AttackTile(tile);
					return true;
				}
				else
				{
					OldPoint = Point;
					Point = nextPoint;
					return true;
				}
			}
			else
			{
				return false;
			}
		}

		public bool AttackTile(FloorTile tile)
		{
			if (tile != null && Point.IsNearby(tile.Point))
			{
				if (tile.Cost > 1)
				{
					tile.Cost = tile.Cost - 1;
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}

		}
	}


}