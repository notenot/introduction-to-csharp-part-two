using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            if (state.Goal > state.Chests.Count)
                return new List<Point>();

            var dijkstra = new DijkstraPathFinder();
            var notVisitedChests = new HashSet<Point>(state.Chests);
            var visitedChestCount = 0;
            var currentEnergy = 0;
            var currentCell = state.Position;
            var pathToGoal = new List<Point>();

            while (true)
            {
                var path = dijkstra.GetPathsByDijkstra(state, currentCell, notVisitedChests).FirstOrDefault();
                if (path == null) return new List<Point>();

                notVisitedChests.Remove(path.End);
                ++visitedChestCount;
                currentEnergy += path.Cost;
                currentCell = path.End;
                pathToGoal.AddRange(path.Path.Skip(1));

                if (visitedChestCount == state.Goal && currentEnergy <= state.Energy) break;
                if (currentEnergy >= state.Energy) return new List<Point>();
            }

            return pathToGoal;
        }
    }
}