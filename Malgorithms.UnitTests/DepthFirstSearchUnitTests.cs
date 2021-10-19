// <copyright file="DepthFirstSearchUnitTests.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.UnitTests
{
    using Malgorithms.Graph;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Malgorithms.UnitTests.DepthFirstSearchUnitTests
    /// </summary>
    [TestClass]
    public class DepthFirstSearchUnitTests
    {
        /// <summary>
        /// [n1]
        /// </summary>
        [TestMethod]
        public void DepthFirstSearch_DirectedGraphWithOneTraversalOrder_ReturnsExpectedPath()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var order = new List<int>();

            new DepthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1 });
        }

        /// <summary>
        /// [n1]
        /// </summary>
        [TestMethod]
        public void DepthFirstSearch_DirectedGraphWithOneFindExisting_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { Name = "Ben" };

            var result = new DepthFirstSearch().Find(n1, x => x.Name == "Ben");

            Assert.AreEqual(result, n1);
        }

        /// <summary>
        /// [n1]
        /// </summary>
        [TestMethod]
        public void DepthFirstSearch_DirectedGraphWithOneFindNonExisting_ReturnsNull()
        {
            var n1 = new MalgorithmUnitTestObject() { Name = "Ben" };

            var result = new DepthFirstSearch().Find(n1, x => x.Name == "John");

            Assert.AreEqual(result, null);
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
        public void DepthFirstSearch_DirectedGraphTraversalOrder1_ReturnsExpectedPath()
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

            new DepthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 3, 7, 9, 6, 2, 5, 8, 4 });
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
        ///     ├─n8
        ///       ├─n13
        ///         ├─n15
        ///   └─n2
        ///     ├─n5
        ///     ├─n6
        ///       ├─n11
        /// </summary>
        [TestMethod]
        public void DepthFirstSearch_DirectedGraphTraversalOrder2_ReturnsExpectedPath()
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

            new DepthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 4, 10, 14, 9, 3, 8, 13, 15, 7, 12, 2, 6, 11, 5 });
        }

        /// <summary>
        /// [n1]
        ///   └─n3
        ///   └─n2
        ///     ├─n1
        /// </summary>
        [TestMethod]
        public void DepthFirstSearch_DirectedGraphWithGraphCycle0_ReturnsExpectedPath()
        {
            var n1 = new MalgorithmUnitTestObject() { NodeId = 1 };
            var n2 = new MalgorithmUnitTestObject() { NodeId = 2 };
            var n3 = new MalgorithmUnitTestObject() { NodeId = 3 };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n2.Nodes.AddLast(n1);
            var order = new List<int>();

            new DepthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 3, 2 });
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
        public void DepthFirstSearch_DirectedGraphWithGraphCycle1_ReturnsExpectedPath()
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

            new DepthFirstSearch().Traverse(n1, x => order.Add(x.NodeId.Value));

            CollectionAssert.AreEqual(order, new[] { 1, 3, 4, 5, 2 });
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
        public void DepthFirstSearch_DirectedGraphSimpleTreeStructureFind_ReturnsExpected()
        {
            var n1 = new MalgorithmUnitTestObject() { Name = "Ben", NodeId = 1 };
            var n2 = new MalgorithmUnitTestObject() { Name = "John", NodeId = 2 };
            var n3 = new MalgorithmUnitTestObject() { Name = "Jemma", NodeId = 3 };
            var n4 = new MalgorithmUnitTestObject() { Name = "Sophie", NodeId = 4 };
            var n5 = new MalgorithmUnitTestObject() { Name = "Sarah", NodeId = 5 };
            var n6 = new MalgorithmUnitTestObject() { Name = "Sally", NodeId = 6 };
            var n7 = new MalgorithmUnitTestObject() { Name = "Serena", NodeId = 7 };
            n1.Nodes.AddLast(n2);
            n1.Nodes.AddLast(n3);
            n2.Nodes.AddLast(n4);
            n2.Nodes.AddLast(n5);
            n3.Nodes.AddLast(n6);
            n3.Nodes.AddLast(n7);

            var result = new DepthFirstSearch().Find(n1, x => x.Name == "Serena");

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
        public void DepthFirstSearch_DirectedGraphComplexTreeStructureFind_ReturnsExpected()
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

            var result = new DepthFirstSearch().Find(n1, x => x.Name.StartsWith("V"));

            Assert.AreEqual(result, n14);
        }
    }
}
