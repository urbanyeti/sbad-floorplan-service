using SBad.Map;
using System.Collections.Generic;

namespace SBad.Map.Navigation
{
	public interface IPathfinder
	{
		List<Location> FindPath(Location start, Location end);
	}
}