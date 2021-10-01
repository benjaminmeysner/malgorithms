// <copyright file="MergeSortTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Sort.Comparison;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.UnitTests.MergeSortTests
    /// </summary>
    [TestClass]
    public class MergeSortTests
    {
        [TestMethod]
        public void MergeSort_SortTopDown_ReturnsExpected()
        {
            int[] source = Enumerable.Repeat(0, 100).Select(i => new Random().Next(0, 100)).ToArray();
            int[] expected = new int[source.Length];
            var ms = new MergeSort(x => { x.Variant = Enums.MergeSortVariant.TopDown; });
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected);

            ms.Sort(source);

            CollectionAssert.AreEqual(source, expected);
        }

        [TestMethod]
        public void MergeSort_SortTopDownDescending_ReturnsExpected()
        {
            int[] source = Enumerable.Repeat(0, 100).Select(i => new Random().Next(0, 100)).ToArray();
            int[] expected = new int[source.Length];
            var ms = new MergeSort(x => { x.Variant = Enums.MergeSortVariant.TopDown; });
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected);

            ms.Sort(source);

            CollectionAssert.AreEqual(source, expected);
        }
    }
}
