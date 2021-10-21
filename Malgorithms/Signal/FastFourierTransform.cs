// <copyright file="FastFourierTransform.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal
{
    using Malgorithms.Signal.Options;
    using System;
    using System.Numerics;
    using System.Threading.Tasks;

    /// <summary>
    /// Malgorithms.Signal.FastFourierTransform
    /// <para/>TODO: #1 Allow for signal length / sample count not a power of 2 (zero pad the signal).
    /// <para/>TODO: #2 Iterative version.
    /// </summary>
    public class FastFourierTransform : BaseFastFourierTransform, IFastFourierTransform
    {
        private FastFourierTransformOptions _options;

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
            SanitiseSignal(signal);
            Complex[] signalCopy = DoubleToComplex(signal);FftRecursive(signalCopy, signalCopy.Length);
            return signalCopy;
        }

        /// <summary>
        /// <para />Compute the forward FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Recursive Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// <para />https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="sampleCount">The sample count.</param>
        private void FftRecursive(Complex[] signal, int sampleCount)
        {
            if (sampleCount == 1)
            {
                // Use larger base case, use iterative method if less than, say 100?
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

            Parallel.Invoke(() => FftRecursive(even, even.Length), () => FftRecursive(odd, odd.Length));

            double tFactors = -2.0 * Math.PI / sampleCount;

            for (int j = 0; j < halfSampleCount; j++)
            {
                Complex t = Complex.FromPolarCoordinates(1.0, tFactors * j) * odd[j];
                signal[j] = even[j] + t;
                signal[j + halfSampleCount] = even[j] - t;
            }
        }
    }
}