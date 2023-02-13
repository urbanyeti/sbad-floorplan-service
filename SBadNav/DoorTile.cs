using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Nav
{
	public class DoorTile : FloorTile, IDoorTile
	{
		public DoorTile(int x, int y, ITile exitTile = null, int? cost = null, string notes = "")
			: base(x, y, cost, notes)
		{
			ExitTile = exitTile;
		}

		public ITile ExitTile { get; set; }

		public override ITile Shift(Location origin)
		{
			return new DoorTile(X + origin.X, Y + origin.Y, ExitTile?.Shift(origin), Cost, Notes);
		}
	}
}
