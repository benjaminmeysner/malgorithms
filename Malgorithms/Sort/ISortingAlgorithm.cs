// <copyright file="ISortingAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort
{
    using System;

    /// <summary>
    /// Malgorithms.Sort.ISortingAlgorithm
    /// </summary>
    public interface ISortingAlgorithm
    {
        /// <summary>
        /// Malgorithms.Sort.ISortingAlgorithm.Sort
        /// </summary>
        /// <typeparam name="T">The <paramref name="source"/> type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>

        public void Sort<T>(T[] source, Comparison<T> comparison = null);
    }
}
