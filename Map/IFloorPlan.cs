﻿using System.Collections.Generic;

namespace SBad.Map
{
	public interface IFloorPlan
	{
		List<ITile> FloorTiles { get; set; }
		int Height { get; set; }
		string Name { get; set; }
		string Notes { get; set; }
		int Width { get; set; }

		void AddAgent(string key, IAgent agent);
		void AddRoom(FloorRoom room);
		void AddRoom(FloorRoom room, Location origin);
		void FillArea(int width, int height, int cost, bool borders);
		ITile GetFloorTile(int x, int y);
		ITile GetFloorTile(Location point);
		FloorRoom GetRoom(ITile tile);
		FloorRoom GetRoom(Location point);
		bool IsAgentOnTile(int x, int y);
		bool IsAgentOnTile(ITile floorTile);
	}
}