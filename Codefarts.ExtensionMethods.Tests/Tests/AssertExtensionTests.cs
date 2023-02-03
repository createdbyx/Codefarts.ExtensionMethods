// <copyright file="AssertExtensionTests.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.ExtensionMethods.Tests
{
    using System;
    using Codefarts.ExtensionMethods.Tests.Mocks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Assert Extensions")]
    public class AssertExtensionTests
    {
        [TestMethod]
        public void SettingPropertyValue()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.That.PropertyChanged(mock, nameof(mock.IntegerProperty), 456);
            Assert.AreEqual(456, mock.IntegerProperty);
        }

        [TestMethod]
        public void NullInstanceParam()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                AssertExtensions.PropertyChanged(null, mock, nameof(mock.IntegerProperty), 456);
            });
        }

        [TestMethod]
        public void NullSenderParam()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Assert.That.PropertyChanged(null, nameof(mock.IntegerProperty), 456);
            });
        }

        [TestMethod]
        public void NullPropertyNameParam()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Assert.That.PropertyChanged(mock, null, 456);
            });
        }

        [TestMethod]
        public void EmptyPropertyNameParam()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Assert.That.PropertyChanged(mock, string.Empty, 456);
            });
        }

        [TestMethod]
        public void WhitespacePropertyNameParam()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Assert.That.PropertyChanged(mock, new string(' ', 12), 456);
            });
        }

        [TestMethod]
        public void NoPropertySetter()
        {
            var mock = new NotifyExtensionMock();

            Assert.ThrowsException<NullReferenceException>(() =>
            {
                Assert.That.PropertyChanged(mock, nameof(mock.NoSetterBool), true);
            });
        }

        [TestMethod]
        public void PrivatePropertySetter()
        {
            var mock = new NotifyExtensionMock();

            Assert.ThrowsException<NullReferenceException>(() =>
            {
                Assert.That.PropertyChanged(mock, nameof(mock.PrivateSetterFloat), 2.5f);
            });
        }

        [TestMethod]
        public void InternalPropertySetter()
        {
            var mock = new NotifyExtensionMock();

            Assert.ThrowsException<NullReferenceException>(() =>
            {
                Assert.That.PropertyChanged(mock, nameof(mock.InternalSetterFloat), 5f);
            });
        }
    }
}