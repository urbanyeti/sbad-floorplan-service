using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Map
{
	public class DoorTile : FloorTile
	{
		public DoorTile(int x, int y, ITile exitTile = null, int? cost = null, string notes = "")
			: base(x, y, cost, notes)
		{
			ExitTile = exitTile;
		}

		public ITile ExitTile { get; set; }

		public override ITile Shift(Point origin)
		{
			return new DoorTile(X + origin.X, Y + origin.Y, ExitTile.Shift(origin), Cost, Notes);
		}
	}
}
