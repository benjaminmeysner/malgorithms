// <copyright file="Graph.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Models.Graph
    /// </summary>
    public class Graph<T> : IGraph<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        public Graph() : this(new LinkedList<T>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public Graph(LinkedList<T> nodes)
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
