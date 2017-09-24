using SBad.Map;
using System.Collections.Generic;

namespace SBad.Map.Navigation
{
	public interface IPathfinder
	{
		List<Point> FindPath(Point start, Point end);
	}
}