// <copyright file="THelpers.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Helpers
{
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.Helpers.THelpers
    /// </summary>
    internal static class THelpers
    {
        /// <summary>
        /// Swaps both of the elements.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        /// <summary>
        /// Splits the specified array into two halves at the middle.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <returns></returns>
        public static (T[] Left, T[] Right) Split<T>(T[] source)
        {
            int middle = source.Length / 2;
            return (source.Take(middle).ToArray(), source.Skip(middle).ToArray());
        }

        /// <summary>
        /// Determines the maximum of the parameters.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>
        /// The maximum of the parameters.
        /// </returns>
        public static (T Value, int Index) Max<T>((T, int) left, (T, int) right, Comparison<T> comparer)
        {
            return comparer(left.Item1, right.Item1) > 0 ? left : right;
        }

        /// <summary>
        /// Determines the minimum of the parameters.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>
        /// The minimum of the parameters.
        /// </returns>
        public static (T Value, int Index) Min<T>((T, int) left, (T, int) right, Comparison<T> comparer)
        {
            return comparer(left.Item1, right.Item1) < 0 ? left : right;
        }
    }
}
