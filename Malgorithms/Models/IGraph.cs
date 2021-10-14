﻿// <copyright file="IGraph.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.Models.IGraph
    /// </summary>
    public interface IGraph<T>
    {
        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        /// <value>
        /// The node identifier.
        /// </value>
        public int? NodeId { get; set; }

        /// <summary>
        /// Gets or sets the nodes.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        public LinkedList<T> Nodes { get; }
    }
}
