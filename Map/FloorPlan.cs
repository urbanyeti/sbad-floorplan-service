using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBad.Map
{
	public class FloorPlan
	{
		public FloorPlan() { }
		public FloorPlan(int width, int height, bool fill = false)
		{
			Width = width;
			Height = height;

			if (fill)
			{
				FillArea(width, height, cost: 1, borders: true);
			}

		}
		public string Name { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		protected List<FloorRoom> FloorRooms { get; private set; } = new List<FloorRoom>();
		public List<ITile> FloorTiles { get; set; } = new List<ITile>();
		public List<BotAgent> BotAgents { get; set; } = new List<BotAgent>();
		public string Notes { get; set; }

		public void AddRoom(FloorRoom room)
		{
			AddRoom(room, new Point(0, 0));
		}
		public void AddRoom(FloorRoom room, Point origin)
		{
			var placedRoom = room.Shift(origin);
			FloorRooms.Add(placedRoom);
			foreach (var tile in placedRoom.FloorTiles)
			{
				if (tile.X < Width && tile.Y < Height)
				{
					var oldTile = GetFloorTile(tile.X, tile.Y);
					if (oldTile != null)
					{
						FloorTiles.Remove(oldTile);
					}
					FloorTiles.Add(tile);
				}
			}
		}

		public ITile GetFloorTile(Point point)
		{
			return GetFloorTile(point.X, point.Y);
		}

		public ITile GetFloorTile(int x, int y)
		{
			return FloorTiles.SingleOrDefault(t => x >= 0 && y >= 0 && t.X == x && t.Y == y);
		}

		public void FillArea(int width, int height, int cost, bool borders)
		{
			FloorTiles = new List<ITile>();
			for (int col = 0; col < width; col++)
			{
				for (int row = 0; row < height; row++)
				{
					var tile = new FloorTile(col, row);
					switch (new Point(col, row))
					{
						// Borders
						case var p when borders && (p.X == 0 || p.X == (width - 1) || p.Y == 0 || p.Y == (height - 1)):
							tile.Cost = -1;
							tile.Notes = "Border";
							break;
						default:
							tile.Cost = cost;
							tile.Notes = "Floor";
							break;
					}
					FloorTiles.Add(tile);
				}
			}
		}
	}
}
