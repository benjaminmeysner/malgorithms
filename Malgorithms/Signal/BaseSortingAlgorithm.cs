// <copyright file="BaseFastFourierTransform.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal
{
    using Malgorithms.Resources;
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Malgorithms.Signal.BaseFastFourierTransform
    /// </summary>
    public class BaseFastFourierTransform
    {
        /// <summary>
        /// Sanitises a <paramref name="signal" /> for <see cref="Transform({Complex[]})" />.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ArgumentNullException">Thrown if the signal is null.</exception>
        public void SanitiseSignal(Complex[] signal)
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
