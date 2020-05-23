// <copyright file="INotifyPropertyChangedTests.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using Codefarts.ExtensionMethods;

namespace Codefarts.Tests.ExtensionMethods
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using Codefarts.ExtensionMethods_Tests.Mocks;
    using Codefarts.UnitTestExtensionMethods;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Notify Extension")]
    public class INotifyPropertyChangedTests
    {
        [TestInitialize]
        public void TestInit()
        {
            //Assert.That.PropertyChangedCancel();
            //Thread.Sleep(10);
            //Assert.That.PropertyChangedClearHistory();
        }

        [TestMethod]
        public void NullInstance()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                INotifyPropertyChangedExtensionMethods.Notify(null, nameof(mock.IntegerProperty));
            });
        }

        [TestMethod]
        public void NullPropertyName()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var raised = 0;
            mock.PropertyChanged += (s, e) => { raised++; };
            var result = mock.Notify((string)null);
            Assert.IsTrue(result);
            Assert.AreEqual(1, raised);
        }

        [TestMethod]
        public void EmptyPropertyName()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var raised = 0;
            mock.PropertyChanged += (s, e) => { raised++; };
            var result = mock.Notify(string.Empty);
            Assert.IsTrue(result);
            Assert.AreEqual(1, raised);
        }

        [TestMethod]
        public void WhitespacePropertyName()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var raised = 0;
            mock.PropertyChanged += (s, e) => { raised++; };
            var result = mock.Notify(new string(' ', 5));
            Assert.IsTrue(result);
            Assert.AreEqual(1, raised);
        }

        [TestMethod]
        public void JustName()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            Assert.That.PropertyChanged(mock, nameof(mock.IntegerProperty), () =>
            {
                mock.Notify(nameof(mock.IntegerProperty));
            });
        }

        [TestMethod]
        public void NameAndValues()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;

            mock.PropertyChanged += (s, e) =>
             {
                 Assert.IsInstanceOfType(e, typeof(PropertyChangedEventArgs<int>));
                 var pce = (PropertyChangedEventArgs<int>)e;
                 Assert.AreEqual(111, pce.OldValue);
                 Assert.AreEqual(222, pce.NewValue);
             };

            Assert.That.PropertyChanged(mock, nameof(mock.IntegerProperty), () =>
            {
                mock.Notify(nameof(mock.IntegerProperty), 111, 222);
            });
        }

        [TestMethod]
        public void JustNameVerifySender()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var raised = 0;
            mock.PropertyChanged += (s, e) =>
            {
                raised++;
                Assert.AreEqual(s, mock);
                Assert.IsTrue(ReferenceEquals(s, mock));
            };
            var result = mock.Notify(nameof(mock.IntegerProperty));
            Assert.IsTrue(result);
            Assert.AreEqual(1, raised);
        }

        [TestMethod]
        public void JustNameVerifyCustomSender()
        {
            var mock = new NotifyExtensionMock();
            var customSender = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var raised = 0;
            mock.PropertyChanged += (s, e) =>
            {
                raised++;
                Assert.AreNotEqual(s, mock);
                Assert.IsFalse(ReferenceEquals(s, mock));
                Assert.AreEqual(s, customSender);
                Assert.IsTrue(ReferenceEquals(s, customSender));
            };
            var result = mock.Notify(customSender, nameof(mock.IntegerProperty));
            Assert.IsTrue(result);
            Assert.AreEqual(1, raised);
        }

        [TestMethod]
        public void JustNameSingleRaisedCount()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var raised = 0;
            mock.PropertyChanged += (s, e) => { raised++; };
            mock.Notify(nameof(mock.IntegerProperty));
            Assert.AreEqual(1, raised);
        }

        [TestMethod]
        public void NotifyWithoutHandler()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            var result = mock.Notify(nameof(mock.IntegerProperty));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotifyWithHandler()
        {
            var mock = new NotifyExtensionMock();

            mock.IntegerProperty = 123;
            mock.PropertyChanged += (s, e) => { };
            var result = mock.Notify(nameof(mock.IntegerProperty));
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public async System.Threading.Tasks.Task JustWithNameAsync()
        //{
        //    var mock = new NotifyExtensionMock();

        //    mock.IntegerProperty = 123;
        //    var cancelSource = new CancellationTokenSource(5000);
        //    await Assert.That.PropertyChanged(mock, nameof(mock.IntegerProperty), cancelSource.Token);

        //    mock.IntegerProperty = 456;

        //    // introduce slight delay just to be safe
        //    var startTime = DateTime.Now;
        //    var timedOut = false;
        //    await Assert.That.PropertyChangedWaitThreads() > 0 || timedOut)
        //    {
        //        Thread.Sleep(1);
        //        if (DateTime.Now > startTime + TimeSpan.FromSeconds(5))
        //        {
        //            timedOut = true;
        //        }
        //    }

        //    Assert.IsFalse(timedOut, "Test Timed out");

        //    var calls = Assert.That.PropertyChangedGetHistory();
        //    Assert.AreEqual(1, calls.Count);
        //    Assert.AreEqual(1, calls[nameof(mock.IntegerProperty)]);
        //}
    }
}