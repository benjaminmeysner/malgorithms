// <copyright file="IGraphSearchAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Graph
{
    using Malgorithms.Models;
    using System;

    /// <summary>
    /// Malgorithms.Graph.IGraphSearchAlgorithm
    /// </summary>
    public interface IGraphSearchAlgorithm
    {
        /// <summary>
        /// Malgorithms.Graph.IGraphSearchAlgorithm.Find
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// The the item if it exists in the <paramref name="graph" />. Otherwise returns null.
        /// </returns>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>
        public T Find<T>(T graph, Func<T, bool> predicate) where T : Graph<T>;

        /// <summary>
        /// Malgorithms.Graph.IGraphSearchAlgorithm.Traverse
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        /// The the item if it exists in the <paramref name="graph" />. Otherwise returns null.
        /// </returns>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>
        public void Traverse<T>(T graph, Action<T> action) where T : Graph<T>;
    }
}
