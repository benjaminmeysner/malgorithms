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
        private const double _epsilon = 0.00001;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FastFourierTransform_FftNullInput_Throws() // Currently Throws, should zero pad!
        {
            Complex[] result = new FastFourierTransform().Fft(null);

            CollectionAssert.AreEqual(result, new[] { 0.0 } /* Arbitrary */, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FastFourierTransform_FftWithInputNotPowerOfTwo_OutputSizeIsNextPowerOfTwoUp()
        {
            Complex[] result = new FastFourierTransform().Fft(new[] { 0.75, 0.66, 0.45 });

            Assert.AreEqual(result.Length, 4);
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInput0_MatchesMatlabOutput()
        {
            // 2 ^ 0
            Complex[] expected = new[] { new Complex(0, 0) };

            Complex[] result = new FastFourierTransform().Fft(new[] { 0.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInput1_MatchesMatlabOutput()
        {
            // 2 ^ 2
            Complex[] expected = new[] { new Complex(10, 0), new Complex(-2, 2), new Complex(-2, 0), new Complex(-2, -2) };

            Complex[] result = new FastFourierTransform().Fft(new[] { 1.0, 2.0, 3.0, 4.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInput2_MatchesMatlabOutput()
        {
            // 2 ^ 2
            Complex[] expected = new[] { new Complex(-10, 0), new Complex(2, -2), new Complex(2, 0), new Complex(2, 2) };

            Complex[] result = new FastFourierTransform().Fft(new[] { -1.0, -2.0, -3.0, -4.0 });

            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));
        }

        [TestMethod]
        public void FastFourierTransform_FftWithInputFromFile1_MatchesFftwNETOutput()
        {
            // 2 ^ 7
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
            // 2 ^ 21
            double[] testData = TestHelpers.GenerateRandomDoubles((int)Math.Pow(2, 21), -1000.0000, 1000.0000);
            double[] testDataFftwFormat = TestHelpers.FftDataInFftwFormat(testData);
            Complex[] expected = new Complex[testData.Length];
            Complex[] result = null;
            IPinnedArray<double> fftwin = new PinnedArray<double>(testDataFftwFormat);
            using FftwArrayComplex output = new(DFT.GetComplexBufferSize(fftwin.GetSize()));

            TestHelpers.TimeMethod(() => result = new FastFourierTransform().Fft(testData), out var malgFft);
            TestHelpers.TimeMethod(() => DFT.FFT(fftwin, output), out var fftwFft); // This is a slow call. Beware.

            TestHelpers.FftwArrayComplexToComplex(output, expected);
            CollectionAssert.AreEqual(result, expected, new ComplexEpsilonComparer(_epsilon));

            // TEST RESULTS
            // DELL LAPTOP (UPPER/AVERAGE SPEC)
            // No parallelism on input size = 2 ^ 21 = 1366ms
            // Parallel on input size = 2 ^ 21 = 609ms

            // Ryzen 3900x 3.8GHZ Windows 10 16GB ram
            // No parallelism on input size = 2 ^ 21 = 1042ms
            // Parallel on input size = 2 ^ 21 = 507ms
            // Parallel on input size = (2 ^ 21) + 1 = 850ms ## Not a 2^N input!

            // NOTE * Currently unable to test input lengths != 2^N due to FFTW not zero padding
            // their input sequences and this currently does.
        }
    }
}
