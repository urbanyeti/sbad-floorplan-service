using System;
using System.Collections.Generic;
using System.Text;

namespace SBad.FloorPlan
{
	public class RoomService
	{
		public FloorRoom GenerateRoom(RoomPlan roomType)
		{
			FloorRoom room = new FloorRoom();
			room.FloorTiles = new List<FloorTile>();

			Direction doorDir = roomType.DoorWall;
			if (roomType.DoorWall == Direction.Random)
			{
				doorDir = (Direction)new Random().Next(1, 5);
			}

			for (int col = 0; col < roomType.Width; col++)
			{
				for (int row = 0; row < roomType.Height; row++)
				{
					var tile = new FloorTile(col, row);
					switch (new Point(col, row))
					{
						// Door
						case var p when ((doorDir == Direction.North && p.Y == 0 && p.X == ((roomType.Width - 1) / 2))
							|| (doorDir == Direction.South && p.Y == (roomType.Height - 1)) && p.X == ((roomType.Width - 1) / 2))
							|| (doorDir == Direction.West && p.X == 0 && p.Y == ((roomType.Height - 1) / 2))
							|| (doorDir == Direction.East && p.X == (roomType.Width - 1) && p.Y == ((roomType.Height - 1) / 2)):
							tile.Cost = roomType.DoorValue;
							tile.Notes = "Door";
							break;
						// Walls
						case var p when (p.X == 0 || p.X == (roomType.Width - 1) || p.Y == 0 || p.Y == (roomType.Height - 1)):
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
