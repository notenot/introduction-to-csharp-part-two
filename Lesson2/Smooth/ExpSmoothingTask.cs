using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
            var smoothed = 0.0;
            var isFirst = true;
            foreach (var point in data)
            {
                point.ExpSmoothedY = isFirst
                    ? point.OriginalY
                    : alpha * point.OriginalY + (1.0 - alpha) * smoothed;
                isFirst = false;
                smoothed = point.ExpSmoothedY;
                
                yield return point;
            }
        }
    }
}