// <copyright file="BasePermutationAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Permutation
{
    using Malgorithms.Resources;
    using System;

    /// <summary>
    /// Malgorithms.Sort.BasePermutationAlgorithm
    /// </summary>
    public class BasePermutationAlgorithm
    {
        /// <summary>
        /// Validates <paramref name="source"/> for <see cref="Permute{T}(T[])"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <returns>The original source or an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source is null.</exception>
        public T[] PermuteValidation<T>(T[] source, IPermutationAlgorithmOptions options)
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
