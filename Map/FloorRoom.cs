using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Map
{
    public class FloorRoom
    {
		public string Name { get; set; }
		public List<ITile> FloorTiles { get; set; } = new List<ITile>();
		public string Notes { get; set; }

		public FloorRoom Shift(Point point)
		{
			var shiftedRoom = new FloorRoom
			{
				Name = Name,
				Notes = Notes
			};
			foreach(var tile in FloorTiles)
			{
				shiftedRoom.FloorTiles.Add(tile.Shift(point));
			}

			return shiftedRoom;
		}
	}
}
