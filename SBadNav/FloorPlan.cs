﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBad.Nav
{
	public class FloorPlan : IFloorPlan
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
		protected List<FloorRoom> FloorRooms { get; } = new List<FloorRoom>();
		public List<ITile> FloorTiles { get; set; } = new List<ITile>();
		protected virtual Dictionary<string, IAgent> Agents { get; } = new Dictionary<string, IAgent>();
		public string Notes { get; set; }

		public void AddRoom(IFloorRoom room)
		{
			AddRoom(room, new Location(0, 0));
		}
		public void AddRoom(IFloorRoom room, Location origin)
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

		public void AddAgent(string key, IAgent agent)
		{
			Agents[key] = (agent);
		}

		public bool IsAgentOnTile(ITile floorTile)
		{
			if (floorTile == null)
			{
				return false;
			}

			return Agents.Values.Any(b => b.X == floorTile.X && b.Y == floorTile.Y);
		}

		public bool IsAgentOnTile(int x, int y)
		{
			var tile = GetFloorTile(x, y);
			return IsAgentOnTile(tile);
		}

		public ITile GetFloorTile(Location point)
		{
			return GetFloorTile(point.X, point.Y);
		}

		public ITile GetFloorTile(int x, int y)
		{
			return FloorTiles.SingleOrDefault(t => x >= 0 && y >= 0 && t.X == x && t.Y == y);
		}

        public IFloorRoom GetRoom(Location point)
        {
            return GetRoom(GetFloorTile((point.X), point.Y));
        }

        public IFloorRoom GetRoom(ITile tile)
        {
           return FloorRooms.FirstOrDefault(x => x.FloorTiles.Contains(tile));
        }

		public void FillArea(int width, int height, int cost, bool borders)
		{
			FloorTiles = new List<ITile>();
			for (int col = 0; col < width; col++)
			{
				for (int row = 0; row < height; row++)
				{
					var tile = new FloorTile(col, row);
					switch (new Location(col, row))
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
