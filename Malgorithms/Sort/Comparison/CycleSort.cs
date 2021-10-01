// <copyright file="CycleSort.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison
{
    using Malgorithms.Helpers;
    using Malgorithms.Sort.Comparison.Options;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Sort.Comparison.CycleSort
    /// </summary>
    /// <seealso cref="Malgorithms.Sort.BaseSortingAlgorithm" />
    /// <seealso cref="Malgorithms.Sort.ISortingAlgorithm" />
    public class CycleSort : BaseSortingAlgorithm, ISortingAlgorithm
    {
        private CycleSortOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CycleSort"/> class.
        /// </summary>
        /// <param name="options">The <see cref="CycleSort"/> options.</param>
        public CycleSort(Action<CycleSortOptions> options = null)
        {
            _options = new CycleSortOptions();
            if (!(options is null))
            {
                options(_options);
            }
        }

        /// <summary>
        /// <para />Cycle Sort a <see cref="{T}[]" /> <see cref="Array" /> using the configured <see cref="CycleSortOptions" />.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> is null.</exception>
        /// <remarks>
        /// <para />Cycle Sort is an in-place, unstable sorting algorithm,
        /// a comparison sort that is theoretically optimal in terms of the total number of writes to the original array, unlike any other in-place sorting algorithm.
        ///<para/> Cycle Sort has a best, average and worst time complexity of quadratic O(n²).
        /// </remarks>
        public void Sort<T>(T[] source, Comparison<T> comparison = null)
        {
            SortValidation(source, _options);
            if (!SourceAlreadySorted(source))
            {
                comparison ??= new Comparison<T>((x, y) => Comparer<T>.Default.Compare(x, y)); // Ascending by default.
                Sort(source, _options?.Range?.Start ?? 0, _options?.Range?.End ?? source.Length - 1, comparison);
            }
        }

        /// <summary>
        /// <para />Cycle Sort implementation for a <paramref name="source" /> of type <see cref="{T}" />.
        /// <para />https://en.wikipedia.org/wiki/Cycle_sort
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        private static void Sort<T>(T[] source, int low, int high, Comparison<T> comparison = null)
        {
            for (int i = low; i < high; i++)
            {
                int j = i;
                T item = source[i];

                for(int k = i + 1; k < high + 1; k++)
                {
                    if (comparison(source[k], item) < 0)
                    {
                        j++;
                    }
                }

                if (i == j)
                {
                    continue;
                }

                while (comparison(item, source[j]) == 0)
                {
                    j++;
                }

                THelpers.Swap(ref source[j], ref item);

                while(j != i)
                {
                    j = i;

                    for(int m = i + 1; m < high + 1; m++)
                    {
                        if (comparison(source[m], item) < 0)
                        {
                            j++;
                        }
                    }

                    while (comparison(item, source[j]) == 0)
                    {
                        j++;
                    }

                    THelpers.Swap(ref source[j], ref item);
                }
            }
        }
    }
}
