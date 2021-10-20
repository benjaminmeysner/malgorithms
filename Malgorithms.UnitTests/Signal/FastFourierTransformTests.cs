// <copyright file="FastFourierTransformTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests.Signal
{
    using FFTW.NET;
    using Malgorithms.Signal;
    using Malgorithms.UnitTests.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Numerics;

    /// <summary>
    /// Malgorithms.UnitTests.Signal.FastFourierTransformTests
    /// <para/>https://www.dsprelated.com/showthread/comp.dsp/71595-1.php
    /// </summary>
    [TestClass]
    public class FastFourierTransformTests
    {
        private const double _epsilon = 0.00000001;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FastFourierTransform_NullInput_Throws() // Currently Throws, should zero pad!
        {
            Complex[] result = new FastFourierTransform().Transform((double[])null);

            CollectionAssert.AreEqual(result, new[] { 0.0 } /* Arbitrary */, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FastFourierTransform_WithInputNotPowerOfTwo_Throws() // Currently Throws, should zero pad!
        {
            Complex[] result = new FastFourierTransform().Transform(new[] { 0.0, 0.0, 0.0 });

            CollectionAssert.AreEqual(result, new[] { 0.0 } /* Arbitrary */, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_WithInput0_MatchesMatlabOutput()
        {
            Complex[] expected = new[] { new Complex(0, 0) };

            Complex[] result = new FastFourierTransform().Transform(new[] { 0.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_WithInput1_MatchesMatlabOutput()
        {
            Complex[] expected = new[] { new Complex(10, 0), new Complex(-2, 2), new Complex(-2, 0), new Complex(-2, -2) };

            Complex[] result = new FastFourierTransform().Transform(new[] { 1.0, 2.0, 3.0, 4.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_WithInput2_MatchesMatlabOutput()
        {
            Complex[] expected = new[] { new Complex(-10, 0), new Complex(2, -2), new Complex(2, 0), new Complex(2, 2) };

            Complex[] result = new FastFourierTransform().Transform(new[] { -1.0, -2.0, -3.0, -4.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_WithInputFromFile1_MatchesFftwNETOutput()
        {
            double[] testData = TestHelpers.FftDataFromFile(@"..\..\..\TestData\fft_testdata.csv");
            double[] testDataFftwFormat = TestHelpers.FftDataInFftwFormat(testData);
            Complex[] expected = new Complex[testData.Length];
            IPinnedArray<double> fftwin = new PinnedArray<double>(testDataFftwFormat);
            using (FftwArrayComplex output = new(DFT.GetComplexBufferSize(fftwin.GetSize())))
            {
                // Fftw.NET is a wrapper around the Fftw algorithm and can be considered reliable
                DFT.FFT(fftwin, output);
                TestHelpers.FftwArrayComplexToComplex(output, expected);
            }

            Complex[] result = new FastFourierTransform().Transform(testData);

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }
    }
}
