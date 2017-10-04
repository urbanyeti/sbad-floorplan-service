using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBad.Map
{
	public static class FloorExtensions
	{
		public static FloorTile GetNorth(this FloorPlan floorPlan, FloorTile floorTile)
		{
			return floorPlan.FloorTiles.SingleOrDefault(t => t.X == floorTile?.X && t.Y == floorTile?.Y-1);
		}

		public static FloorTile GetSouth(this FloorPlan floorPlan, FloorTile floorTile)
		{
			return floorPlan.FloorTiles.SingleOrDefault(t => t.X == floorTile?.X && t.Y == floorTile?.Y + 1);
		}

		public static FloorTile GetWest(this FloorPlan floorPlan, FloorTile floorTile)
		{
			return floorPlan.FloorTiles.SingleOrDefault(t => t.X == floorTile?.X -1 && t.Y == floorTile?.Y);
		}

		public static FloorTile GetEast(this FloorPlan floorPlan, FloorTile floorTile)
		{
			return floorPlan.FloorTiles.SingleOrDefault(t => t.X == floorTile?.X + 1 && t.Y == floorTile?.Y);
		}

		public static FloorTile GetFloorTile(this FloorPlan floorPlan, int x, int y)
		{
			return floorPlan.FloorTiles.GetFloorTile(x, y);
		}

		public static FloorTile GetFloorTile(this IEnumerable<FloorTile> floorTiles, int x, int y)
		{
			return floorTiles.SingleOrDefault(t => x >= 0 && y >= 0 && t.X == x && t.Y == y);
		}

		public static bool IsAgentOnTile(this FloorPlan floorPlan, FloorTile floorTile)
		{
			if (floorTile == null)
			{
				return false;
			}

			return floorPlan.BotAgents.Any(b => b.X == floorTile.X && b.Y == floorTile.Y);
		}

		public static bool IsAgentOnTile(this FloorPlan floorPlan, int x, int y)
		{
			var tile = floorPlan.GetFloorTile(x, y);
			return floorPlan.IsAgentOnTile(tile);
		}


		public static string Print(this FloorPlan floorPlan, List<Point> path = null)
		{
			if (path == null)
			{
				path = new List<Point>();
			}

			var sb = new StringBuilder();
			var maxRow = floorPlan.FloorTiles.Max(x => x.Y);
			var maxCol = floorPlan.FloorTiles.Max(x => x.X);

			for (int row = 0; row <= maxRow; row++)
			{
				for (int col = 0; col <= maxCol; col++)
				{
					if (floorPlan.IsAgentOnTile(col, row))
					{
						sb.Append("@");
					}
					else if (path.Contains(new Point(col, row)))
					{
						sb.Append(path.IndexOf(new Point(col, row)).ToLetter());
						//sb.Append("=");
					}
					else
					{
						var tile = floorPlan.GetFloorTile(col, row);
						sb.Append(tile.Print());
					}
				}
				sb.AppendLine();
			}
			string output = sb.ToString();


			return output;
		}

		public static string Print(this IEnumerable<FloorTile> floorTiles)
		{
			var sb = new StringBuilder();
			var maxRow = floorTiles.Max(x => x.Y);
			var maxCol = floorTiles.Max(x => x.X);

			for (int row = 0; row <= maxRow; row++)
			{
				for (int col = 0; col <= maxCol; col++)
				{
					var tile = floorTiles.GetFloorTile(col, row);
					sb.Append(tile.Print());
				}
				sb.AppendLine();
			}
			string output = sb.ToString();

			return output;
		}

		public static string Print(this FloorTile floorTile)
		{
			switch(floorTile?.Cost)
			{
				case var c when (c == null || c < 0):
					return "+";
				case var c when (c == 1):
					return "_";
				case var c when (c >= 0 && c <= 9):
					return c.ToString();
				default:
					return "X";
			}
		}

		public static string ToLetter(this int number)
		{
			return Convert.ToChar(number + 65).ToString();
		}
	}
}
