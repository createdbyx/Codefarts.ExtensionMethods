namespace Codefarts.ExtensionMethods.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Base Types")]
    public class IntegerExtensionTests
    {
        [TestMethod]
        public void IsInRange_Number_In_Range()
        {
            Assert.IsTrue(123.IsInRange(0, 500));
        }

        [TestMethod]
        public void IsInRange_Number_Below_Range()
        {
            Assert.IsFalse(123.IsInRange(400, 500));
        }

        [TestMethod]
        public void IsInRange_Number_Above_Range()
        {
            Assert.IsFalse(123.IsInRange(0, 100));
        }

        [TestMethod]
        public void IsInRange_Number_At_Min_Range()
        {
            Assert.IsTrue(123.IsInRange(123, 500));
        }

        [TestMethod]
        public void IsInRange_Number_At_Max_Range()
        {
            Assert.IsTrue(123.IsInRange(0, 123));
        }
    }
}