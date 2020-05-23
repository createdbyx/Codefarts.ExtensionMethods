// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>

namespace Codefarts.Tests.ExtensionMethods
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Array")]
    public class ArrayReplaceExtensionMethodTests
    {
        [TestMethod]
        public void At_Index_Zero()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Replace(0, new[] { 5 });
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(5, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void Middle()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Replace(1, new[] { 9, 9 ,9 });
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(9, items[1]);
            Assert.AreEqual(9, items[2]);
            Assert.AreEqual(9, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void At_End_Expect_Expanded_Array()
        {   
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Replace(4, new[] { 4, 5, 6, 7, 8, 9 });
            Assert.AreEqual(10, items.Length);
            for (var i = 0; i < items.Length; i++)
            {
                Assert.AreEqual(i, items[i]);
            }
        }

        [TestMethod]
        public void One_At_Negative_Index()
        {
            try
            {
                var items = new[] { 0, 1, 2, 3, 4 };
                items = items.Replace(-1, new[] { 5 });
                Assert.Fail("Expected 'ArgumentOutOfRangeException' to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void One_Beyond_End_Of_Array()
        {
            try
            {
                var items = new[] { 0, 1, 2, 3, 4 };
                items = items.Replace(100, new[] { 5 });
                Assert.Fail("Expected 'ArgumentOutOfRangeException' to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }
    }
}