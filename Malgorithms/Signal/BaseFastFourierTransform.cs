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
        public void SanitiseSignal<T>(T[] signal)
        {
            if (signal is null)
            {
                throw new ArgumentNullException(string.Format(StandardText.ParameterCannotBeNull, nameof(signal)));
            }

            if (!IsPowerOfTwo(signal.Length))
            {
                // May support this in future, extend array with an empty complex number.
                throw new ArgumentException(string.Format(StandardText.SignalSampleCountNotPowerOfTwo, nameof(signal)));
            }
        }

        /// <summary>
        /// Converts an array of <see cref="{Double}" /> to an array of <see cref="{Complex}" />.
        /// Filling all imaginary components to zero.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>
        ///   A new Complex number array.
        /// </returns>
        protected static Complex[] DoubleToComplex(double[] signal)
        {
            Complex[] newSignal = new Complex[signal.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                newSignal[i] = new Complex(signal[i], _empty);
            }
            return newSignal;
        }

        /// <summary>
        /// Determines whether [the specified sample count] [is power of two].
        /// </summary>
        /// <param name="sampleCount">The sample count.</param>
        /// <returns>
        ///   <c>true</c> if [is power of two] [the specified sample count]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsPowerOfTwo(int sampleCount)
        {
            return (sampleCount != 0) && ((sampleCount & (sampleCount - 1)) == 0);
        }
    }
}
