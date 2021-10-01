// <copyright file="QuickSort.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison
{
    using Malgorithms.Enums;
    using Malgorithms.Helpers;
    using Malgorithms.Sort.Comparison.Options;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Sort.Comparison.QuickSort
    /// </summary>
    /// <seealso cref="Malgorithms.Sort.BaseSortingAlgorithm" />
    /// <seealso cref="Malgorithms.Sort.ISortingAlgorithm" />
    public class QuickSort : BaseSortingAlgorithm, ISortingAlgorithm
    {
        private QuickSortOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickSort"/> class.
        /// </summary>
        /// <param name="options">The <see cref="QuickSort"/> options.</param>
        /// <remarks>
        /// By default, the configuration is Hoare (1959) partition scheme using a 'median-of-three' pivot.
        /// </remarks>
        public QuickSort(Action<QuickSortOptions> options = null) 
        {
            _options = new QuickSortOptions();
            if (!(options is null))
            {
                options(_options);
            }
        }

        /// <summary>
        /// <para />Quick Sort a <see cref="{T}[]" /> <see cref="Array" /> using the configured <see cref="QuickSortPartitionScheme" />.
        /// <para />Hoare (1959) is the default partitioning scheme used if no such configuration is supplied.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> is null.</exception>
        /// <remarks>
        /// <para />The Hoare (1959) scheme runs in linear time complexity O(n) at best, an average of O(n log(n)) and quadratic O(n²) at worst (for a sorted or near sorted source).
        /// <para />The Lomuto scheme is less effecient in that it will degrade to O(n²) when all elements are equal.
        /// <para />The default pivot is picked by the 'median-of-three' technique, 3 indexes into the source (first, middle, last)
        /// and taking the median of the values at those indexes.
        /// </remarks>
        public void Sort<T>(T[] source, Comparison<T> comparison = null)
        {
            SortValidation(source, _options);
            if (!SourceAlreadySorted(source))
            {
                comparison ??= new Comparison<T>((x, y) => Comparer<T>.Default.Compare(x, y)); // Ascending by default.
                Func<T[], int, int, Comparison<T>, T> pivotSelector = GetPivotSelector<T>();
                Func<T[], int, int, Comparison<T>, Func<T[], int, int, Comparison<T>, T>, int> scheme = GetPartitionScheme<T>();
                Func<int, int> highPivot = _options.Scheme == QuickSortPartitionScheme.Hoare ? x => x : x => x - 1;

                Sort(source, _options?.Range?.Start ?? 0, _options?.Range?.End ?? source.Length - 1, comparison, highPivot, pivotSelector, scheme);
            }
        }

        /// <summary>
        /// <para />Quick Sort implementation for a <paramref name="source" /> of type <see cref="{T}" />.
        /// <para />https://en.wikipedia.org/wiki/Quicksort#Optimizations
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        /// <param name="highPivot">The high pivot.</param>
        /// <param name="pivotSelector">The pivot selector.</param>
        /// <param name="scheme">The scheme.</param>
        private void Sort<T>(
            T[] source,
            int low,
            int high,
            Comparison<T> comparison,
            Func<int, int> highPivot,
            Func<T[], int, int, Comparison<T>, T> pivotSelector,
            Func<T[], int, int, Comparison<T>, Func<T[], int, int, Comparison<T>, T>, int> scheme)
        {
            if (low >= high)
            {
                return;
            }

            int pivot = scheme(source, low, high, comparison, pivotSelector);
            Sort(source, low, highPivot(pivot), comparison, highPivot, pivotSelector, scheme);
            Sort(source, pivot + 1, high, comparison, highPivot, pivotSelector, scheme);
        }

        /// <summary>
        /// <para />Lomuto Partition Scheme implementation.
        /// <para />https://en.wikipedia.org/wiki/Quicksort#Lomuto_partition_scheme
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        /// <param name="pivotSelector">The pivot selector.</param>
        /// <returns>
        /// The new partioning index.
        /// </returns>
        private static int LomutoPartitionScheme<T>(
            T[] source,
            int low,
            int high,
            Comparison<T> comparison,
            Func<T[], int, int, Comparison<T>, T> pivotSelector)
        {
            T pivot = pivotSelector(source, low, high, comparison);
            int i = low;

            for (int j = low; j < high; j++)
            {
                if (comparison(source[j], pivot) < 0)
                {
                    THelpers.Swap(ref source[i], ref source[j]);
                    i++;
                }
            }

            THelpers.Swap(ref source[i], ref source[high]);
            return i;
        }

        /// <summary>
        /// <para />Hoare Partition Scheme implementation.
        /// <para />https://en.wikipedia.org/wiki/Quicksort#Hoare_partition_scheme
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        /// <param name="pivotSelector">The pivot selector.</param>
        /// <returns>
        /// The new partioning index.
        /// </returns>
        private static int HoarePartitionScheme<T>(
            T[] source,
            int low,
            int high,
            Comparison<T> comparison,
            Func<T[], int, int, Comparison<T>, T> pivotSelector)
        {
            T pivot = pivotSelector(source, low, high, comparison);
            int i = low - 1;
            var j = high + 1;

            while (true)
            {
                do { i++; } while (comparison(source[i], pivot) < 0);
                do { j--; } while (comparison(source[j], pivot) > 0);

                if (i >= j)
                {
                    return j;
                }

                THelpers.Swap(ref source[i], ref source[j]);
            }
        }

        /// <summary>
        /// Gets the pivot finding mechanism for <see cref="Sort{T}(T[], Comparison{T})" /> based on the configuration.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <returns>
        /// The pivot selector.
        /// </returns>
        private Func<T[], int, int, Comparison<T>, T> GetPivotSelector<T>()
        {
            Func<int, int> highFunc = _options.Scheme == QuickSortPartitionScheme.Hoare ? x => x - 1 : x => x;

            return _options.PivotSelection switch
            {
                QuickSortPivotSelection.Last => (x, low, high, comparison) => x[highFunc(high)],
                QuickSortPivotSelection.Randomised => (x, low, high, comparison) => RandomisedPivotIndex(x, low, highFunc(high)),
                QuickSortPivotSelection.MedianOfThree => (x, low, high, comparison) => MedianOfThreePivotIndex(x, low, highFunc(high), comparison),
                _ => (x, low, high, comparison) => MedianOfThreePivotIndex(x, low, highFunc(high), comparison),
            };
        }

        /// <summary>
        /// Gets partitioning scheme for <see cref="Sort{T}(T[], Comparison{T})"/> based on the configuration.
        /// </summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <returns>
        /// The partitioning scheme.
        /// </returns>
        private Func<T[], int, int, Comparison<T>, Func<T[], int, int, Comparison<T>, T>, int> GetPartitionScheme<T>()
        {
            if (!(_options is null))
            {
                return _options.Scheme switch
                {
                    QuickSortPartitionScheme.Lomuto => (x, low, high, comparison, pivotSelector) => LomutoPartitionScheme(x, low, high, comparison, pivotSelector),
                    QuickSortPartitionScheme.Hoare => (x, low, high, comparison, pivotSelector) => HoarePartitionScheme(x, low, high, comparison, pivotSelector),
                    _ => (x, low, high, comparison, pivotSelector) => HoarePartitionScheme(x, low, high, comparison, pivotSelector),
                };
            }

            return (x, low, high, comparison, pivotSelector) => HoarePartitionScheme(x, low, high, comparison, pivotSelector);
        }

        /// <summary>
        /// Gets the 'median-of-three' pivot from the <paramref name="source" /> subarray defined by the low and high bounds.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>
        /// The pivot value.
        /// </returns>
        private static T MedianOfThreePivotIndex<T>(T[] source, int low, int high, Comparison<T> comparison)
        {
            (T, int) x = (source[low], low);
            (T, int) y = (source[high], high);

            int middleIndex = low + (high - low) / 2;
            (T, int) z = (source[middleIndex], middleIndex);

            var medianOfThree = THelpers.Max(THelpers.Min(x, y, comparison), THelpers.Min(THelpers.Max(x, y, comparison), z, comparison), comparison);
            THelpers.Swap(ref source[medianOfThree.Index], ref source[high]);

            return medianOfThree.Value;
        }

        /// <summary>
        /// Gets a random pivot by accessing a random index in the subarray defined by the low and high bounds.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source"/> type.</typeparam>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <returns>
        /// The pivot value.
        /// </returns>
        private static T RandomisedPivotIndex<T>(T[] source, int low, int high)
        {
            THelpers.Swap(ref source[high], ref source[new Random().Next(low, high + 1)]);
            return source[high]; // This is now the new randomly accessed indexed pivot.
        }

        /// <summary>
        /// Gets or sets the <see cref="QuickSort"/> options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public QuickSortOptions Options
        {
            get => _options;
            set => _options = value;
        }
    }
}
