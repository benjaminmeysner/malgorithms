// <copyright file="BreadthFirstSearch.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Graph
{
    using System;
    using System.Collections.Generic;
    using Malgorithms.Exceptions;
    using Malgorithms.Models;
    using Malgorithms.Resources;

    /// <summary>
    /// Malgorithms.Graph.BreadthFirstSearch
    /// </summary>
    public class BreadthFirstSearch : BaseGraphAlgorithm, IGraphSearchAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadthFirstSearch" /> class.
        /// </summary>
        public BreadthFirstSearch() { }

        /// <summary>
        /// <para />Breadth-first search (BFS) is an algorithm for searching a tree data structure for a node that satisfies a given property.
        /// <para />This will throw if any cyclic (non-Tree structure) dependency is found during traversal.
        /// https://en.wikipedia.org/wiki/Breadth-first_search
        /// <para />A BFS Search will be at worst O(|E| + |V|) (The sum of the edges + the sum of the vertices as at worst we will need to visit each node in the tree.
        /// <para />A full tree traversal will be worst case due to a complete traversal of the tree.
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// The node if it exists in the <paramref name="graph" />. Otherwise returns null.
        /// </returns>
        public T Find<T>(T graph, Func<T, bool> predicate) where T : Graph<T>
        {
            if (!HasNodes(graph))
            {
                return predicate(graph) ? graph : null;
            }

            return Traverse(graph, new Queue<T>(), new List<T>(), predicate);
        }

        /// <summary>
        /// Breadth-first traversal (BFS) is an algorithm for traversing a tree data structure.
        /// This will throw if any cyclic (non-Tree structure) dependency is found during traversal.
        /// https://en.wikipedia.org/wiki/Breadth-first_search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="nodeAction">The node action.</param>
        public void Traverse<T>(T graph, Action<T> nodeAction = null) where T : Graph<T>
        {
            if (!HasNodes(graph))
            {
                nodeAction(graph);
                return;
            }

            Traverse(graph, new Queue<T>(), new List<T>(), nodeAction: nodeAction);
        }

        /// <summary>
        /// If a predicate it supplied, finds the specified node in the graph which satisfies it.
        /// If an action is supplied, invokes this action on each current node reached.
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="queue">The queue.</param>
        /// <param name="visited">The visited.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="nodeAction">The node action.</param>
        /// <returns>
        /// The node if it exists in the <paramref name="graph" />. Otherwise returns null.
        /// </returns>
        private static T Traverse<T>(T graph, Queue<T> queue, List<T> visited, Func<T, bool> predicate = null, Action<T> nodeAction = null) where T : Graph<T>
        {
            queue.Enqueue(graph);
            while (queue.Count != 0)
            {
                T current = queue.Dequeue();
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
                Traverse(current, queue, visited);
            }

            return null;
        }

        /// <summary>
        /// Malgorithms.Graph.BreadthFirstSearch.Traverse
        /// </summary>
        /// <typeparam name="T">The <paramref name="graph" /> type.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="queue">The queue.</param>
        /// <param name="visited">The visited.</param>
        private static void Traverse<T>(T graph, Queue<T> queue, List<T> visited) where T : Graph<T>
        {
            foreach (var node in graph.Nodes)
            {
                if (visited.Contains(node))
                {
                    // Cycle detected in graph.
                    continue;
                }

                queue.Enqueue(node);
            }
        }
    }
}
