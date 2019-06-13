using System;
using System.Collections.Generic;

namespace yield
{
	public static class MovingMaxTask
	{
        private class PotentialMaxValues
        {
            public double CurrentMax => deque.First.Value.Item2;

            private int currentIndex;
            private int maxLength;
            private LinkedList<Tuple<int, double>> deque;

            public PotentialMaxValues(int windowWidth)
            {
                currentIndex = 0;
                maxLength = windowWidth;
                deque = new LinkedList<Tuple<int, double>>();
            }

            public void Add(double value)
            {
                while (deque.Count != 0 && deque.Last.Value.Item2 <= value)
                    deque.RemoveLast();
                deque.AddLast(new Tuple<int, double>(currentIndex, value));
                ++currentIndex;
                while (deque.First.Value.Item1 < currentIndex - maxLength)
                    deque.RemoveFirst();
            }
        }

        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var values = new PotentialMaxValues(windowWidth);
            foreach (var point in data)
            {
                values.Add(point.OriginalY);
                point.MaxY = values.CurrentMax;
                yield return point;
            }
        }
    }
}