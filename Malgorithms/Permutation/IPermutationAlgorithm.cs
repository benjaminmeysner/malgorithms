// <copyright file="IPermutationAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Permutation
{
    using System;

    /// <summary>
    /// Malgorithms.Permutation.IPermutationAlgorithm
    /// </summary>
    public interface IPermutationAlgorithm
    {
        /// <summary>
        /// Malgorithms.Permutation.IPermutationAlgorithm.Permute
        /// </summary>
        /// <typeparam name="T">The <paramref name="source"/> type.</typeparam>
        /// <param name="source">The source.</param>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>

        public void Permute<T>(T[] source);
    }
}
