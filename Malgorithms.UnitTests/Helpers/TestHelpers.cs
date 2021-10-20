// <copyright file="TestHelpers.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests.Helpers
{
    using FFTW.NET;
    using Microsoft.VisualBasic.FileIO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Malgorithms.UnitTests.Helpers.TestHelpers
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Generates the objects with random values.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static MalgorithmUnitTestObject[] GenerateObjectsWithRandomValues(int amount)
        {
            MalgorithmUnitTestObject[] objects = new MalgorithmUnitTestObject[amount];
            for (int i = 0; i < amount; i++)
            {
                objects[i] = new MalgorithmUnitTestObject { Id = new Random().Next(int.MinValue, int.MaxValue), Name = RandomString(6) };
            }
            return objects;
        }

        /// <summary>
        /// Generates a 'random' (test-worthy) string of alpha numeric characters.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Gets the Fft data from file at the designated path.
        /// </summary>
        /// <returns></returns>
        public static double[] FftDataFromFile(string filePath)
        {
            List<double> data = new();
            using (TextFieldParser parser = new(@$"{filePath}"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        data.Add(double.Parse(field));
                    }
                }
            }

            return data.ToArray();
        }

        /// <summary>
        /// Copies a FftwArrayComplex to .NET complex array.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <returns></returns>
        public static void FftwArrayComplexToComplex(FftwArrayComplex input, Complex[] output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[i];
            }
        }

        /// <summary>
        /// Sets the data in FFTW format. It seems like Fftw look for the input in an already
        /// expanded real number array with a zero imaginary part.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static double[] FftDataInFftwFormat(double[] input)
        {
            double[] output = new double[input.Length * 2];
            for (int i = 0; i < input.Length; i++)
            {
                output[i * 2] = input[i];
                output[i * 2 + 1] = 0;
            }

            return output;
        }
    }
}
