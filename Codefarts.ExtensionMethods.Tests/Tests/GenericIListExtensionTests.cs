// // <copyright>
// //   Copyright (c) 2012 Codefarts
// //   All rights reserved.
// //   contact@codefarts.com
// //   http://www.codefarts.com
// // </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codefarts.ExtensionMethods.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("IList")]
    public class GenericIListExtensionTests
    {
        private IList<string> singleInstanceStringList;
        private IList<string> doubleInstanceStringList;

        [TestInitialize]
        public void Setup()
        {
            this.singleInstanceStringList = new List<string>(new[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" });
            this.doubleInstanceStringList = new List<string>(this.singleInstanceStringList);
            foreach (var item in this.singleInstanceStringList)
            {
                this.doubleInstanceStringList.Add(item);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.singleInstanceStringList = null;
            this.doubleInstanceStringList = null;
        }

        [TestMethod]
        public void RemoveAnySingleInstance()
        {
            Assert.AreEqual(10, this.singleInstanceStringList.Count);
            this.singleInstanceStringList.RemoveAny(x => x.Equals("Four"));
            Assert.AreEqual(9, this.singleInstanceStringList.Count);
        }

        [TestMethod]
        public void RemoveAnyMultipleInstance()
        {
            Assert.AreEqual(20, this.doubleInstanceStringList.Count);
            this.doubleInstanceStringList.RemoveAny(x => x.Equals("Four"));
            Assert.AreEqual(18, this.doubleInstanceStringList.Count);
        }

        [TestMethod]
        public void RemoveAnyMultipleInstanceContains()
        {
            Assert.AreEqual(20, this.doubleInstanceStringList.Count);
            var removeItems = new[] { "One", "Two" };
            this.doubleInstanceStringList.RemoveAny(x => removeItems.Any(i => x.Equals(i)));
            Assert.AreEqual(16, this.doubleInstanceStringList.Count);
        }

        [TestMethod]
        public void RemoveAnyMultipleInstanceNotFound()
        {
            Assert.AreEqual(20, this.doubleInstanceStringList.Count);
            var removeItems = new[] { "X1", "X2" };
            this.doubleInstanceStringList.RemoveAny(x => removeItems.Any(i => x.Equals(i)));
            Assert.AreEqual(20, this.doubleInstanceStringList.Count);
        }

        [TestMethod]
        public void RemoveAnyWithNullPredicate()
        {
            try
            {
                this.singleInstanceStringList.RemoveAny(null);
                Assert.Fail("Should have thrown ArgumentNullException.");
            }
            catch (Exception e)
            {
            }
        }
    }
}
