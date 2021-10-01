// <copyright file="MergeSortOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison.Options
{
    using Malgorithms.Enums;

    /// <summary>
    /// Malgorithms.Sort.Comparison.MergeSortOptions
    /// </summary>
    public class MergeSortOptions : ISortingAlgorithmOptions
    {
        /// <summary>
        /// Gets or sets the merge sort variant.
        /// </summary>
        /// <value>
        /// The merge sort variant.
        /// </value>
        public MergeSortVariant Variant { get; set; } = MergeSortVariant.TopDown;

        /// <summary>
        /// Gets or sets the sort range.
        /// </summary>
        /// <value>
        /// The sort range.
        /// </value>
        public (int Start, int End)? Range { get; set; }
    }
}
