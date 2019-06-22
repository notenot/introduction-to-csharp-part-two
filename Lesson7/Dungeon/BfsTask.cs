using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
	public class BfsTask
	{
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var visitedPoints = new HashSet<Point>();
            var queue = new Queue<SinglyLinkedList<Point>>();
            visitedPoints.Add(start);
            queue.Enqueue(new SinglyLinkedList<Point>(start));
            while (queue.Count != 0)
            {
                var currentPoint = queue.Dequeue();
                var nextPoints = GetAdjacentPoints(currentPoint.Value)
                    .Where(map.InBounds)
                    .Where(point => map.Dungeon[point.X, point.Y] == MapCell.Empty)
                    .Where(point => !visitedPoints.Contains(point));

                foreach (var nextPoint in nextPoints)
                {
                    visitedPoints.Add(nextPoint);
                    var pathPoint = new SinglyLinkedList<Point>(nextPoint, currentPoint);
                    queue.Enqueue(pathPoint);
                    if (chests.Contains(nextPoint))
                        yield return pathPoint;
                }
            }
        }

        private static IEnumerable<Point> GetAdjacentPoints(Point point)
        {
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (dx == 0 || dy == 0)
                    yield return new Point { X = point.X + dx, Y = point.Y + dy };
        }
    }
}