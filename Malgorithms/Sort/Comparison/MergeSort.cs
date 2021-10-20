// <copyright file="MergeSort.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison
{
    using Malgorithms.Resources;
    using Malgorithms.Sort.Comparison.Options;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Sort.Comparison.MergeSort
    /// </summary>
    /// <seealso cref="Malgorithms.Sort.BaseSortingAlgorithm" />
    /// <seealso cref="Malgorithms.Sort.ISortingAlgorithm" />
    public class MergeSort : BaseSortingAlgorithm, ISortingAlgorithm
    {
        private MergeSortOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeSort"/> class.
        /// </summary>
        /// <param name="options">The <see cref="MergeSort"/> options.</param>
        public MergeSort(Action<MergeSortOptions> options = null)
        {
            _options = new MergeSortOptions();
            if (!(options is null))
            {
                options(_options);
            }
        }

        /// <summary>
        /// <para />Merge Sort a <see cref="{T}[]" /> <see cref="Array" /> using the configured <see cref="MergeSortOptions.Variant" />.
        /// <para />Top down is the default variant used if no such configuration is supplied.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> is null.</exception>
        /// <remarks>
        /// <para />The top down variant runs in O(n log (n)) at best, an average of O(n log(n)) and O(n log (n)) at worst (for a sorted or near sorted source).
        /// <para />The natural variant is an improvement at O(n) at best, an average of O(n log(n)) and O(n log (n)) at worst.
        /// </remarks>
        public void Sort<T>(T[] source, Comparison<T> comparison = null)
        {
            SortValidation(source, _options);
            if (!SourceAlreadySorted(source))
            {
                comparison ??= new Comparison<T>((x, y) => Comparer<T>.Default.Compare(x, y)); // Ascending by default.
                SortVariant(source, _options?.Range?.Start ?? 0, _options?.Range?.End ?? source.Length, comparison);
            }
        }

        /// <summary>
        /// <para />Top down merge sort implementation for a <paramref name="source" /> of type <see cref="{T}" />.
        /// <para />https://en.wikipedia.org/wiki/Merge_sort#Top-down_implementation.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        private void TopDownSort<T>(T[] copy, int low, int high, T[] source, Comparison<T> comparison)
        {
            if (high - low < 2)
            {
                return;
            }

            int middle = (high + low) / 2;
            TopDownSort(source, low, middle, copy, comparison);
            TopDownSort(source, middle, high, copy, comparison);
            TopDownMerge(copy, low, middle, high, source, comparison);
        }

        /// <summary>
        /// <para />Top down merge sort implementation for a <paramref name="source" /> of type <see cref="{T}" />.
        /// <para />https://en.wikipedia.org/wiki/Merge_sort#Top-down_implementation.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="middle">The middle.</param>
        /// <param name="high">The high.</param>
        /// <param name="copy">The copy.</param>
        /// <param name="comparison">The comparison.</param>
        private static void TopDownMerge<T>(T[] source, int low, int middle, int high, T[] copy, Comparison<T> comparison)
        {
            int i = low;
            int j = middle;

            // While there are elements in the left or right runs...
            for (int k = low; k < high; k++)
            {
                // If left run head exists and is <= existing right run head.
                if (i < middle && (j >= high || comparison(source[i], source[j]) <= 0))
                {
                    copy[k] = source[i];
                    i++;
                    continue;
                }

                copy[k] = source[j];
                j++;
            }
        }

        /// <summary>
        /// <para />Directs the merge sort to the configured variant.
        /// <para />https://en.wikipedia.org/wiki/Merge_sort#Variants
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="comparison">The comparison.</param>
        private void SortVariant<T>(T[] source, int low, int high, Comparison<T> comparison)
        {
            if (!(_options is null))
            {
                switch (_options.Variant)
                {
                    case MergeSortVariant.TopDown:
                        T[] b = new T[source.Length];
                        Array.Copy(source, b, source.Length);
                        TopDownSort(b, low, high, source, comparison);
                        break;
                    case MergeSortVariant.Natural:
                    case MergeSortVariant.BottomUp:
                        throw new NotImplementedException(StandardText.AlgorithmNotImplemented);
                }
            }
        }
    }
}
