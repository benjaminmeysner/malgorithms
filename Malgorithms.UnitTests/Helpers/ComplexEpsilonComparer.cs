// <copyright file="ComplexEpsilonComparer.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests.Helpers
{
    using System;
    using System.Collections;
    using System.Numerics;

    /// <summary>
    /// Malgorithms.UnitTests.Helpers.EpsilonComparer
    /// </summary>
    /// <seealso cref="System.Collections.IComparer" />
    public class ComplexEpsilonComparer : IComparer
    {
        private readonly double _epsilon;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpsilonComparer"/> class.
        /// </summary>
        /// <param name="epsilon">The epsilon.</param>
        public ComplexEpsilonComparer(double epsilon)
        {
            _epsilon = epsilon;
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />:   - If less than 0, <paramref name="x" /> is less than <paramref name="y" />.   - If 0, <paramref name="x" /> equals <paramref name="y" />.   - If greater than 0, <paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        public int Compare(object x, object y)
        {
            Complex a = (Complex)x;
            Complex b = (Complex)y;
            bool result = (Math.Abs(a.Real - b.Real) <= _epsilon) && (Math.Abs(a.Imaginary - b.Imaginary) <= _epsilon);
            return result ? 0 : 1;
        }
    }
}
