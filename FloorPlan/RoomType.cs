namespace SBad.FloorPlan
{
	public class RoomPlan
	{
		public string Name { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int WallValue { get; set; }
		public int FloorValue { get; set; }
		public int DoorValue { get; set; }
		public Direction DoorWall { get; set; }
	}
}