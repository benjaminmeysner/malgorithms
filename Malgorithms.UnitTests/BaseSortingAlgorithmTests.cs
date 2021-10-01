// <copyright file="BaseSortingAlgorithmTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Enums;
    using Malgorithms.Sort;
    using Malgorithms.Sort.Comparison;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.UnitTests.BaseSortingAlgorithmTests
    /// </summary>
    [TestClass]
    public class BaseSortingAlgorithmTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SortValidation_SourceNull_Throws()
        {
            var bsa = new BaseSortingAlgorithm();

            bsa.SortValidation<int>(null, new QuickSortOptions { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(0, 3)]
        [DataRow(-1, 2)]
        public void SortValidation_OptionsInvalidRange_Throws(int startRange, int endRange)
        {
            int[] source = new[] { 3, 2, 1 };
            var bsa = new BaseSortingAlgorithm();

            bsa.SortValidation(source, new QuickSortOptions { Range = (startRange, endRange) });
        }
    }
}
