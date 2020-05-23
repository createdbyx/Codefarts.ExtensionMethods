// <copyright file="AssertExtensions.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using System;
using System.Threading.Tasks;

namespace Codefarts.UnitTestExtensionMethods
{
    using System.Collections.Concurrent;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class AssertExtensions
    {
        //  private static ConcurrentDictionary<string, int> eventCalls;
        //  private static int waitingThreads = 0;
        //  private static bool cancelWaiting;

        //static AssertExtensions()
        //{
        //    eventCalls = new ConcurrentDictionary<string, int>();
        //}

        //public static void PropertyChangedCancel(this Assert instance)
        //{
        //    cancelWaiting = true;
        //}

        //public static void PropertyChangedClearHistory(this Assert instance)
        //{
        //    eventCalls.Clear();
        //}

        //public static ReadOnlyDictionary<string, int> PropertyChangedGetHistory(this Assert instance)
        //{
        //    return new ReadOnlyDictionary<string, int>(eventCalls);
        //}

        //public static int PropertyChangedWaitThreads(this Assert instance)
        //{
        //    return waitingThreads;
        //}

        public static void PropertyChanged<T>(this Assert instance, INotifyPropertyChanged sender, string propertyName, T newValue)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var raised = 0;
            sender.PropertyChanged += (s, e) =>
            {
                raised += e.PropertyName == propertyName ? 1 : 0;
            };

            var propInfo = sender.GetType().GetProperty(propertyName);
            var setMethod = propInfo.GetSetMethod();

            if (setMethod == null)
            {
                throw new NullReferenceException($"Property '{propertyName}' has no setter.");
            }

            setMethod.Invoke(sender, new object[] { newValue });

            Assert.AreEqual(1, raised, $"sender raised property changed event {raised} times with matching property name.");
        }

        public static void PropertyChanged(this Assert instance, INotifyPropertyChanged sender, string propertyName, Action callback)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var raised = 0;
            sender.PropertyChanged += (s, e) =>
            {
                raised += e.PropertyName == propertyName ? 1 : 0;
            };

            callback();

            Assert.AreEqual(1, raised, $"sender raised property changed event {raised} times with matching property name.");
        }

        //public static async Task<bool> PropertyChanged(this Assert instance, INotifyPropertyChanged sender, string propertyName, CancellationToken cancelToken)
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException(nameof(instance));
        //    }

        //    if (sender == null)
        //    {
        //        throw new ArgumentNullException(nameof(sender));
        //    }

        //    if (string.IsNullOrWhiteSpace(propertyName))
        //    {
        //        throw new ArgumentNullException(nameof(propertyName));
        //    }

        //    var queState = Task.Factory.StartNew(
        //        state =>
        //        {
        //            var raised = false;
        //            var implementation = (INotifyPropertyChanged)state;
        //            implementation.PropertyChanged += (s, e) =>
        //            {
        //                //if (!eventCalls.ContainsKey(propertyName))
        //                //{
        //                //    eventCalls[propertyName] = 0;
        //                //}

        //                //eventCalls[propertyName]++;

        //                raised = e.PropertyName == propertyName;
        //            };

        //            //Interlocked.Increment(ref waitingThreads);

        //            while (!raised || !cancelToken.IsCancellationRequested)
        //            {
        //                Thread.Sleep(1);
        //            }

        //            return raised;
        //            //Interlocked.Decrement(ref waitingThreads);

        //        }, sender, cancelToken, TaskCreationOptions.None, TaskScheduler.Default);

        //    return queState.Result;
        //    //if (!queState)
        //    //{
        //    //    Assert.Fail("Failed to queue background thread.");
        //    //    return;
        //    //}

        //    //// give chance for thread to wait in a loop
        //    //Thread.Sleep(defaultWait ?? 1000);
        //}
    }
}
