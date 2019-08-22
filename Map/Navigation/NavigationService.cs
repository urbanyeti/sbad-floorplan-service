using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBad.Map.Navigation
{
	public class NavigationService
	{
		public NavigationService(FloorPlan floorPlan)
		{
			Refresh(floorPlan);
			Pathfinder = new AStarPathfinder(MaxRow, MaxCol, Weight);
		}

		public int MaxRow { get; set; }
		public int MaxCol { get; set; }
		public decimal[,] Weight;
		public IPathfinder Pathfinder { get; set; }
		public FloorPlan FloorPlan { get; set; }

		public void Refresh(FloorPlan floorPlan)
		{
			FloorPlan = floorPlan;
			MaxRow = floorPlan.FloorTiles.Max(x => x.Y);
			MaxCol = floorPlan.FloorTiles.Max(x => x.X);

			Weight = new decimal[MaxCol + 1, MaxRow + 1];

			for (int row = 0; row <= MaxRow; row++)
			{
				for (int col = 0; col <= MaxCol; col++)
				{
					var tile = floorPlan.GetFloorTile(col, row);
					Weight[col, row] = tile?.Cost ?? 0;
				}
			}
		}

		public List<Location> Navigate(Location start, Location end, List<Location> path)
		{
			List<Location> pathSoFar = path;
			IFloorRoom room = FloorPlan.GetRoom(start);

			if (room == FloorPlan.GetRoom((end)))
			{
				pathSoFar.AddRange(FindPath(start, end));
				return pathSoFar;
			}
			else
			{
				var doors = room.DoorTiles;
				foreach (var door in room.DoorTiles)
				{
					pathSoFar.AddRange(Pathfinder.FindPath(start, door.Point));
					var exitTile = door.ExitTile;

					Navigate(exitTile.Point, end, pathSoFar);
				}
			}


			return pathSoFar;
		}

		public List<Location> FindPath(Location start, Location end)
		{
			return Pathfinder.FindPath(start, end);
		}
	}
}
