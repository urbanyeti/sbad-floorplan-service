﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Map
{
    public class DoorTile : FloorTile
	{
        public DoorTile(int x, int y, int? cost = null, string notes = "")
            : base(x, y, cost, notes)
        {
            
        }
	}
}