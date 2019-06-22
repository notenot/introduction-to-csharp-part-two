using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var pathFromStartToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit })
                .FirstOrDefault();
            if (pathFromStartToExit == null)
                return new MoveDirection[0];

            if (map.Chests.Any(chest => pathFromStartToExit.ToList().Contains(chest)))
                return ConvertToDirections(pathFromStartToExit, true);

            var pathsFromStart = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
            var pathsFromExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
            var shortestPath = pathsFromStart
                .Join(pathsFromExit, path => path.Value, path => path.Value, ConvertToDirections)
                .OrderBy(path => path.Length)
                .FirstOrDefault();
            return shortestPath ?? ConvertToDirections(pathFromStartToExit, true);
        }

        public static MoveDirection[] ConvertToDirections(
            SinglyLinkedList<Point> fullPath, bool reverse = false)
        {
            var list = fullPath.ToList();
            if (reverse)
                list.Reverse();
            return list
                .Zip(list.Skip(1), (before, after) => 
                    Walker.ConvertOffsetToDirection(new Size(after.X - before.X, after.Y - before.Y)))
                .ToArray();
        }

        private static MoveDirection[] ConvertToDirections(
            SinglyLinkedList<Point> pathFromStart, 
            SinglyLinkedList<Point> pathFromExit) => 
            ConvertToDirections(pathFromStart, true)
                .Concat(ConvertToDirections(pathFromExit))
                .ToArray();
    }
}