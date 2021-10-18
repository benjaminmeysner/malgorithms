// <copyright file="MalgorithmUnitTestObject.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Models;

    /// <summary>
    /// Malgorithms.UnitTests.MalgorithmUnitTestObject
    /// </summary>
    public class MalgorithmUnitTestObject : DirectedGraph<MalgorithmUnitTestObject>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
