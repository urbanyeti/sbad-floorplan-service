using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SBad.FloorPlan.Navigation;
using FluentAssertions;

namespace SBad.FloorPlan.Test
{
	[TestClass]
	public class Navigation_service_tests
	{
		[TestMethod]
		public void NavigationService_works()
		{
			FloorPlan plan = NSubstitute.Substitute.For<FloorPlan>();
			plan.FloorTiles.AddRange(_FillArea(20, 10, 1));
			plan.GetFloorTile(1, 0).Cost = 3;
			plan.GetFloorTile(0, 1).Cost = 2;
			plan.GetFloorTile(1, 1).Cost = 2;
			plan.GetFloorTile(1, 8).Cost = 3;
			plan.GetFloorTile(0, 8).Cost = 3;

			NavigationService service = NSubstitute.Substitute.For<NavigationService>(plan);
			var path = service.FindPath(new Point(0, 0), new Point(14, 8));

			Debug.Write(plan.Print(path));
		}

		[TestMethod]
		public void NavigationService_weighs_values()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			FloorPlan plan = new FloorPlan(20, 20);

			RoomPlan roomPlan = new RoomPlan
			{
				Name = "Test",
				Width = 5,
				Height = 5,
				WallValue = 20,
				FloorValue = 1,
				DoorValue = 3,
				DoorWall = Direction.East
			};
			FloorRoom room = roomService.GenerateRoom(roomPlan);
			plan.AddRoom(room, new Point(3, 12));

			var display = plan.Print();
			Debug.WriteLine(display);

			NavigationService navService = NSubstitute.Substitute.For<NavigationService>(plan);
			var path = navService.FindPath(new Point(1, 1), new Point(5, 14));
			display = plan.Print(path);
			Debug.Write(display);
		}

		[TestMethod]
		public void NavigationService_agent_follows_path()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			FloorPlan plan = new FloorPlan(5, 4);

			BotAgent agent = NSubstitute.Substitute.For<BotAgent>();
			agent.Point = new Point(1, 1);
			plan.BotAgents.Add(agent);

			NavigationService navService = NSubstitute.Substitute.For<NavigationService>(plan);
			agent.SetPath(navService.FindPath(agent.Point, new Point(3, 2)));

			agent.Path.Count.ShouldBeEquivalentTo(4);

			var display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+@11+" + Environment.NewLine + "+===+"
				+ Environment.NewLine+ "+++++" + Environment.NewLine);

			agent.FollowPath().Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+=11+" + Environment.NewLine + "+@==+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath().Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+=11+" + Environment.NewLine + "+=@=+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath().Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+=11+" + Environment.NewLine + "+==@+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath().Should().BeFalse();
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
