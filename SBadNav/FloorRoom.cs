using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBad.Nav
{
    public class FloorRoom : IFloorRoom
    {
        public string Name { get; set; }
        public List<ITile> FloorTiles { get; set; } = new List<ITile>();
        public List<DoorTile> DoorTiles { get { return FloorTiles.Where(x => x is DoorTile).Select(x => (DoorTile)x).ToList(); } }
        public string Notes { get; set; }

        public FloorRoom Shift(Location point)
        {
            var shiftedRoom = new FloorRoom
            {
                Name = Name,
                Notes = Notes
            };
            foreach (var tile in FloorTiles)
            {
                shiftedRoom.FloorTiles.Add(tile.Shift(point));
            }

            return shiftedRoom;
        }
    }
}
