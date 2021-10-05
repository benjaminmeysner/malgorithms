// <copyright file="BaseSearchAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Search
{
    using Malgorithms.Resources;
    using System;

    /// <summary>
    /// Malgorithms.Search.BaseSearchAlgorithm
    /// </summary>
    public class BaseSearchAlgorithm
    {
        /// <summary>
        /// The index not found constant.
        /// </summary>
        public const int IndexNotFound = -1;

        /// <summary>
        /// Validates <paramref name="source"/> for <see cref="Search{T}(T[], {T}, Comparison{T})"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="options">The options.</param>
        /// <returns>The original source or an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source is null.</exception>
        public static T[] SearchValidation<T>(T[] source, ISearchAlgorithmOptions options)
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
