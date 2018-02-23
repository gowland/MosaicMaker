using System;
using System.Collections.Generic;

namespace CommonUtilities
{
    public static class TwoDimensionalArrayExtensions
    {
        public static IEnumerable<TInput> Flatten<TInput>(this TInput[,] items)
        {
            int d0 = items.GetLength(0);
            int d1 = items.GetLength(1);
            for (int i0 = 0; i0 < d0; i0 += 1)
            for (int i1 = 0; i1 < d1; i1 += 1)
                yield return items[i0, i1];
        }

        public static TOutput[,] Select<TInput, TOutput>(this TInput[,] items, Func<TInput, TOutput> f)
        {
            int d0 = items.GetLength(0);
            int d1 = items.GetLength(1);
            TOutput[,] result = new TOutput[d0, d1];
            for (int i0 = 0; i0 < d0; i0 += 1)
            for (int i1 = 0; i1 < d1; i1 += 1)
                result[i0, i1] = f(items[i0, i1]);
            return result;
        }

        public static IEnumerable<T> Select<T>(this T[,] items)
        {
            int d0 = items.GetLength(0);
            int d1 = items.GetLength(1);
            for (int i0 = 0; i0 < d0; i0 += 1)
            for (int i1 = 0; i1 < d1; i1 += 1)
                yield return items[i0, i1];
        }

        public static void Foreach<T>(this T[,] items, Action<int, int, T> action)
        {
            int d0 = items.GetLength(0);
            int d1 = items.GetLength(1);
            for (int i0 = 0; i0 < d0; i0 += 1)
            for (int i1 = 0; i1 < d1; i1 += 1)
                action(i0, i1, items[i0, i1]);
        }
    }
}
