// <copyright file="FisherYatesShuffleTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Enums;
    using Malgorithms.Permutation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Malgorithms.UnitTests.FisherYatesShuffleTests
    /// </summary>
    [TestClass]
    public class FisherYatesShuffleTests
    {
        [TestMethod]
        public void FisherYatesShuffle_OriginalPermute_LooksOk()
        {
            int[] source = Enumerable.Range(0, 10).ToArray();
            var fys = new FisherYatesShuffle(x => { x.Variant = Enums.FisherYatesShuffleVariant.Original; });

            fys.Permute(source);
        }

        [TestMethod]
        public void FisherYatesShuffle_ModernPermute_LooksOk()
        {
            int[] source = Enumerable.Range(0, 10).ToArray();
            var fys = new FisherYatesShuffle(x => { x.Variant = Enums.FisherYatesShuffleVariant.Modern; });

            fys.Permute(source);
        }

        [TestMethod]
        [DataRow(FisherYatesShuffleVariant.Modern)]
        [DataRow(FisherYatesShuffleVariant.Original)]
        public void FisherYatesShuffle_PermuteWithRangeOnLeftBoundary_ReturnsExpected(FisherYatesShuffleVariant variant)
        {
            int[] source = Enumerable.Range(0, 10).ToArray();
            var fys = new FisherYatesShuffle(x => { x.Variant = variant; x.Range = (0, 4); });

            fys.Permute(source);

            Assert.AreEqual(source[5], 5);
            Assert.AreEqual(source[6], 6);
            Assert.AreEqual(source[7], 7);
            Assert.AreEqual(source[8], 8);
            Assert.AreEqual(source[9], 9);
        }

        [TestMethod]
        [DataRow(FisherYatesShuffleVariant.Modern)]
        [DataRow(FisherYatesShuffleVariant.Original)]
        public void FisherYatesShuffle_PermuteWithRangeOnRightBoundary_ReturnsExpected(FisherYatesShuffleVariant variant)
        {
            int[] source = Enumerable.Range(0, 10).ToArray();
            var fys = new FisherYatesShuffle(x => { x.Variant = variant; x.Range = (5, 9); });

            fys.Permute(source);

            Assert.AreEqual(source[0], 0);
            Assert.AreEqual(source[1], 1);
            Assert.AreEqual(source[2], 2);
            Assert.AreEqual(source[3], 3);
            Assert.AreEqual(source[4], 4);
        }
    }
}
