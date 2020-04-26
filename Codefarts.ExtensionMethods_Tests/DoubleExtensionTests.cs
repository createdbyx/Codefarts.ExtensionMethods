namespace Codefarts.Tests.ExtensionMethods
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Base Types")]
    public class DoubleExtensionTests
    {
        [TestMethod]
        public void IsInRange_Number_In_Range()
        {
            Assert.IsTrue(123d.IsInRange(0d, 500d));
        }

        [TestMethod]
        public void IsInRange_Number_Below_Range()
        {
            Assert.IsFalse(123d.IsInRange(400d, 500d));
        }

        [TestMethod]
        public void IsInRange_Number_Above_Range()
        {
            Assert.IsFalse(123d.IsInRange(0d, 100d));
        }

        [TestMethod]
        public void IsInRange_Number_At_Min_Range()
        {
            Assert.IsTrue(123d.IsInRange(123d, 500d));
        }

        [TestMethod]
        public void IsInRange_Number_At_Max_Range()
        {
            Assert.IsTrue(123d.IsInRange(0f, 123d));
        }
    }
}