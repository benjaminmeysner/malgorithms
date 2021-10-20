// <copyright file="BinarySearchTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests.Search
{
    using Malgorithms.Search;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Malgorithms.UnitTests.Search.BinarySearchTests
    /// </summary>
    [TestClass]
    public class BinarySearchTests
    {
        [TestMethod]
        [DataRow(10, 10)]
        [DataRow(0, 0)]
        [DataRow(20, -1)]
        [DataRow(999, -1)]
        public void BinarySearch_OriginalSearch_ReturnsExpected(int search, int expected)
        {
            int[] source = Enumerable.Range(0, 20).ToArray();

            int i = new BinarySearch().IndexOf(source, search);

            Assert.IsTrue(i == expected);
        }

        [TestMethod]
        [DataRow(10, 0)]
        [DataRow(0, -1)]
        [DataRow(999, -1)]
        public void BinarySearch_ExponentialSearch_ReturnsExpected(int search, int expected)
        {
            int[] source = Enumerable.Range(10, 50).ToArray();

            int i = new BinarySearch(x => { x.Variant = BinarySearchVariant.Exponential; }).IndexOf(source, search);

            Assert.IsTrue(i == expected);
        }

        [TestMethod]
        [DataRow(9, -1)]
        [DataRow(10, 10)]
        [DataRow(30, 30)]
        [DataRow(31, -1)]
        public void BinarySearch_OriginalSearchRange_ReturnsExpected(int search, int expected)
        {
            int[] source = Enumerable.Range(0, 50).ToArray();

            int i = new BinarySearch(x => { x.Range = (10, 30); }).IndexOf(source, search);

            Assert.IsTrue(i == expected);
        }

        [TestMethod]
        [DataRow(9, -1)]
        [DataRow(10, 10)]
        [DataRow(30, 30)]
        [DataRow(31, -1)]
        public void BinarySearch_ExponentialSearchRange_ReturnsExpected(int search, int expected)
        {
            int[] source = Enumerable.Range(0, 50).ToArray();

            int i = new BinarySearch(x => { x.Range = (10, 30); x.Variant = BinarySearchVariant.Exponential; }).IndexOf(source, search);

            Assert.IsTrue(i == expected);
        }
    }
}
