// <copyright file="BinarySearchOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Search.Options
{
    using Malgorithms.Enums;

    /// <summary>
    /// Malgorithms.Search.Options.BinarySortOption
    /// </summary>
    public class BinarySearchOptions : ISearchAlgorithmOptions
    {
        /// <summary>
        /// Gets or sets the sort range.
        /// </summary>
        /// <value>
        /// The sort range.
        /// </value>
        public (int Start, int End)? Range { get; set; }

        /// <summary>
        /// Binary search variant.
        /// </summary>
        public BinarySearchVariant Variant { get; set; }
    }
}
