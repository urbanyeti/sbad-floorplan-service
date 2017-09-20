using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.FloorPlan
{
	public class FloorTile
	{
		public FloorTile(int x, int y, int? cost = null, string notes = "")
		{
			X = x;
			Y = y;
			Cost = cost;
			Notes = notes;
		}
		public int X { get; set; }
		public int Y { get; set; }
		public int? Cost { get; set; }
		public string Notes { get; set; } = "";
		public FloorTile Shift(Point origin)
		{
			return new FloorTile(X + origin.X, Y + origin.Y, Cost, Notes);
		}
	}
}
