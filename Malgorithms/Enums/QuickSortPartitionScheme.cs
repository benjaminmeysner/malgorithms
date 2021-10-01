// <copyright file="QuickSortPartitionScheme.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Enums
{
    /// <summary>
    /// Malgorithms.Enums.QuickSortPartitionScheme
    /// </summary>
    public enum QuickSortPartitionScheme
    {
        /// <summary>
        /// Hoare (1959) partition scheme.
        /// https://en.wikipedia.org/wiki/Quicksort#Hoare_partition_scheme
        /// </summary>
        Hoare,

        /// <summary>
        /// This scheme is attributed to Nico Lomuto and popularized by Bentley in his book Programming Pearls[15] and Cormen et al. in their book Introduction to Algorithms.
        /// https://en.wikipedia.org/wiki/Quicksort#Lomuto_partition_scheme
        /// </summary>
        Lomuto
    }
}
