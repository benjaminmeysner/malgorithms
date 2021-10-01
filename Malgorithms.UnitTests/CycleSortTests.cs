// <copyright file="CycleSortTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Sort.Comparison;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.UnitTests.CycleSortTests
    /// </summary>
    [TestClass]
    public class CycleSortTests
    {
        [TestMethod]
        public void CycleSort_Sort_ReturnsExpected()
        {
            int[] source = Enumerable.Repeat(0, 100).Select(i => new Random().Next(0, 100)).ToArray();
            int[] expected = new int[source.Length];
            Array.Copy(source, expected, source.Length);
            Array.Sort(expected);

            new CycleSort().Sort(source);

            CollectionAssert.AreEqual(source, expected);
        }
    }
}
