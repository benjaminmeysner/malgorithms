// <copyright file="QuickSortOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison.Options
{
    using Malgorithms.Enums;

    /// <summary>
    /// Malgorithms.Sort.Comparison.Options.QuickSortOptions
    /// </summary>
    public class QuickSortOptions : IPermutationAlgorithmOptions
    {
        /// <summary>
        /// Gets or sets the pivot selection.
        /// </summary>
        /// <value>
        /// The pivot selection.
        /// </value>
        public QuickSortPivotSelection PivotSelection { get; set; } = QuickSortPivotSelection.MedianOfThree;

        /// <summary>
        /// Gets or sets the partition scheme.
        /// </summary>
        /// <value>
        /// The scheme.
        /// </value>
        public QuickSortPartitionScheme Scheme { get; set; } = QuickSortPartitionScheme.Hoare;

        /// <summary>
        /// Gets or sets the sort range.
        /// </summary>
        /// <value>
        /// The sort range.
        /// </value>
        public (int Start, int End)? Range { get; set; }
    }
}
