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
    public class ArrayCropExtensionMethodTests
    {
        [TestMethod]
        public void From_End()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(3);
            Assert.AreEqual(3, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
        }

        [TestMethod]
        public void From_End_With_Index()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(3, 2);
            Assert.AreEqual(2, items.Length);
            Assert.AreEqual(3, items[0]);
            Assert.AreEqual(4, items[1]);
        }

        [TestMethod]
        public void From_End_With_Longer_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(3, 200);
            Assert.AreEqual(2, items.Length);
            Assert.AreEqual(3, items[0]);
            Assert.AreEqual(4, items[1]);
        }

        [TestMethod]
        public void From_Start()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(0, 3);
            Assert.AreEqual(3, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
        }

        [TestMethod]
        public void Index_Less_Then_Zero()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(-2, 2);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void Index_Greater_Then_Array_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(200, 2);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void Negative_Length_With_Index()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            try
            {
                items = items.Crop(0, -2);
                Assert.Fail("Expected ArgumentOutOfRangeException to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void Negative_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(-10);
            Assert.AreEqual(0, items.Length);
        }

        [TestMethod]
        public void Zero_Length_Using_Index()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            try
            {
                items = items.Crop(0, 0);
                Assert.Fail("Expected ArgumentOutOfRangeException to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void Zero_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Crop(0);
            Assert.AreEqual(0, items.Length);
        }     
    }
}
