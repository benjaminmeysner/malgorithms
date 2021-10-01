// <copyright file="ISortingAlgorithmOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort
{
    /// <summary>
    /// Malgorithms.Sort.ISortingAlgorithmOptions
    /// </summary>
    public interface ISortingAlgorithmOptions
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
