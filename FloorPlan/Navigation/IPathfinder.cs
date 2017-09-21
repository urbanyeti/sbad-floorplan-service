using SBad.FloorPlan;
using System.Collections.Generic;

namespace SBad.FloorPlan.Navigation
{
	public interface IPathfinder
	{
		List<Point> FindPath(Point start, Point end);
	}
}