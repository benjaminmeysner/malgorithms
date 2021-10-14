// <copyright file="BreadthFirstSearchUnitTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Exceptions;
    using Malgorithms.Graph;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.UnitTests.BreadthFirstSearchUnitTests
    /// </summary>
    [TestClass]
    public class BreadthFirstSearchUnitTests
    {
        /// <summary>
        /// [n1]
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_TraversalOrder0_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var order = new List<int>();

            new BreadthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1 });
        }

        /// <summary>
        /// [n1]
        ///   └─n3
        ///     ├─n6
        ///     ├─n7
        ///       ├─n9
        ///   └─n2
        ///     ├─n4
        ///     ├─n5
        ///       ├─n8
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_TraversalOrder1_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var n2 = new MalgorithmUnitTestObject() { NodeId = 2 };
            var n3 = new MalgorithmUnitTestObject() { NodeId = 3 };
            var n4 = new MalgorithmUnitTestObject() { NodeId = 4 };
            var n5 = new MalgorithmUnitTestObject() { NodeId = 5 };
            var n6 = new MalgorithmUnitTestObject() { NodeId = 6 };
            var n7 = new MalgorithmUnitTestObject() { NodeId = 7 };
            var n8 = new MalgorithmUnitTestObject() { NodeId = 8 };
            var n9 = new MalgorithmUnitTestObject() { NodeId = 9 };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n2.Nodes.AddLast(n4);
            n2.Nodes.AddLast(n5);
            n3.Nodes.AddLast(n6);
            n3.Nodes.AddLast(n7);
            n5.Nodes.AddLast(n8);
            n7.Nodes.AddLast(n9);
            var order = new List<int>();

            new BreadthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }

        /// <summary>
        /// [n1]
        ///   └─n4
        ///     ├─n9
        ///     ├─n10
        ///       ├─n14
        ///   └─n3
        ///     ├─n7
        ///       ├─n12
        ///         ├─n15
        ///     ├─n8
        ///       ├─n13
        ///   └─n2
        ///     ├─n5
        ///     ├─n6
        ///       ├─n11
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_TraversalOrder2_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var n2 = new MalgorithmUnitTestObject() { NodeId = 2 };
            var n3 = new MalgorithmUnitTestObject() { NodeId = 3 };
            var n4 = new MalgorithmUnitTestObject() { NodeId = 4 };
            var n5 = new MalgorithmUnitTestObject() { NodeId = 5 };
            var n6 = new MalgorithmUnitTestObject() { NodeId = 6 };
            var n7 = new MalgorithmUnitTestObject() { NodeId = 7 };
            var n8 = new MalgorithmUnitTestObject() { NodeId = 8 };
            var n9 = new MalgorithmUnitTestObject() { NodeId = 9 };
            var n10 = new MalgorithmUnitTestObject() { NodeId = 10 };
            var n11 = new MalgorithmUnitTestObject() { NodeId = 11 };
            var n12 = new MalgorithmUnitTestObject() { NodeId = 12 };
            var n13 = new MalgorithmUnitTestObject() { NodeId = 13 };
            var n14 = new MalgorithmUnitTestObject() { NodeId = 14 };
            var n15 = new MalgorithmUnitTestObject() { NodeId = 15 };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n1.Nodes.AddLast(n4);
            n2.Nodes.AddLast(n5);
            n2.Nodes.AddLast(n6);
            n6.Nodes.AddLast(n11);
            n3.Nodes.AddLast(n7);
            n3.Nodes.AddLast(n8);
            n8.Nodes.AddLast(n13);
            n7.Nodes.AddLast(n12);
            n13.Nodes.AddLast(n15);
            n4.Nodes.AddLast(n9);
            n4.Nodes.AddLast(n10);
            n10.Nodes.AddLast(n14);
            var order = new List<int>();

            new BreadthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        }

        /// <summary>
        /// [n1]
        ///   └─n3
        ///   └─n2
        ///     ├─n1
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_WithGraphCycle0_Throws()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var n2 = new MalgorithmUnitTestObject() { NodeId = 2 };
            var n3 = new MalgorithmUnitTestObject() { NodeId = 3 };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n2.Nodes.AddLast(n1);
            var order = new List<int>();

            new BreadthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 2, 3 });
        }

        /// <summary>
        /// [n1]
        ///   └─n3
        ///     ├─n1
        ///     ├─n4
        ///   └─n2
        ///     ├─n1
        ///     ├─n3
        ///     ├─n4
        ///       ├─n5
        ///          ├─n2
        ///          ├─n3
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_WithGraphCycle1_Throws()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var n2 = new MalgorithmUnitTestObject() { NodeId = 2 };
            var n3 = new MalgorithmUnitTestObject() { NodeId = 3 };
            var n4 = new MalgorithmUnitTestObject() { NodeId = 4 };
            var n5 = new MalgorithmUnitTestObject() { NodeId = 5 };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n2.Nodes.AddLast(n1);
            n3.Nodes.AddLast(n1);
            n2.Nodes.AddLast(n4);
            n3.Nodes.AddLast(n4);
            n4.Nodes.AddLast(n5);
            n5.Nodes.AddLast(n3);
            n5.Nodes.AddLast(n2);
            n2.Nodes.AddLast(n3);
            var order = new List<int>();

            new BreadthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 2, 3, 4, 5 });
        }

        /// <summary>
        /// [n1]
        /// └─n3
        ///   ├─n6
        ///   ├─n7
        /// └─n2
        ///   ├─n4
        ///   ├─n5
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_SimpleTreeStructureFind_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { Name = "Ben" };
            var n2 = new MalgorithmUnitTestObject() { Name = "John" };
            var n3 = new MalgorithmUnitTestObject() { Name = "Jemma" };
            var n4 = new MalgorithmUnitTestObject() { Name = "Sophie" };
            var n5 = new MalgorithmUnitTestObject() { Name = "Sarah" };
            var n6 = new MalgorithmUnitTestObject() { Name = "Sally" };
            var n7 = new MalgorithmUnitTestObject() { Name = "Serena" };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n2.Nodes.AddLast(n4);
            n2.Nodes.AddLast(n5);
            n3.Nodes.AddLast(n6);
            n3.Nodes.AddLast(n7);

            var result = new BreadthFirstSearch().Find(n1, x => x.Name == "Serena");

            Assert.AreEqual(result, n7);
        }

        /// <summary>
        /// [n1]
        ///   └─n4
        ///     ├─n9
        ///     ├─n10
        ///       ├─n14
        ///   └─n3
        ///     ├─n7
        ///       ├─n12
        ///         ├─n15
        ///     ├─n8
        ///       ├─n13
        ///   └─n2
        ///     ├─n5
        ///     ├─n6
        ///       ├─n11
        /// </summary>
        [TestMethod]
        public void BreadthFirstSearch_ComplexTreeStructureFind_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { Name = "Ben" };
            var n2 = new MalgorithmUnitTestObject() { Name = "Barry" };
            var n3 = new MalgorithmUnitTestObject() { Name = "Glenn" };
            var n4 = new MalgorithmUnitTestObject() { Name = "Joe" };
            var n5 = new MalgorithmUnitTestObject() { Name = "Henry" };
            var n6 = new MalgorithmUnitTestObject() { Name = "Harry" };
            var n7 = new MalgorithmUnitTestObject() { Name = "Hilda" };
            var n8 = new MalgorithmUnitTestObject() { Name = "Robin" };
            var n9 = new MalgorithmUnitTestObject() { Name = "Rupert" };
            var n10 = new MalgorithmUnitTestObject() { Name = "Liam" };
            var n11 = new MalgorithmUnitTestObject() { Name = "Peter" };
            var n12 = new MalgorithmUnitTestObject() { Name = "David" };
            var n13 = new MalgorithmUnitTestObject() { Name = "Michael" };
            var n14 = new MalgorithmUnitTestObject() { Name = "Vivian" };
            var n15 = new MalgorithmUnitTestObject() { Name = "Thomas" };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n1.Nodes.AddLast(n4);
            n2.Nodes.AddLast(n5);
            n2.Nodes.AddLast(n6);
            n6.Nodes.AddLast(n11);
            n3.Nodes.AddLast(n7);
            n3.Nodes.AddLast(n8);
            n8.Nodes.AddLast(n13);
            n7.Nodes.AddLast(n12);
            n13.Nodes.AddLast(n15);
            n4.Nodes.AddLast(n9);
            n4.Nodes.AddLast(n10);
            n10.Nodes.AddLast(n14);
            var order = new List<int>();

            var result = new BreadthFirstSearch().Find(n1, x => x.Name.StartsWith("V"));

            Assert.AreEqual(result, n14);
        }
    }
}
