// <copyright file="BaseSortingAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort
{
    using Malgorithms.Resources;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.Sort.BaseSortingAlgorithm
    /// </summary>
    public class BaseSortingAlgorithm
    {
        /// <summary>
        /// Determines whether the source is already sorted.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source"/> type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>true if the <paramref name="source"/> is already sorted; otherwise, false. The default is false.</returns>
        protected static bool SourceAlreadySorted<T>(T[] source)
        {
            // [.Any() (O(1))] || [.Length (O(1))]
            return !source.Any() || source.Length <= 1;
        }

        /// <summary>
        /// Validates <paramref name="source"/> for <see cref="Sort{T}(T[], Comparison{T})"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <returns>The original source or an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source is null.</exception>
        public T[] SortValidation<T>(T[] source, ISortingAlgorithmOptions options)
        {
            if (source is null)
            {
                throw new ArgumentNullException(string.Format(StandardText.ParameterCannotBeNull, nameof(source)));
            }

            if (options != null && options.Range.HasValue
                && (options.Range.Value.Start < 0 || options.Range.Value.End > source.Length - 1))
            {
                throw new ArgumentException(string.Format(StandardText.ParameterValueInvalid, "Range"));
            }

            return source;
        }
    }
}
