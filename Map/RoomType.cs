namespace SBad.Map
{
	public class RoomPlan
	{
		public string Name { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int WallValue { get; set; } = 0;
		public int FloorValue { get; set; }
		public DoorTile DoorTile { get; set; } = null;
	}
}