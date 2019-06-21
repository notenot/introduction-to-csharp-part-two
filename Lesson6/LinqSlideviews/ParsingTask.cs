using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            return lines
                .Skip(1)
                .Select(line =>
                {
                    var slideInfo = line.Split(';');
                    if (slideInfo.Length == 3 &&
                        int.TryParse(slideInfo[0], out var slideId) &&
                        Enum.TryParse(slideInfo[1], true, out SlideType slideType))
                        return new SlideRecord(slideId, slideType, slideInfo[2]);
                    return null;
                })
                .Where(record => record != null)
                .ToDictionary(record => record.SlideId);
        }

        private static SlideType ParseSlideType(string value)
        {
            if (Enum.TryParse(value, true, out SlideType result))
                return result;
            throw new FormatException("Unknown slide type");
        }

		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
		/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
		/// Такой словарь можно получить методом ParseSlideRecords</param>
		/// <returns>Список информации о посещениях</returns>
		/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
                .Skip(1)
                .Select(line =>
                {
                    var visitInfo = line.Split(';');
                    if (visitInfo.Length == 4 && 
                        int.TryParse(visitInfo[0], out var userId) && 
                        int.TryParse(visitInfo[1], out var slideId) && 
                        slides.ContainsKey(slideId) &&
                        DateTime.TryParse($"{visitInfo[2]} {visitInfo[3]}", out var date))
                        return new VisitRecord(userId, slideId, date, slides[slideId].SlideType);
                    throw new FormatException($"Wrong line [{line}]");
                });
        }
	}
}