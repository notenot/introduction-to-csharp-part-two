using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;

namespace Greedy
{
    public class DijkstraData
    {
        public Point? Previous { get; set; }
        public int Price { get; set; }

        public DijkstraData(Point? previous, int price)
        {
            Previous = previous;
            Price = price;
        }
    }

    public class DijkstraPathFinder
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, 
            IEnumerable<Point> targets)
        {
            var track = new Dictionary<Point, DijkstraData> { [start] = new DijkstraData(null, 0) };
            var notVisitedCells = InitNotVisitedCells(state);
            var notVisitedTargets = new HashSet<Point>(targets);
            while (true)
            {
                Point? cellToOpen = null;
                var bestPrice = int.MaxValue;
                foreach (var cell in notVisitedCells)
                    if (track.ContainsKey(cell) && track[cell].Price < bestPrice)
                    {
                        bestPrice = track[cell].Price;
                        cellToOpen = cell;
                    }
                if (cellToOpen == null) yield break;
                if (notVisitedTargets.Contains(cellToOpen.Value))
                {
                    notVisitedTargets.Remove(cellToOpen.Value);
                    yield return GetPathFromStartToTarget(track, cellToOpen);
                    if (notVisitedTargets.Count == 0) yield break;
                }
                OpenCell(state, track, cellToOpen.Value);
                notVisitedCells.Remove(cellToOpen.Value);
            }
        }

        private static PathWithCost GetPathFromStartToTarget(Dictionary<Point, DijkstraData> track, Point? target)
        {
            if (target == null)
                return null;

            var path = new List<Point>();
            var current = target;
            while (current != null)
            {
                path.Add(current.Value);
                current = track[current.Value].Previous;
            }
            path.Reverse();
            return new PathWithCost(track[target.Value].Price, path.ToArray());
        }

        private static void OpenCell(State state, Dictionary<Point, DijkstraData> track, Point cellToOpen)
        {
            var possibleDirections = new[]
            {
                new Point(-1, 0), new Point(0, -1),
                new Point(0, 1), new Point(1, 0)
            };

            var nextCells = possibleDirections
                .Select(point => new Point(point.X + cellToOpen.X, point.Y + cellToOpen.Y))
                .Where(state.InsideMap)
                .Where(point => !state.IsWallAt(point));

            foreach (var cell in nextCells)
            {
                var currentPrice = track[cellToOpen].Price + state.CellCost[cell.X, cell.Y];
                var nextPoint = cell;
                if (!track.ContainsKey(nextPoint) || track[nextPoint].Price > currentPrice)
                    track[nextPoint] = new DijkstraData(cellToOpen, currentPrice);
            }
        }

        private static List<Point> InitNotVisitedCells(State state)
        {
            var result = new List<Point>();
            for (var i = 0; i < state.MapWidth; ++i)
            for (var j = 0; j < state.MapHeight; ++j)
                result.Add(new Point(i, j));
            return result;
        }
    }
}