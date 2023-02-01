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

    [TestClass, TestCategory("Base Types")]
    public class SingleExtensionTests
    {
        [TestMethod]
        public void IsInRange_Number_In_Range()
        {
            Assert.IsTrue(123f.IsInRange(0f, 500f));
        }

        [TestMethod]
        public void IsInRange_Number_Below_Range()
        {
            Assert.IsFalse(123f.IsInRange(400f, 500f));
        }

        [TestMethod]
        public void IsInRange_Number_Above_Range()
        {
            Assert.IsFalse(123f.IsInRange(0f, 100f));
        }

        [TestMethod]
        public void IsInRange_Number_At_Min_Range()
        {
            Assert.IsTrue(123f.IsInRange(123f, 500f));
        }

        [TestMethod]
        public void IsInRange_Number_At_Max_Range()
        {
            Assert.IsTrue(123f.IsInRange(0f, 123f));
        }
    }
}