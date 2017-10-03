using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBad.Map.Navigation
{
    public class AStarPathfinder : IPathfinder
    {
		public AStarPathfinder(int maxRow, int maxCol, int[,] weight)
		{
			MaxRow = maxRow;
			MaxCol = maxCol;
			Weight = weight;
		}
		public int MaxRow { get; set; }
		public int MaxCol { get; set; }
		public int[,] Weight { get; set; }

		public List<Point> FindPath(Point start, Point end)
		{
			var checkedNodes = new List<Point>();
			var newNodes = new List<Point> { start };
			var parentNodes = new Dictionary<Point, Point>();
			var nodeDistance = new Dictionary<Point, int>();
			var expectedDistance = new Dictionary<Point, float>();

			// Init
			nodeDistance.Add(start, 0);
			expectedDistance.Add(start, 0 + +Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y));

			while (newNodes.Count > 0)
			{
				// Get the node with the lowest estimated cost to finish
				var current = newNodes.OrderBy(x => expectedDistance[x]).First();
				if (current.X == end.X && current.Y == end.Y)
				{
					// We're done-- grab the most direct route
					return _ShortestPath(parentNodes, end);
				}
				// Node is checked
				newNodes.Remove(current);
				checkedNodes.Add(current);

				foreach (var neighbor in _GetNearbyNodes(current))
				{
					var currentDistance = nodeDistance[current] + Weight[neighbor.X, neighbor.Y];

					// Already found shorter path?
					if (checkedNodes.Contains(neighbor) && currentDistance >= nodeDistance[neighbor])
					{
						continue;
					}

					if (!checkedNodes.Contains(neighbor) || currentDistance < nodeDistance[neighbor])
					{
						if (parentNodes.Keys.Contains(neighbor))
						{
							parentNodes[neighbor] = current;
						}
						else
						{
							parentNodes.Add(neighbor, current);
						}

						nodeDistance[neighbor] = currentDistance;
						expectedDistance[neighbor] = nodeDistance[neighbor] + Math.Abs(neighbor.X - end.X) + Math.Abs(neighbor.Y - end.Y);

						if (!newNodes.Contains(neighbor))
						{
							newNodes.Add(neighbor);
						}
					}
				}
			}

			throw new Exception($"Can't find path from ({start.X},{start.Y}) to ({end.X},{end.Y})");
		}

		private IEnumerable<Point> _GetNearbyNodes(Point node)
		{
			var nodes = new List<Point>();

			// North
			if (node.Y > 0 && Weight[node.X, node.Y - 1] > 0) { nodes.Add(new Point(node.X, node.Y - 1)); }
			// Northeast
			if (node.Y > 0 && Weight[node.X + 1, node.Y - 1] > 0) { nodes.Add(new Point(node.X + 1, node.Y - 1)); }
			// East
			if (node.X < MaxCol && Weight[node.X + 1, node.Y] > 0) { nodes.Add(new Point(node.X + 1, node.Y)); }
			// Southeast
			if (node.Y < MaxRow && Weight[node.X + 1, node.Y + 1] > 0) { nodes.Add(new Point(node.X + 1, node.Y + 1)); }
			// South
			if (node.Y < MaxRow && Weight[node.X, node.Y + 1] > 0) { nodes.Add(new Point(node.X, node.Y + 1)); }
			// Southwest
			if (node.X > 0 && Weight[node.X - 1, node.Y + 1] > 0) { nodes.Add(new Point(node.X - 1, node.Y + 1)); }
			// West
			if (node.X > 0 && Weight[node.X - 1, node.Y] > 0) { nodes.Add(new Point(node.X - 1, node.Y)); }
			// Northwest
			if (node.X > 0 && Weight[node.X - 1, node.Y - 1] > 0) { nodes.Add(new Point(node.X - 1, node.Y - 1)); }

			return nodes;
		}

		private List<Point> _ShortestPath(Dictionary<Point, Point> parentNodes, Point current)
		{
			if (!parentNodes.Keys.Contains(current))
			{
				return new List<Point> { current };
			}

			var path = _ShortestPath(parentNodes, parentNodes[current]);
			path.Add(current);
			return path;
		}
	}
}
