// <copyright file="FisherYatesShuffleOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Permutation.Options
{
    /// <summary>
    /// Malgorithms.Permutation.Options.FisherYatesShuffleOptions
    /// </summary>
    public class FisherYatesShuffleOptions : IPermutationAlgorithmOptions
    {
        /// <summary>
        /// Gets or sets the sort range.
        /// </summary>
        /// <value>
        /// The sort range.
        /// </value>
        public (int Start, int End)? Range { get; set; }

        /// <summary>
        /// Gets or sets the shuffle variant.
        /// </summary>
        /// <value>
        /// The variant.
        /// </value>
        public FisherYatesShuffleVariant Variant { get; set; } = FisherYatesShuffleVariant.Original;
    }
}
