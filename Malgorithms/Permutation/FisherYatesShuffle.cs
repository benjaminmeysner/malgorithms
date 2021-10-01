// <copyright file="FisherYatesShuffle.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Permutation
{
    using Malgorithms.Helpers;
    using Malgorithms.Permutation.Options;
    using Malgorithms.Resources;
    using System;
    using System.Linq;

    /// <summary>
    /// Malgorithms.Permutation.FisherYatesShuffle
    /// </summary>
    /// <seealso cref="Malgorithms.Permutation.BasePermutationAlgorithm" />
    /// <seealso cref="Malgorithms.Permutation.IPermutationAlgorithm" />
    /// <remarks>
    /// The Fisher–Yates shuffle is named after Ronald Fisher and Frank Yates, who first described it, and is also known as the Knuth shuffle after Donald Knuth.
    /// </remarks>
    public class FisherYatesShuffle : BasePermutationAlgorithm, IPermutationAlgorithm
    {
        private FisherYatesShuffleOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="FisherYatesShuffle"/> class.
        /// </summary>
        /// <param name="options">The <see cref="FisherYatesShuffle"/> options.</param>
        /// <remarks>
        /// By default, the configuration is the original Fisher Yates shuffle method.
        /// </remarks>
        public FisherYatesShuffle(Action<FisherYatesShuffleOptions> options = null)
        {
            _options = new FisherYatesShuffleOptions();
            if (!(options is null))
            {
                options(_options);
            }
        }

        /// <summary>
        /// <para />Shuffle an <see cref="{T}[]" /><see cref="Array" /> using Fisher Yates Shuffle and the configured <see cref="FisherYatesShuffleOptions" />.
        /// <para />The Fisher–Yates shuffle is an algorithm for generating a random permutation of a finite sequence—in plain terms, the algorithm shuffles the sequence.
        /// Also known as the 'Knuth Shuffle'. The default variant used is the original as described in the wikipedia listing.
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source" /> is null.</exception>
        /// <remarks>
        ///   <para />https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Fisher_and_Yates
        /// </remarks>
        public void Permute<T>(T[] source)
        {
            PermuteValidation(source, _options);
            PermuteVariant(source, _options?.Range?.Start ?? 0, _options?.Range?.End ?? source.Length - 1);
        }

        /// <summary>
        /// <para />Fisher Yates Modern implementation.
        /// <para />https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        private static void Modern<T>(T[] source, int low, int high)
        {
            for (int i = high; i > low; i--)
            {
                int j = new Random().Next(low, i);
                THelpers.Swap(ref source[j], ref source[i]);
            }
        }

        /// <summary>
        /// <para />Fisher Yates Original implementation.
        /// <para />https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Fisher_and_Yates'_original_method
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        private static void Original<T>(T[] source, int low, int high)
        {
            T[] copy = new T[source.Length];
            Array.Copy(source, copy, source.Length);
            int[] visited = new int[source.Length];

            for (int i = high; i > low; i--)
            {
                int j;
                do
                {
                    j = new Random().Next(low, high + 1);
                }
                while (visited.Contains(j));

                source[i] = copy[j];
                visited[i] = j;
            }
        }

        /// <summary>
        /// <para />Directs the merge sort to the configured variant.
        /// <para />https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Variants
        /// <para />https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#Modern_method
        /// </summary>
        /// <typeparam name="T">The <paramref name="source" /> type.</typeparam>
        /// <param name="source">The input source.</param>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void PermuteVariant<T>(T[] source, int low, int high)
        {
            if (!(_options is null))
            {
                switch (_options.Variant)
                {
                    case Enums.FisherYatesShuffleVariant.Modern:
                        Modern(source, low, high);
                        break;
                    case Enums.FisherYatesShuffleVariant.Original:
                        Original(source, low, high);
                        break;
                    case Enums.FisherYatesShuffleVariant.Sattolo:
                        throw new NotImplementedException(StandardText.AlgorithmNotImplemented);
                }
            }
        }
    }
}
