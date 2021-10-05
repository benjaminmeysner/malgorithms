// <copyright file="ISearchAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Search
{
    using System;

    /// <summary>
    /// Malgorithms.Search.ISearchAlgorithm
    /// </summary>
    public interface ISearchAlgorithm
    {
        /// <summary>
        /// Malgorithms.Search.ISearchAlgorithm.IndexOf
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The index of the item if it exists in the <paramref name="source"/>. Otherwise returns -1.</returns>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>
        public int IndexOf<T>(T[] source, T item, Comparison<T> comparison = null);
    }
}
