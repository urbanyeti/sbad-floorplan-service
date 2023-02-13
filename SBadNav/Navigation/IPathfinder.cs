using SBad.Nav;
using System.Collections.Generic;

namespace SBad.Nav.Navigation
{
	public interface IPathfinder
	{
		List<Location> FindPath(Location start, Location end);
	}
}