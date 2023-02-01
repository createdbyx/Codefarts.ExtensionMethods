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
    public class ArrayMoveExtensionMethodTests
    {
        [TestMethod]
        public void MoveLeft_One()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(3, 2, -1);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(3, items[2]);
            Assert.AreEqual(4, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void MoveLeft_Two_Hundred_Items()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(3, 200, -1);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(3, items[2]);
            Assert.AreEqual(4, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void MoveLeft_Negative_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            try
            {
                items = items.Move(3, -2, -1);
                Assert.Fail("Expected ArgumentOutOfRangeException.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void MoveRight_Negative_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            try
            {
                items = items.Move(2, -2, 1);
                Assert.Fail("Expected ArgumentOutOfRangeException.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void MoveLeft_To_Negative_One()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(0, 2, -1);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(1, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void MoveLeft_To_Negative_Array_Length()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(0, 5, -5);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void MoveRight_One()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(3, 2, 1);
            Assert.AreEqual(6, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(3, items[4]);
            Assert.AreEqual(4, items[5]);
        }

        [TestMethod]
        public void MoveRight_One_Inside_Array()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(1, 2, 1);
            Assert.AreEqual(5, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(1, items[2]);
            Assert.AreEqual(2, items[3]);
            Assert.AreEqual(4, items[4]);
        }

        [TestMethod]
        public void MoveRight_Five()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(0, 5, 5);
            Assert.AreEqual(10, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
            Assert.AreEqual(0, items[5]);
            Assert.AreEqual(1, items[6]);
            Assert.AreEqual(2, items[7]);
            Assert.AreEqual(3, items[8]);
            Assert.AreEqual(4, items[9]);
        }

        [TestMethod]
        public void MoveRight_Three()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Move(3, 2, 3);
            Assert.AreEqual(8, items.Length);
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
            Assert.AreEqual(3, items[3]);
            Assert.AreEqual(4, items[4]);
            Assert.AreEqual(0, items[5]);
            Assert.AreEqual(3, items[6]);
            Assert.AreEqual(4, items[7]);
        }
    }
}