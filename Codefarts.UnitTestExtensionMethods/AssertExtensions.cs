// <copyright file="AssertExtensions.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.ComponentModel;

    public static class AssertExtensions
    {
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
            sender.PropertyChanged += (s, e) => { raised += e.PropertyName == propertyName ? 1 : 0; };

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
            sender.PropertyChanged += (s, e) => { raised += e.PropertyName == propertyName ? 1 : 0; };

            callback();

            Assert.AreEqual(1, raised, $"sender raised property changed event {raised} times with matching property name.");
        }
    }
}