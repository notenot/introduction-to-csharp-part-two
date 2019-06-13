using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
            var window = new Queue<double>();
            var sum = 0.0;
            foreach (var point in data)
            {
                if (window.Count == windowWidth)
                    sum -= window.Dequeue();

                sum += point.OriginalY;
                window.Enqueue(point.OriginalY);
                point.AvgSmoothedY = sum / window.Count;

                yield return point;
            }
        }
	}
}