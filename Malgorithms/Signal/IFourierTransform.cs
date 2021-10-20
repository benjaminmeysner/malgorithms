// <copyright file="IFourierTransform.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal
{
    using System.Numerics;

    /// <summary>
    /// Malgorithms.Signal.IFourierTransform
    /// </summary>
    public interface IFourierTransform
    {
        /// <summary>
        /// Malgorithms.Signal.IFourierTransform.Transform
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>Array of <see cref="{Complex}"/>, the DFT of <paramref name="signal"/></returns>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>
        public Complex[] Transform(Complex[] signal);

        /// <summary>
        /// Malgorithms.Signal.IFourierTransform.InverseTransform
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>Array of <see cref="{Complex}"/>, the DFT of <paramref name="signal"/></returns>
        /// <remarks>
        /// Comments are made above each class implementation.
        /// </remarks>
        public Complex[] InverseTransform(Complex[] signal);
    }
}
