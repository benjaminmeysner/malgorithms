// <copyright file="QuickSortTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Enums;
    using Malgorithms.Sort.Comparison;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.UnitTests.QuickSortTests
    /// </summary>
    [TestClass]
    public class QuickSortTests
    {
        [TestMethod]
        public void QuickSort_SortLomutoSchemeDescending_ReturnsExpected()
        {
            int[] source = Enumerable.Repeat(0, 10).Select(i => new Random().Next(0, 10)).ToArray();
            int[] expected = new int[source.Length];
            // Use 'median-of-three' as the pivot selection. Arbritrary.
            var qs = new QuickSort(x => { x.Scheme = QuickSortPartitionScheme.Lomuto; });
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

            qs.Sort(source, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

            CollectionAssert.AreEqual(source, expected);
        }

        [TestMethod]
        public void QuickSort_SortHoareSchemeDescending_ReturnsExpected()
        {
            int[] source = Enumerable.Repeat(0, 10).Select(i => new Random().Next(0, 10)).ToArray();
            int[] expected = new int[source.Length];
            // Use 'median-of-three' as the pivot selection. Arbritrary.
            var qs = new QuickSort(x => { x.Scheme = QuickSortPartitionScheme.Hoare; });
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

            qs.Sort(source, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

            CollectionAssert.AreEqual(source, expected);
        }

        [TestMethod]
        [DataRow(QuickSortPivotSelection.Last)]
        [DataRow(QuickSortPivotSelection.MedianOfThree)]
        [DataRow(QuickSortPivotSelection.Randomised)]
        public void QuickSort_SortOptionsLomutoSchemeVaryingPivotSelectors_ReturnsExpected(QuickSortPivotSelection pivot)
        {
            int[] source = Enumerable.Repeat(0, 100).Select(i => new Random().Next(0, 100)).ToArray();
            int[] expected = new int[source.Length];
            var qs = new QuickSort(x => { x.Scheme = QuickSortPartitionScheme.Lomuto; x.PivotSelection = pivot; });
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected);

            qs.Sort(source);

            CollectionAssert.AreEqual(source, expected);
        }

        [TestMethod]
        [DataRow(QuickSortPivotSelection.Last)]
        [DataRow(QuickSortPivotSelection.MedianOfThree)]
        [DataRow(QuickSortPivotSelection.Randomised)]
        public void QuickSort_SortOptionsHoareSchemeVaryingPivotSelectors_ReturnsExpected(QuickSortPivotSelection pivot)
        {
            int[] source = Enumerable.Repeat(0, 100).Select(i => new Random().Next(0, 100)).ToArray();
            int[] expected = new int[source.Length];
            var qs = new QuickSort(x => { x.Scheme = QuickSortPartitionScheme.Hoare; x.PivotSelection = pivot; });
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected);

            qs.Sort(source);

            CollectionAssert.AreEqual(source, expected);
        }

        [TestMethod]
        [DataRow(QuickSortPartitionScheme.Hoare, true)]
        [DataRow(QuickSortPartitionScheme.Hoare, false)]
        [DataRow(QuickSortPartitionScheme.Lomuto, true)]
        [DataRow(QuickSortPartitionScheme.Lomuto, false)]
        public void QuickSort_SortComplexObjectCustomComparisonId_ReturnsExpected(QuickSortPartitionScheme scheme, bool ascending)
        {
            MalgorithmUnitTestObject[] objects = TestHelpers.GenerateObjectsWithRandomValues(100);
            MalgorithmUnitTestObject[] expected = new MalgorithmUnitTestObject[objects.Length];
            var qs = new QuickSort(x => { x.Scheme = scheme; x.PivotSelection = QuickSortPivotSelection.MedianOfThree; });
            Array.Copy(objects, expected, objects.Length);
            Array.Sort(expected, (x, y) => ascending ? x.Id.CompareTo(y.Id) : y.Id.CompareTo(x.Id));

            qs.Sort(objects, (x, y) => ascending ? x.Id.CompareTo(y.Id) : y.Id.CompareTo(x.Id));

            CollectionAssert.AreEqual(objects, expected);
        }

        [TestMethod]
        [DataRow(QuickSortPartitionScheme.Hoare, true)]
        [DataRow(QuickSortPartitionScheme.Hoare, false)]
        [DataRow(QuickSortPartitionScheme.Lomuto, true)]
        [DataRow(QuickSortPartitionScheme.Lomuto, false)]
        public void QuickSort_SortComplexObjectCustomComparisonName_ReturnsExpected(QuickSortPartitionScheme scheme, bool ascending)
        {
            MalgorithmUnitTestObject[] objects = TestHelpers.GenerateObjectsWithRandomValues(100);
            MalgorithmUnitTestObject[] expected = new MalgorithmUnitTestObject[objects.Length];
            var qs = new QuickSort(x => { x.Scheme = scheme; x.PivotSelection = QuickSortPivotSelection.MedianOfThree; });
            Array.Copy(objects, expected, objects.Length);
            Array.Sort(expected, (x, y) => ascending ? x.Name.CompareTo(y.Name) : y.Name.CompareTo(x.Name));

            qs.Sort(objects, (x, y) => ascending ? x.Name.CompareTo(y.Name) : y.Name.CompareTo(x.Name));

            CollectionAssert.AreEqual(objects, expected);
        }
    }
}
