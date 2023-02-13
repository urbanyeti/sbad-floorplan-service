using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SBad.Nav.Navigation;
using FluentAssertions;

namespace SBad.Nav.Test
{
	[TestClass]
	public class Navigation_service_tests
	{
		[TestMethod]
		public void NavigationService_works()
		{
			var plan = new FloorPlan();
			plan.FloorTiles.AddRange(_FillArea(20, 10, 1));
			plan.GetFloorTile(1, 0).Cost = 3;
			plan.GetFloorTile(0, 1).Cost = 2;
			plan.GetFloorTile(1, 1).Cost = 2;
			plan.GetFloorTile(1, 8).Cost = 3;
			plan.GetFloorTile(0, 8).Cost = 3;

			NavigationService service = NSubstitute.Substitute.For<NavigationService>(plan);
			var path = service.FindPath(new Location(0, 0), new Location(14, 8));

			Debug.Write(plan.Print(path));
		}

		[TestMethod]
		public void NavigationService_weighs_values()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			FloorPlan plan = new FloorPlan(20, 20, fill:true);

			RoomPlan roomPlan = new RoomPlan
			{
				Name = "Test",
				Width = 5,
				Height = 5,
				WallValue = 20,
				FloorValue = 1,
				DoorTile = new DoorTile(4, 2, cost:3, notes:"Door")
			};
            IFloorRoom room = roomService.GenerateRoom(roomPlan);
			plan.AddRoom(room, new Location(3, 12));

			var display = plan.Print();
			Debug.WriteLine(display);

			NavigationService navService = NSubstitute.Substitute.For<NavigationService>(plan);
			var path = navService.FindPath(new Location(1, 1), new Location(5, 14));
			display = plan.Print(path);
			Debug.Write(display);
		}

		private IEnumerable<FloorTile> _FillArea(int width, int height, int? cost = null)
		{
			var tiles = new List<FloorTile>();
			for (int row = 0; row < height; row++)
			{
				for (int col = 0; col < width; col++)
				{
					tiles.Add(new FloorTile(col, row, cost: cost));
				}
			}
			return tiles;
		}
	}
}
