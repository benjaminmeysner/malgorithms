// <copyright file="BaseFastFourierTransform.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal
{
    using Malgorithms.Resources;
    using System;
    using System.Numerics;

    /// <summary>
    /// Malgorithms.Signal.BaseFastFourierTransform
    /// </summary>
    public class BaseFastFourierTransform
    {
        private const double _empty = 0;

        /// <summary>
        /// Sanitises a <paramref name="signal" /> for <see cref="Transform({Complex[]})" />.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ArgumentNullException">Thrown if the signal is null.</exception>
        protected static void TryZeroPadSignal(ref double[] signal)
        {
            Array.Resize(ref signal, NextPowerOfTwo(signal.Length));
        }

        /// <summary>
        /// Converts an array of <see cref="{Double}" /> to an array of <see cref="{Complex}" />.
        /// Filling all imaginary components to zero.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>
        ///   A new Complex number array.
        /// </returns>
        protected static Complex[] Initialise(ref double[] signal)
        {
            if (signal is null)
            {
                throw new ArgumentNullException(StandardText.ParameterCannotBeNull);
            }

            if (!IsPowerOfTwo(signal.Length))
            {
                TryZeroPadSignal(ref signal);
            }

            Complex[] newSignal = new Complex[signal.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                newSignal[i] = new Complex(signal[i], _empty);
            }

            return newSignal;
        }

        /// <summary>
        /// Converts an array of <see cref="{Double}" /> to an array of <see cref="{Complex}" />.
        /// Filling all imaginary components to zero.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>
        ///   A new Complex number array.
        /// </returns>
        protected static Complex[] CreateComplexArray(double[] signal, out Complex[] complexSignal)
        {
            complexSignal = new Complex[signal.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                complexSignal[i] = new Complex(signal[i], _empty);
            }

            return complexSignal;
        }

        /// <summary>
        /// Determines whether [the specified sample count] [is power of two].
        /// </summary>
        /// <param name="sampleCount">The sample count.</param>
        /// <returns>
        ///   <c>true</c> if [is power of two] [the specified sample count]; otherwise, <c>false</c>.
        /// </returns>
        protected static bool IsPowerOfTwo(int sampleCount)
        {
            return (sampleCount != 0) && ((sampleCount & (sampleCount - 1)) == 0);
        }

        /// <summary>
        /// Returns the next power of two up from a number.
        /// </summary>
        /// <param name="number">A number.</param>
        /// <returns>Next power of 2</returns>
        protected static int NextPowerOfTwo(int number)
        {
            number--;
            number |= number >> 1;
            number |= number >> 2;
            number |= number >> 4;
            number |= number >> 8;
            number |= number >> 16;
            return ++number;
        }
    }
}
