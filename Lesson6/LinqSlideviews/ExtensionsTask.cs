using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
        {
            var sortedItems = items.OrderBy(item => item).ToArray();
            var length = sortedItems.Length;
            if (length == 0)
                throw new InvalidOperationException();

            var middle = length / 2;
            return length % 2 != 0
                ? sortedItems[middle]
                : (sortedItems[middle - 1] + sortedItems[middle]) / 2;
        }

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            using (var enumerator = items.GetEnumerator())
            { 
                if (!enumerator.MoveNext())
                    yield break;

                var previous = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    yield return Tuple.Create(previous, enumerator.Current);
                    previous = enumerator.Current;
                }
            }
        }
	}
}