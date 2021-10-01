// <copyright file="SmoothSort.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Sort.Comparison
{
    using System;

    /// <summary>
    /// Malgorithms.Sort.Comparison.SmoothSort
    /// </summary>
    /// <seealso cref="Malgorithms.Sort.BaseSortingAlgorithm" />
    /// <seealso cref="Malgorithms.Sort.ISortingAlgorithm" />
    public class SmoothSort : BaseSortingAlgorithm, ISortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothSort"/> class.
        /// </summary>
        public SmoothSort()
        {
        }

        public void Sort<T>(T[] source, Comparison<T> comparison = null)
        {
            throw new NotImplementedException();
        }
    }
}
