using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBad.Map;
using SBad.Map.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBad.Map.Test
{
	[TestClass]
	public class Agent_tests
	{
		[TestMethod]
		public void Agent_follows_path()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			FloorPlan plan = new FloorPlan(5, 4, fill:true);

			BotAgent agent = NSubstitute.Substitute.For<BotAgent>();
			agent.Point = new Point(1, 1);
			plan.BotAgents.Add(agent);

			NavigationService navService = NSubstitute.Substitute.For<NavigationService>(plan);
			agent.SetPath(navService.FindPath(agent.Point, new Point(3, 2)));

			var display = plan.Print(agent.Path);
			Debug.WriteLine(display);

			agent.Path.Count.ShouldBeEquivalentTo(3);

			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+@__+" + Environment.NewLine + "+_BC+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath(plan).Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+A__+" + Environment.NewLine + "+_@C+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath(plan).Should().BeTrue();
			display = plan.Print(agent.Path);
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("+++++" + Environment.NewLine + "+A__+" + Environment.NewLine + "+_B@+"
				+ Environment.NewLine + "+++++" + Environment.NewLine);

			agent.FollowPath(plan).Should().BeFalse();
		}

		[TestMethod]
		public void Agents_attack_tiles()
		{
			RoomService roomService = NSubstitute.Substitute.For<RoomService>();
			FloorPlan plan = new FloorPlan(3, 3, fill:true);

			BotAgent agent = NSubstitute.Substitute.For<BotAgent>();
			agent.Point = new Point(1, 1);
			plan.BotAgents.Add(agent);

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
