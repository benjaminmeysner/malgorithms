// <copyright file="BinarySearch.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Search
{
    using Malgorithms.Search.Options;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Search.BinarySearch
    /// </summary>
    /// <seealso cref="Malgorithms.Search.BaseSearchAlgorithm" />
    /// <seealso cref="Malgorithms.Search.ISearchAlgorithm" />
    public class BinarySearch : BaseSearchAlgorithm, ISearchAlgorithm
    {
        private BinarySearchOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearch" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BinarySearch(Action<BinarySearchOptions> options = null)
        {
            _options = new BinarySearchOptions();
            if (!(options is null))
            {
                options(_options);
            }
        }

        /// <summary>
        /// <para />Binary Search a <see cref="{T}[]" /> <see cref="Array" /> for <see cref="{T}"/> <paramref name="item"/>.
        /// <par />The original procedure/variant is the default used if no configuration is supplied.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> is null.</exception>
        /// <remarks>
        /// <para />Source must be ordered in that source[i] < source[j] if and only if i < j.
        /// <para />Binary search has a time complexity of O(1) at best, and an average/worst case of O(log n).
        /// </remarks>
        public int IndexOf<T>(T[] source, T item, Comparison<T> comparison = null)
        {
            SearchValidation(source, _options);
            comparison ??= new Comparison<T>((x, y) => Comparer<T>.Default.Compare(x, y)); // Ascending by default.
            return SearchVariant(source, item, _options?.Range?.Start ?? 0, _options?.Range?.End ?? source.Length - 1, comparison);
        }

        /// <summary>
        /// <para />Binary search implementation for a <paramref name="source" /> of type <see cref="{T}" />.
        /// <para />https://en.wikipedia.org/wiki/Binary_search_algorithm#Procedure.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="item">The item.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        private static int OriginalIndexOf<T>(T[] source, T item, int low, int high, Comparison<T> comparison)
        {
            while (low <= high)
            {
                int middle = low + (high - low) / 2;
                if (comparison(item, source[middle]) < 0)
                {
                    high = middle - 1;
                    continue;
                }

                if (comparison(item, source[middle]) > 0)
                {
                    low = middle + 1;
                    continue;
                }

                return middle;
            }

            return IndexNotFound;
        }

        /// <summary>
        /// <para />Expo Binary search implementation for a <paramref name="source" /> of type <see cref="{T}" />.
        /// <para />https://en.wikipedia.org/wiki/Binary_search_algorithm#Exponential_search.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="item">The item.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        private static int ExponentialIndexOf<T>(T[] source, T item, int low, int high, Comparison<T> comparison)
        {
            int bound = low + 1;
            while (bound < high - 1 && comparison(source[bound], item) < 0)
            {
                bound *= 2;
            }

            return OriginalIndexOf(source, item, bound / 2 < low ? low : bound / 2, Math.Min(bound + 1, high), comparison);
        }

        /// <summary>
        /// <para />Directs the binary search to the configured variant.
        /// <para />https://en.wikipedia.org/wiki/Binary_search_algorithm#Variations
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="item">The item.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The index of the found item.</returns>
        private int SearchVariant<T>(T[] source, T item, int low, int high, Comparison<T> comparison)
        {
            return _options.Variant switch
            {
                BinarySearchVariant.Original => OriginalIndexOf(source, item, low, high, comparison),
                BinarySearchVariant.Exponential => ExponentialIndexOf(source, item, low, high, comparison),
                _ => OriginalIndexOf(source, item, low, high, comparison)
            };
        }
    }
}
