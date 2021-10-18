// <copyright file="BaseGraphAlgorithm.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Graph
{
    using Malgorithms.Models;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.Graph.BaseGraphAlgorithm
    /// </summary>
    public class BaseGraphAlgorithm
    {
        /// <summary>
        /// Checks whether the <paramref name="graph"/> has any nodes to visit.
        /// </summary>
        /// <typeparam name="T">The graph type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <returns>
        /// True if has nodes else false.
        /// </returns>
        protected static bool HasNodes<T>(T graph) where T : DirectedGraph<T>
        {
            return graph.Nodes != null && graph.Nodes.Any();
        }

        /// <summary>
        /// Performs the action on a node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeAction">The node action.</param>
        /// <param name="node">The node.</param>
        protected static void NodeAction<T>(Action<T> nodeAction, T node)
        {
            if (!(nodeAction is null))
            {
                nodeAction(node);
            }
        }
    }
}
