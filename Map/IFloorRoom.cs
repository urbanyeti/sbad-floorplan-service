﻿using System.Collections.Generic;

namespace SBad.Map
{
    public interface IFloorRoom
    {
        List<DoorTile> DoorTiles { get; }
        List<ITile> FloorTiles { get; set; }
        string Name { get; set; }
        string Notes { get; set; }

        FloorRoom Shift(Location point);
    }
}