// <copyright file="ISearchAlgorithmOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Search
{
    /// <summary>
    /// Malgorithms.Sort.ISearchAlgorithmOptions
    /// </summary>
    public interface ISearchAlgorithmOptions
    {
        /// <summary>
        /// Gets or sets the sort range.
        /// </summary>
        /// <value>
        /// The sort range.
        /// </value>
        public (int Start, int End)? Range { get; set; }
    }
}
