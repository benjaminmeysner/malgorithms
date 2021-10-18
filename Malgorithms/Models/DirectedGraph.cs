// <copyright file="DirectedGraph.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Models.DirectedGraph
    /// </summary>
    public class DirectedGraph<T> : IDirectedGraph<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedGraph{T}"/> class.
        /// </summary>
        public DirectedGraph() : this(new LinkedList<T>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedGraph{T}"/> class.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public DirectedGraph(LinkedList<T> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Gets or sets the nodes.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public LinkedList<T> Nodes { get; private set; }

        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        /// <value>
        /// The node identifier.
        /// </value>
        public int? NodeId { get; set; }
    }
}
