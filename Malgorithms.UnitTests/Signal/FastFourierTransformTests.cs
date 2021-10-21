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
        public void FastFourierTransform_FftNullInput_Throws() // Currently Throws, should zero pad!
        {
            Complex[] result = new FastFourierTransform().Fft((double[])null);

            CollectionAssert.AreEqual(result, new[] { 0.0 } /* Arbitrary */, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FastFourierTransform_FftWithInputNotPowerOfTwo_Throws() // Currently Throws, should zero pad!
        {
            Complex[] result = new FastFourierTransform().Fft(new[] { 0.0, 0.0, 0.0 });

            CollectionAssert.AreEqual(result, new[] { 0.0 } /* Arbitrary */, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInput0_MatchesMatlabOutput()
        {
            Complex[] expected = new[] { new Complex(0, 0) };

            Complex[] result = new FastFourierTransform().Fft(new[] { 0.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInput1_MatchesMatlabOutput()
        {
            Complex[] expected = new[] { new Complex(10, 0), new Complex(-2, 2), new Complex(-2, 0), new Complex(-2, -2) };

            Complex[] result = new FastFourierTransform().Fft(new[] { 1.0, 2.0, 3.0, 4.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInput2_MatchesMatlabOutput()
        {
            Complex[] expected = new[] { new Complex(-10, 0), new Complex(2, -2), new Complex(2, 0), new Complex(2, 2) };

            Complex[] result = new FastFourierTransform().Fft(new[] { -1.0, -2.0, -3.0, -4.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInputFromFile1_MatchesFftwNETOutput()
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

            Complex[] result = new FastFourierTransform().Fft(testData);

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInputExaminingTimeProfile_ExceptableTimeElapsed()
        {
            double[] testData = TestHelpers.GenerateRandomDoubles(131072, -1000.0000, 1000.0000);
            double[] testDataFftwFormat = TestHelpers.FftDataInFftwFormat(testData);
            Complex[] expected = new Complex[testData.Length];
            Complex[] result = null;
            IPinnedArray<double> fftwin = new PinnedArray<double>(testDataFftwFormat);
            using FftwArrayComplex output = new(DFT.GetComplexBufferSize(fftwin.GetSize()));

            TestHelpers.TimeMethod(() => result = new FastFourierTransform().Fft(testData), out var malgFft);
            TestHelpers.TimeMethod(() => DFT.FFT(fftwin, output), out var fftwFft);

            TestHelpers.FftwArrayComplexToComplex(output, expected);
            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }
    }
}
