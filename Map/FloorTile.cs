using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Map
{
	public class FloorTile : ITile
	{
		public FloorTile(int x, int y, int? cost = null, string notes = "")
		{
			Point = new Point(x, y);
			Cost = cost;
			Notes = notes;
		}
		public Point Point { get; set; }
		public int X { get { return Point.X; } }
		public int Y { get { return Point.Y; } }
		public int? Cost { get; set; }
		public string Notes { get; set; } = "";
		public virtual ITile Shift(Point origin)
		{
			return new FloorTile(X + origin.X, Y + origin.Y, Cost, Notes);
		}

		public override string ToString()
		{
			return Point.ToString();	
		}
	}
}
