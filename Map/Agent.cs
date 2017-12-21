using System;
using System.Collections.Generic;

namespace SBad.Map
{
	public class Agent : IAgent
	{
		public Agent() { }
		public Agent(Location point)
		{
			Point = point;
		}

		public string Name { get; set; }
		public Location Point { get; private set; }
		public Location? OldPoint { get; set; }
		public int X { get { return Point.X; } }
		public int Y { get { return Point.Y; } }
		public string Notes { get; set; }
		public virtual List<Location> Path { get; private set; }

		public virtual IAgent SetPath(List<Location> path)
		{
			Path = path;
			return this;
		}

		public virtual IAgent Move(Location point)
		{
			Point = point;
			return this;
		}

		public virtual bool FollowPath(FloorPlan plan)
		{
			int currentIndex = Path.FindIndex(p => p.X == X && p.Y == Y);
			if (currentIndex < Path.Count - 1)
			{
				Location nextPoint = Path[currentIndex + 1];
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

		public virtual bool AttackTile(ITile tile)
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
