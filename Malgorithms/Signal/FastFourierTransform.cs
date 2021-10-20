// <copyright file="FastFourierTransform.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal
{
    using Malgorithms.Signal.Options;
    using System;
    using System.Numerics;

    /// <summary>
    /// Malgorithms.Signal.FastFourierTransform
    /// <para/>TODO: #1 Allow for signal length / sample count not a power of 2 (zero pad the signal).
    /// <para/>TODO: #2 Iterative version, and allow for parallelisation.
    /// 
    /// </summary>
    public class FastFourierTransform : BaseFastFourierTransform, IFourierTransform
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
        public Complex[] Transform(double[] signal)
        {
            SanitiseSignal(signal);
            return Transform(DoubleToComplex(signal));
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
        public Complex[] Transform(Complex[] signal)
        {
            SanitiseSignal(signal);
            Complex[] newSignal = new Complex[signal.Length];
            Array.Copy(signal, newSignal, signal.Length);
            Fft(newSignal, newSignal.Length);
            return newSignal;
        }

        /// <summary>
        /// <para />Compute the forward FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Recursive Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// <para />https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="sampleCount">The sample count.</param>
        private void Fft(Complex[] signal, int sampleCount)
        {
            if (sampleCount == 1)
            {
                return;
            }

            Complex[] even = new Complex[sampleCount / 2];
            Complex[] odd = new Complex[sampleCount / 2];

            for (int i = 0; i < sampleCount; i++)
            {
                if (i % 2 == 0)
                {
                    even[i / 2] = signal[i];
                }
                else
                {
                    odd[(i - 1) / 2] = signal[i];
                }
            }

            Fft(even, even.Length);
            Fft(odd, odd.Length);

            for (int j = 0; j < sampleCount / 2; j++)
            {
                Complex t = Complex.FromPolarCoordinates(1.0, -2 * Math.PI * j / sampleCount) * odd[j];
                signal[j] = even[j] + t;
                signal[j + sampleCount / 2] = even[j] - t;
            }
        }
    }
}