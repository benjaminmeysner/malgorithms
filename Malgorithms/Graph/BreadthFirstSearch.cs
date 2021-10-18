// <copyright file="BreadthFirstSearch.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Graph
{
    using System;
    using System.Collections.Generic;
    using Malgorithms.Models;

    /// <summary>
    /// Malgorithms.Graph.BreadthFirstSearch
    /// <para/>TODO: #1 Display shortest node path from root.
    /// <para/>TODO: #2 Support undirected graphs.
    /// <para/>TODO: #3 Modified BFS for weighted graphs (an edge weight of 1 or null for this case).
    /// </summary>
    public class BreadthFirstSearch : BaseGraphAlgorithm, IGraphSearchAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadthFirstSearch" /> class.
        /// </summary>
        public BreadthFirstSearch() { }

        /// <summary>
        /// <para />Breadth-first search (BFS) is an algorithm for searching a graph or tree data structure for a node that satisfies a given property.
        /// https://en.wikipedia.org/wiki/Breadth-first_search.
        /// <para />A BFS Search will be at worst O(|E| + |V|) (The sum of the edges + the sum of the vertices as at worst we will need to visit each node in the graph.
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

            return Traverse(graph, new Queue<T>(), new List<T>(), predicate, nodeAction);
        }

        /// <summary>
        /// Breadth-first traversal (BFS) is an algorithm for traversing a graph data structure.
        /// https://en.wikipedia.org/wiki/Breadth-first_search
        /// <para />A BFS Search will be at worst O(|E| + |V|) (The sum of the edges + the sum of the vertices as at worst we will need to visit each node in the graph.
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
        private static T Traverse<T>(T graph, Queue<T> queue, List<T> visited, Func<T, bool> predicate = null, Action<T> nodeAction = null) where T : DirectedGraph<T>
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
        private static void Traverse<T>(T graph, Queue<T> queue, List<T> visited) where T : DirectedGraph<T>
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
