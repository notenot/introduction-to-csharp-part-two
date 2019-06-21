using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
            var filteredVisits = visits
                .OrderBy(record => record.UserId)
                .ThenBy(record => record.DateTime)
                .Bigrams()
                .Where(tuple =>
                    tuple.Item1.SlideType == slideType &&
                    tuple.Item1.UserId == tuple.Item2.UserId &&
                    tuple.Item1.SlideId != tuple.Item2.SlideId)
                .Select(tuple => tuple.Item2.DateTime
                    .Subtract(tuple.Item1.DateTime)
                    .TotalMinutes)
                .Where(minutes => 1.0 <= minutes && minutes <= 120.0)
                .ToList();

            return filteredVisits.Count == 0 ? 0.0 : filteredVisits.Median();
        }
	}
}