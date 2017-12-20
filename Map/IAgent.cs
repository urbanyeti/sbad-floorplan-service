using System.Collections.Generic;

namespace SBad.Map
{
	public interface IAgent
	{
		string Name { get; set; }
		string Notes { get; set; }
		Point? OldPoint { get; set; }
		List<Point> Path { get; }
		Point Point { get; }
		int X { get; }
		int Y { get; }

		bool AttackTile(ITile tile);
		bool FollowPath(FloorPlan plan);
		IAgent SetPath(List<Point> path);
		IAgent Move(Point point);
	}
}