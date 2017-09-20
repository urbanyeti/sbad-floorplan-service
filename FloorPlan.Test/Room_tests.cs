using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using FluentAssertions;
using System.Collections.Generic;

namespace SBad.FloorPlan.Test
{
	[TestClass]
	public class Room_tests
	{
		[TestMethod]
		public void RoomService_generates_room()
		{
			RoomService service = NSubstitute.Substitute.For<RoomService>();

			// North door
			RoomPlan roomType = new RoomPlan
			{
				Name = "Test",
				Width = 3,
				Height = 3,
				WallValue = 5,
				FloorValue = 1,
				DoorValue = 3,
				DoorWall = Direction.North
			};

			FloorRoom room = service.GenerateRoom(roomType);
			string display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("535" + Environment.NewLine + "515" + Environment.NewLine + "555" + Environment.NewLine);

			// East door
			roomType.DoorWall = Direction.East;
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("555" + Environment.NewLine + "513" + Environment.NewLine + "555" + Environment.NewLine);

			// South door
			roomType.DoorWall = Direction.South;
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("555" + Environment.NewLine + "515" + Environment.NewLine + "535" + Environment.NewLine);

			// West door
			roomType.DoorWall = Direction.West;
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("555" + Environment.NewLine + "315" + Environment.NewLine + "555" + Environment.NewLine);

			// North door wide
			roomType.Width = 4;
			roomType.DoorWall = Direction.North;
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("5355" + Environment.NewLine + "5115" + Environment.NewLine + "5555" + Environment.NewLine);
		}
	}
}
