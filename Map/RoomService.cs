using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.Map
{
	public class RoomService : IRoomService<FloorRoom>
	{
		public FloorRoom GenerateRoom(RoomPlan roomType)
		{
			FloorRoom room = new FloorRoom();
			room.FloorTiles = new List<ITile>();

			for (int col = 0; col < roomType.Width; col++)
			{
				for (int row = 0; row < roomType.Height; row++)
				{
					var tile = new FloorTile(col, row);
					switch (new Location(col, row))
					{
						// Door
						case var p when (roomType.DoorTile?.Point == p):
							tile = roomType.DoorTile;
							break;
						// Walls
						case var p when ( p.X == 0 || p.X == (roomType.Width - 1)
																	|| p.Y == 0 || p.Y == (roomType.Height - 1)):
							tile.Cost = roomType.WallValue;
							tile.Notes = "Wall";
							break;
						// Floors
						default:
							tile.Cost = roomType.FloorValue;
							tile.Notes = "Floor";
							break;
					}
					room.FloorTiles.Add(tile);
				}
			}


			return room;
		}
	}
}
