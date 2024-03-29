﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBad.Nav;
using SBad.Nav.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBad.Nav.Test
{
	[TestClass]
	public class Agent_tests
	{
		[TestMethod]
		public void Agent_follows_path()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			var plan = new FloorPlan(5, 4, fill:true);

			var agent = new Agent();// NSubstitute.Substitute.For<Agent>();
			agent.Move(new Location(1, 1));
			plan.AddAgent("Test Agent", agent);

			NavigationService navService = NSubstitute.Substitute.For<NavigationService>(plan);
			agent.SetPath(navService.FindPath(agent.Point, new Location(3, 2)));

			var display = plan.Print(agent.Path);
			Debug.WriteLine(display);

			agent.Path.Count.Should().Be(3);

			display.Should().BeEquivalentTo("+++++" + Environment.NewLine + "+@__+" + Environment.NewLine + "+_BC+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath(plan).Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.Should().BeEquivalentTo("+++++" + Environment.NewLine + "+A__+" + Environment.NewLine + "+_@C+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath(plan).Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.Should().BeEquivalentTo("+++++" + Environment.NewLine + "+A__+" + Environment.NewLine + "+_B@+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath(plan).Should().BeFalse();
		}

		[TestMethod]
		public void Agents_attack_tiles()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			var plan = new FloorPlan(3, 3, fill:true);

			var agent = new Agent(); //NSubstitute.Substitute.For<Agent>();
			agent.Move(new Location(1, 1));
			plan.AddAgent("Test Agent", agent);

			var tile = plan.GetFloorTile(2, 2);
			agent.AttackTile(tile).Should().BeFalse();

			tile = plan.GetFloorTile(1, 2);
			tile.Cost = 2;
			agent.AttackTile(tile).Should().BeTrue();
			tile.Cost.Should().Be(1);

			agent.AttackTile(tile).Should().BeFalse();
			tile.Cost.Should().Be(1);
		}
	}
}
