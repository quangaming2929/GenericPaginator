using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagiSekaiTechnologies.GenericPaginator
{
    internal static class NumberHelper
    {
        public static long NormalizeIndex(this long index, long count)
        {
            if (count == 0) return 0;

            return index < 0 ?
                count + (index % count) :
                index % count;
        }

        public static int NormalizeIndex(this int index, int count)
        {
            if (count == 0) return 0;

            return index < 0 ?
                count + (index % count) :
                index % count;
        }

        public static double[] BezierCurve(int dimension, List<double[]> points, double delta)
        {
            double[] result = new double[dimension];

            for (int dimensionIndex = 0; dimensionIndex < dimension; dimensionIndex++)
            {
                double axisResult = 0;

                for (int i = 0; i < points.Count; i++)
                {
                    int n = points.Count - 1;
                    axisResult += points[i][dimensionIndex] * (Factorial(n) / (Factorial(i) * Factorial(n - i))) * Math.Pow(1 - delta, n - i) * Math.Pow(delta, i);
                }

                result[dimensionIndex] = axisResult;
            }

            return result;
        }

        public static double CubicBezier(double xStart, double yStart, double xEnd, double yEnd, double delta)
        {
            return BezierCurve(2, new List<double[]>() { new double[] { 0, 0 }, new double[] { xStart, yStart }, new double[] { xEnd, yEnd }, new double[] { 1, 1 } }, delta)[1];
        }

        public static double Lerp(double firstFloat, double secondFloat, double by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static long Factorial(long num)
        {

            long result = 1;
            for (long i = num; i > 0; i--)
            {
                result *= i;
            }

            return result;
        }

        private static Random rnd = new Random();
        public static T SelectItem<T>(this Dictionary<T, double> items) where T : class
        {
            // Calculate the summa of all portions.
            double poolSize = 0;
            foreach (var item in items)
            {
                poolSize += item.Value;
            }
            var mult = Math.Pow(10, Mult10UntilInt(poolSize));
            var poolSizeInt = (int)Math.Round(poolSize * mult);

            // Get a random integer from 0 to PoolSize.
            int randomNumber = rnd.Next(0, poolSizeInt + 1);

            // Detect the item, which corresponds to current random number.
            int accumulatedProbability = 0;
            foreach (var item in items)
            {
                accumulatedProbability += (int)Math.Round(item.Value * mult);
                if (randomNumber <= accumulatedProbability)
                    return item.Key;

            }
            return null;
        }

        public static long Mult10UntilInt(double i)
        {
            return 5;
        }

        public static double[] Flattern(double[] num, double delta)
        {
            var aver = num.Sum() / num.Length;
            return num.Select(x => Lerp(x, aver, delta)).ToArray();
        }

        public static bool Random(double percentage)
        {
            return rnd.NextDouble() < percentage;
        }

        #region IsInRange Overrides
        public static bool IsInRange(double value, double min, double max)
        {
            return min <= value && value <= max;
        }

        public static bool IsInRange(int value, int min, int max)
        {
            return min <= value && value <= max;
        }
        #endregion

        public static T GetRandomElement<T>(this IList<T> items)
        {
            return items.Count == 0 ? default : items[rnd.Next(0, items.Count)];
        }
    }
}
