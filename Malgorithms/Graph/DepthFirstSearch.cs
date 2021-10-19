// <copyright file="DepthFirstSearch.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Graph
{
    using System;
    using System.Collections.Generic;
    using Malgorithms.Models;

    /// <summary>
    /// Malgorithms.Graph.DepthFirstSearch
    /// </summary>
    public class DepthFirstSearch : BaseGraphAlgorithm, IGraphSearchAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepthFirstSearch" /> class.
        /// </summary>
        public DepthFirstSearch() { }

        /// <summary>
        /// <para />Depth-first search (DFS) is an algorithm for searching a graph or tree data structure for a node that satisfies a given property.
        /// https://en.wikipedia.org/wiki/Depth-first_search
        /// <para />A DFS Search will be at worst O(|E| + |V|) (The sum of the edges + the sum of the vertices as at worst we will need to visit each node in the graph.
        /// <para />A full graph traversal will be worst case due to a complete traversal of the graph.
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="nodeAction">The node action.</param>
        /// <returns>
        /// The node if it exists in the <paramref name="graph" />. Otherwise returns null.
        /// </returns>
        public T Find<T>(T graph, Func<T, bool> predicate, Action<T> nodeAction = null) where T : DirectedGraph<T>
        {
            if (!HasNodes(graph))
            {
                return predicate(graph) ? graph : null;
            }

            return Traverse(graph, new Stack<T>(), new HashSet<T>(), predicate, nodeAction);
        }

        /// <summary>
        /// <para />Depth-first search (DFS) is an algorithm for searching a graph or tree data structure for a node that satisfies a given property.
        /// https://en.wikipedia.org/wiki/Depth-first_search
        /// <para />A DFS Search will be at worst O(|E| + |V|) (The sum of the edges + the sum of the vertices as at worst we will need to visit each node in the graph.
        /// <para />A full graph traversal will be worst case due to a complete traversal of the graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="nodeAction">The node action.</param>
        public void Traverse<T>(T graph, Action<T> nodeAction = null) where T : DirectedGraph<T>
        {
            if (!HasNodes(graph))
            {
                nodeAction(graph);
                return;
            }

            Traverse(graph, new Stack<T>(), new HashSet<T>(), nodeAction: nodeAction);
        }

        /// <summary>
        /// If a predicate it supplied, finds the specified node in the graph which satisfies it.
        /// If an action is supplied, invokes this action on each current node reached.
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="stack">The stack.</param>
        /// <param name="visited">The visited.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="nodeAction">The node action.</param>
        /// <returns>
        /// The node if it exists in the <paramref name="graph" />. Otherwise returns null.
        /// </returns>
        private static T Traverse<T>(T graph, Stack<T> stack, HashSet<T> visited, Func<T, bool> predicate = null, Action<T> nodeAction = null) where T : DirectedGraph<T>
        {
            stack.Push(graph);
            while (stack.Count != 0)
            {
                T current = stack.Pop();
                if (visited.Contains(current))
                {
                    // Cycle detected in graph.
                    continue;
                }

                visited.Add(current);
                NodeAction(nodeAction, current);

                if (!(predicate is null) && predicate(current))
                {
                    return current;
                }

                // Iterative, see below.
                Traverse(current, stack, visited);
            }

            return null;
        }

        /// <summary>
        /// Malgorithms.Graph.DepthFirstSearch.Traverse
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="stack">The stack.</param>
        /// <param name="visited">The visited.</param>
        private static void Traverse<T>(T graph, Stack<T> stack, HashSet<T> visited) where T : DirectedGraph<T>
        {
            foreach (var node in graph.Nodes)
            {
                if (visited.Contains(node))
                {
                    // Cycle detected in graph.
                    continue;
                }

                stack.Push(node);
            }
        }
    }
}
