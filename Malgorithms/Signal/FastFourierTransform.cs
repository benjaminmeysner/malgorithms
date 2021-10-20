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
        public Complex[] Transform(Complex[] signal)
        {
            SanitiseSignal(signal);
            Transform(signal, signal.Length);
            return null;
        }

        /// <summary>
        /// <para />Compute the forward FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// <para />https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="sampleCount">The sample count.</param>
        private Complex[] Transform(Complex[] signal, int sampleCount)
        {
            //BitReverse(signal);
            return null;
        }

        /// <summary>
        /// <para />Compute the inverse FFT of a <see cref="Complex" /> typed <paramref name="signal" />.
        /// <para />Cooley-Tukey algorithm is used if no such configuration is supplied.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>
        /// Array of <see cref="{Complex}" />, the DFT of <paramref name="signal" />
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Complex[] InverseTransform(Complex[] signal)
        {
            throw new NotImplementedException();
        }
    }
}