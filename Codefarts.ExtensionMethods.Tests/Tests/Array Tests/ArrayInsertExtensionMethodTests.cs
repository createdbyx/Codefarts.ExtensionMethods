// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>

namespace Codefarts.ExtensionMethods.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Array")] 
    public class ArrayInsertExtensionMethodTests
    {
        [TestMethod]
        public void At_Index_Zero()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Insert(0, new[] { 5 });
            Assert.AreEqual(6, items.Length);
            Assert.AreEqual(5, items[0]);
            Assert.AreEqual(0, items[1]);
            Assert.AreEqual(1, items[2]);
            Assert.AreEqual(2, items[3]);
            Assert.AreEqual(3, items[4]);
            Assert.AreEqual(4, items[5]);
        }

        [TestMethod]
        public void One_At_Negative_Index()
        {
            try
            {
                var items = new[] { 0, 1, 2, 3, 4 };
                items = items.Insert(-1, new[] { 5 });
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
                items = items.Insert(100, new[] { 5 });
                Assert.Fail("Expected 'ArgumentOutOfRangeException' to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }
    }
}