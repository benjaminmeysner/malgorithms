// <copyright file="FastFourierTransformOptions.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Signal.Options
{
    /// <summary>
    /// Malgorithms.Signal.Options.FastFourierTransformOptions
    /// </summary>
    public class FastFourierTransformOptions
    {
        /// <summary>
        /// Gets or sets the FFT variant.
        /// </summary>
        /// <value>
        /// The FFT variant.
        /// </value>
        public FastFourierTransformVariant Variant { get; set; } = FastFourierTransformVariant.CooleyTukey;
    }
}
