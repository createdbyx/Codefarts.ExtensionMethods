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
    public class ArrayAddExtensionMethodTests
    {
        [TestMethod]
        public void Add_Array()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Add(new[] { 5 });
            Assert.AreEqual(6, items.Length);
            for (var i = 0; i < items.Length; i++)
            {
                Assert.AreEqual(i, items[i]);
            }
        }

        [TestMethod]
        public void Add()
        {
            var items = new[] { 0, 1, 2, 3, 4 };
            items = items.Add(5);
            Assert.AreEqual(6, items.Length);
            for (var i = 0; i < items.Length; i++)
            {
                Assert.AreEqual(i, items[i]);
            }
        }
    }
}