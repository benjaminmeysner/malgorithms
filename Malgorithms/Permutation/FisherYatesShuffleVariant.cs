// <copyright file="FisherYatesShuffleVariant.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Permutation
{
    /// <summary>
    /// Malgorithms.Enums.FisherYatesShuffleVariant
    /// </summary>
    public enum FisherYatesShuffleVariant
    {
        /// <summary>
        /// Original Fisher Yates / Knuth Shuffle.
        /// </summary>
        Original,

        /// <summary>
        /// The modern algorithm. Author: Richard Durstenfeld.
        /// </summary>
        Modern,

        /// <summary>
        /// Sattolo' algorithm.
        /// </summary>
        Sattolo,
    }
}
