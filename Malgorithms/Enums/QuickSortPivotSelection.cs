// <copyright file="PivotSelection.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Enums
{
    /// <summary>
    /// Malgorithms.Enums.PivotSelection
    /// </summary>
    public enum QuickSortPivotSelection
    {
        /// <summary>
        /// The first value is taken as the pivot.
        /// </summary>
        First,

        /// <summary>
        /// The last value is taken as the pivot.
        /// </summary>
        Last,

        /// <summary>
        /// A median of the first, middle and last value is taken as the pivot.
        /// </summary>
        MedianOfThree,

        /// <summary>
        /// A randomised indexed value is taken as the pivot.
        /// </summary>
        Randomised
    }
}
