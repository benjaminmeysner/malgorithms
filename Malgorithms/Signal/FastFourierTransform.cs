// <copyright file="FastFourierTransform.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal
{
    using Malgorithms.Helpers;
    using Malgorithms.Signal.Options;
    using System;
    using System.Diagnostics.Contracts;
    using System.Numerics;
    using System.Threading.Tasks;

    /// <summary>
    /// Malgorithms.Signal.FastFourierTransform
    /// <para/>TODO: #1 Allow for signal length / sample count not a power of 2 
    /// This could be attained by not using radix2 DIT but check signal length and use either radix3,radix5 etc to
    /// support it.
    /// </summary>
    public class FastFourierTransform : BaseFastFourierTransform, IFastFourierTransform
    {
        // Precompute 2π in the formula 2πik/N.
        private const double ProductOfMinusTwoPi = -6.28318530718;
        private readonly FastFourierTransformOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="FastFourierTransform"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public FastFourierTransform(Action<FastFourierTransformOptions> options = null)
        {
            _options = new FastFourierTransformOptions();
            if (!(options is null))
            {
                options(_options);
            }
        }

        /// <summary>
        /// <para />Compute the forward FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// <para />https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>
        /// Array of <see cref="{Complex}" />, the DFT of <paramref name="signal" />
        /// </returns>
        public Complex[] Fft(double[] signal)
        {
            Contract.Requires(signal != null);

            SanitiseSignal(signal);
            Complex[] signalCopy = DoubleToComplex(signal);
            FftRecursive(signalCopy, signalCopy.Length);

            Contract.EndContractBlock();
            return signalCopy;
        }

        /// <summary>
        /// <para />Compute the forward FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Recursive Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// <para />https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="sampleCount">The sample count.</param>
        private static void FftRecursive(Complex[] signal, int sampleCount)
        {
            // 2 ^ 7
            if (sampleCount <= 128)
            {
                FftIterative(signal, sampleCount);
                return;
            }

            int halfSampleCount = sampleCount >> 1;
            Complex[] even = new Complex[halfSampleCount];
            Complex[] odd = new Complex[halfSampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                if ((i & 1) == 0)
                {
                    even[i >> 1] = signal[i];
                }
                else
                {
                    odd[(i - 1) >> 1] = signal[i];
                }
            }

            if (sampleCount >= 2048) // 2 ^ 11
            {
                Parallel.Invoke(() => FftRecursive(even, even.Length), () => FftRecursive(odd, odd.Length));
            }
            else
            {
                FftRecursive(even, even.Length);
                FftRecursive(odd, odd.Length);
            }

            double tFactors = ProductOfMinusTwoPi / sampleCount;

            for (int j = 0; j < halfSampleCount; j++)
            {
                Complex t = Complex.FromPolarCoordinates(1.0, tFactors * j) * odd[j];
                signal[j] = even[j] + t;
                signal[j + halfSampleCount] = even[j] - t;
            }
        }

        /// <summary>
        /// <para />Compute the forward FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Iterative Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// <para />https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="sampleCount">The sample count.</param>
        private static void FftIterative(Complex[] signal, int sampleCount)
        {
            BitReverse(signal, sampleCount);
            for (int i = 2; i <= sampleCount; i <<= 1)
            {
                double tFactors = ProductOfMinusTwoPi / i;
                int halfCurrentSample = i >> 1;

                for (int j = 0; j < sampleCount; j += i)
                {
                    for (int k = 0; k < halfCurrentSample; k++)
                    {
                        Complex t = Complex.FromPolarCoordinates(1.0, tFactors * k) * signal[j + k + halfCurrentSample];
                        Complex even = signal[j + k];

                        signal[j + k] = even + t;
                        signal[j + k + halfCurrentSample] = even - t;
                    }
                }
            }
        }

        /// <summary>
        /// Performs a Bit Reversal Algorithm on the signal.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="bits">The bits.</param>
        /// <returns>Reversed bits</returns>
        private static void BitReverse(Complex[] signal, int sampleCount)
        {
            int bits = (int)Math.Log(sampleCount, 2);
            int bitsMinusOne = bits - 1;

            for (int i = 1; i < sampleCount; i++)
            {
                int reversedN = i;
                int j = i >> 1;
                int count = bitsMinusOne;

                while (j > 0)
                {
                    reversedN = (reversedN << 1) | (j & 1);
                    count--;
                    j >>= 1;
                }

                int k = (reversedN << count) & ((1 << bits) - 1);
                if (k <= i)
                {
                    continue;
                }

                THelpers.Swap(ref signal[i], ref signal[k]);
            }
        }
    }
}