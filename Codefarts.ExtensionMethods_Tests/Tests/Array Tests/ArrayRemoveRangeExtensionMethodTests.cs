// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>

namespace Codefarts.Tests.ExtensionMethods
{
    using System;
    using System.ComponentModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Array")]
    public class ArrayRemoveRangeExtensionMethodTests
    {
        [TestMethod]
        public void From_Start()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.RemoveRange(0, 2);
            Assert.AreEqual(3, items.Length);
            Assert.AreEqual(2, items[0]);
            Assert.AreEqual(3, items[1]);
            Assert.AreEqual(4, items[2]);
        }

        [TestMethod]
        public void From_End()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.RemoveRange(3, 2);
            Assert.AreEqual(3, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
        }

        [TestMethod]
        public void From_Middle()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.RemoveRange(1, 3);
            Assert.AreEqual(2, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(4, items[1]);
        }

        [TestMethod]
        public void With_Negative_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.RemoveRange(1, -3);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void With_Oversized_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.RemoveRange(2, 200);
            Assert.AreEqual(2, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
        }

        [TestMethod]
        public void With_Negative_Index()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            try
            {
                items = items.RemoveRange(-2, 2);
                Assert.Fail("Expected ArgumentOutOfRangeException exception.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }
    }
}