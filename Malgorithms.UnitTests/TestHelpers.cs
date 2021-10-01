// <copyright file="TestHelpers.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace Malgorithms.UnitTests
{
    /// <summary>
    /// Malgorithms.UnitTests.TestHelpers
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
    }
}
