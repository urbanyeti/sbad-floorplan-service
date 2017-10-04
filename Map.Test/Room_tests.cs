﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using FluentAssertions;
using System.Collections.Generic;

namespace SBad.Map.Test
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
				DoorTile = new DoorTile(1, 0, cost: 3, notes: "Door")
			};

			FloorRoom room = service.GenerateRoom(roomType);
			string display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("535" + Environment.NewLine + "5_5" + Environment.NewLine + "555" + Environment.NewLine);

			// East door
			roomType.DoorTile = new DoorTile(2, 1, cost: 3, notes: "Door");
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("555" + Environment.NewLine + "5_3" + Environment.NewLine + "555" + Environment.NewLine);

			// South door
			roomType.DoorTile = new DoorTile(1, 2, cost: 3, notes: "Door");
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("555" + Environment.NewLine + "5_5" + Environment.NewLine + "535" + Environment.NewLine);

			// West door
			roomType.DoorTile = new DoorTile(0, 1, cost: 3, notes: "Door");
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("555" + Environment.NewLine + "3_5" + Environment.NewLine + "555" + Environment.NewLine);

			// North door wide
			roomType.Width = 4;
			roomType.DoorTile = new DoorTile(1, 0, cost: 3, notes: "Door");
			room = service.GenerateRoom(roomType);
			display = room.FloorTiles.Print();
			Debug.WriteLine(display);
			display.ShouldBeEquivalentTo("5355" + Environment.NewLine + "5__5" + Environment.NewLine + "5555" + Environment.NewLine);
		}
	}
}
