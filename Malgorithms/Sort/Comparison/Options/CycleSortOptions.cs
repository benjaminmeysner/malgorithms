// <copyright file="CycleSortOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison.Options
{
    /// <summary>
    /// Malgorithms.Sort.Comparison.Options.CycleSortOptions
    /// </summary>
    public class CycleSortOptions : IPermutationAlgorithmOptions
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
