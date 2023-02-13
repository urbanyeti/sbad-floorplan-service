using System.Collections.Generic;

namespace SBad.Nav
{
	public interface IAgent
	{
		string Name { get; set; }
		string Notes { get; set; }
		Location? OldPoint { get; set; }
		List<Location> Path { get; }
		Location Point { get; }
		int X { get; }
		int Y { get; }

		bool AttackTile(ITile tile);
		bool FollowPath(FloorPlan plan);
		IAgent SetPath(List<Location> path);
		IAgent Move(Location point);
	}
}