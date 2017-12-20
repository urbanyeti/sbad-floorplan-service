using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBad.Map.Navigation
{
    public class AStarPathfinder : IPathfinder
    {
		public AStarPathfinder(int maxRow, int maxCol, decimal[,] weight)
		{
			MaxRow = maxRow;
			MaxCol = maxCol;
			Weight = weight;
		}
		public int MaxRow { get; set; }
		public int MaxCol { get; set; }
		public decimal[,] Weight { get; set; }

		public List<Point> FindPath(Point start, Point end)
		{
			var checkedNodes = new List<Point>();
			var newNodes = new List<Point> { start };
			var parentNodes = new Dictionary<Point, Point>();
			var nodeDistance = new Dictionary<Point, decimal>();
			var expectedDistance = new Dictionary<Point, decimal>();

			try
			{

				// Init
				nodeDistance.Add(start, 0);
				expectedDistance.Add(start, Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y));

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
						var weight = Weight[neighbor.X, neighbor.Y];

						// Diagonal moves take longer
						if (Math.Abs(current.X - neighbor.X) + Math.Abs(current.Y - neighbor.Y) > 1)
						{
							weight *= 1.4m;
						}

						var currentDistance = nodeDistance[current] + weight;

						// Already found shorter path?
						if (checkedNodes.Contains(neighbor) && currentDistance >= nodeDistance[neighbor])
						{
							continue;
						}

						if (!nodeDistance.Keys.Contains(neighbor) || currentDistance < nodeDistance[neighbor])
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
			}

			catch (Exception ex)
			{
				throw new PathNotFound($"Error generating path from {start} to {end}", ex);
			}

			throw new PathNotFound($"Can't find path from {start} to {end}");
		}

		private IEnumerable<Point> _GetNearbyNodes(Point node)
		{
			var nodes = new List<Point>();

			// North
			if (node.Y > 0 && Weight[node.X, node.Y - 1] > 0) { nodes.Add(new Point(node.X, node.Y - 1)); }
			// Northeast
			if (node.Y > 0 && node.X < MaxCol && Weight[node.X + 1, node.Y - 1] > 0) { nodes.Add(new Point(node.X + 1, node.Y - 1)); }
			// East
			if (node.X < MaxCol && Weight[node.X + 1, node.Y] > 0) { nodes.Add(new Point(node.X + 1, node.Y)); }
			// Southeast
			if (node.Y < MaxRow && node.X < MaxCol && Weight[node.X + 1, node.Y + 1] > 0) { nodes.Add(new Point(node.X + 1, node.Y + 1)); }
			// South
			if (node.Y < MaxRow && Weight[node.X, node.Y + 1] > 0) { nodes.Add(new Point(node.X, node.Y + 1)); }
			// Southwest
			if (node.Y < MaxRow &&  node.X > 0 && Weight[node.X - 1, node.Y + 1] > 0) { nodes.Add(new Point(node.X - 1, node.Y + 1)); }
			// West
			if (node.X > 0 && Weight[node.X - 1, node.Y] > 0) { nodes.Add(new Point(node.X - 1, node.Y)); }
			// Northwest
			if (node.Y > 0 && node.X > 0 && Weight[node.X - 1, node.Y - 1] > 0) { nodes.Add(new Point(node.X - 1, node.Y - 1)); }

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
