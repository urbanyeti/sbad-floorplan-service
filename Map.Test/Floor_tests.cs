using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using FluentAssertions;
using System.Collections.Generic;

namespace SBad.Map.Test
{
	[TestClass]
	public class Floor_tests
	{
		[TestMethod]
		public void FloorPlan_displays_costs()
		{
			var plan = new FloorPlan();
			plan.FloorTiles.Add(new FloorTile(0, 0));
			plan.FloorTiles.Add(new FloorTile(1, 0, cost: -1));
			plan.FloorTiles.Add(new FloorTile(2, 0, cost: 1));
			plan.FloorTiles.Add(new FloorTile(3, 0, cost: 2));
			plan.FloorTiles.Add(new FloorTile(4, 0, cost: 3));
			plan.FloorTiles.Add(new FloorTile(0, 1, cost: 4));
			plan.FloorTiles.Add(new FloorTile(2, 1, cost: 6));
			plan.FloorTiles.Add(new FloorTile(3, 1, cost: 99));
			plan.FloorTiles.Add(new FloorTile(1, 1, cost: 5));

			string display = plan.Print();

			Debug.Write(display);
			display.ShouldBeEquivalentTo("++_23" + Environment.NewLine + "456X+" + Environment.NewLine);
		}

		[TestMethod]
		public void FloorPlan_displays_paths()
		{
			var plan = new FloorPlan();
			plan.FloorTiles.AddRange(_FillArea(2, 2));

			var path = new List<Point>();
			path.Add(new Point(0, 0));
			path.Add(new Point(1, 1));
			string display = plan.Print(path);

			Debug.Write(display);
			display.ShouldBeEquivalentTo("A+" + Environment.NewLine + "+B" + Environment.NewLine);
		}

		[TestMethod]
		public void FloorPlan_displays_agents()
		{
			var plan = new FloorPlan();//NSubstitute.Substitute.For<FloorPlan>();
			plan.FloorTiles.AddRange(_FillArea(3, 1));

			plan.AddAgent("Test Agent", new Agent { Point = new Point(1, 0) });
			string display = plan.Print();

			Debug.Write(display);
			display.ShouldBeEquivalentTo("+@+" + Environment.NewLine);
		}
		[TestMethod]
		public void FloorPlan_detects_tiles()
		{
			FloorPlan plan = NSubstitute.Substitute.For<FloorPlan>();
			plan.GetFloorTile(10, 1).Should().BeNull();

			plan.FloorTiles.AddRange(_FillArea(30, 10, 1));
			plan.GetFloorTile(20, 1).Cost.Value.ShouldBeEquivalentTo(1);

			plan.GetFloorTile(10, 2).Cost = 5;
			plan.GetFloorTile(10, 2).Cost.Value.ShouldBeEquivalentTo(5);

			plan.GetFloorTile(30, 0).Should().BeNull();
			plan.GetFloorTile(0, 10).Should().BeNull();
		}

		[TestMethod]
		public void FloorPlan_detects_adjacent_tiles()
		{
			FloorPlan plan = NSubstitute.Substitute.For<FloorPlan>();
			plan.FloorTiles.AddRange(_FillArea(5, 5));
			var center = plan.GetFloorTile(2, 2);

			plan.GetNorth(center).Should().BeSameAs(plan.GetFloorTile(2, 1));
			plan.GetSouth(center).Should().BeSameAs(plan.GetFloorTile(2, 3));
			plan.GetWest(center).Should().BeSameAs(plan.GetFloorTile(1, 2));
			plan.GetEast(center).Should().BeSameAs(plan.GetFloorTile(3, 2));

			plan.GetNorth(center).Should().NotBeSameAs(plan.GetFloorTile(2, 3));

			var corner = plan.GetFloorTile(0, 0);
			plan.GetNorth(corner).Should().BeNull();
			plan.GetSouth(corner).Should().BeSameAs(plan.GetFloorTile(0, 1));
			plan.GetWest(corner).Should().BeNull();
			plan.GetEast(corner).Should().BeSameAs(plan.GetFloorTile(1, 0));

			corner = plan.GetFloorTile(4, 4);
			plan.GetNorth(corner).Should().BeSameAs(plan.GetFloorTile(4, 3));
			plan.GetSouth(corner).Should().BeNull();
			plan.GetWest(corner).Should().BeSameAs(plan.GetFloorTile(3, 4));
			plan.GetEast(corner).Should().BeNull();
		}
		[TestMethod]
		public void FloorPlan_detects_agents()
		{
			FloorPlan plan = new FloorPlan();//NSubstitute.Substitute.For<FloorPlan>();
			plan.FloorTiles.AddRange(_FillArea(30, 10, 1));
			plan.IsAgentOnTile(11, 4).Should().BeFalse();

			var agent = new Agent();// NSubstitute.Substitute.For<IAgent>();
			agent.Point = new Point(11, 4);

			plan.AddAgent("Test Agent", agent);
			plan.IsAgentOnTile(11, 4).Should().BeTrue();

			plan.IsAgentOnTile(4, 11).Should().BeFalse();
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
