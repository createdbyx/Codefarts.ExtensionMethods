// <copyright file="INotifyPropertyChangedExtensionMethods.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using Codefarts.ExtensionMethods;

namespace System.ComponentModel
{
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public static class INotifyPropertyChangedExtensionMethods
    {
        public static bool Notify(this INotifyPropertyChanged instance, string propertyName)
        {
            return instance.InternalNotify<object>(instance, propertyName, null, null, false);
        }

        public static bool Notify<T>(this INotifyPropertyChanged instance, string propertyName, T oldValue, T newValue)
        {
            return instance.InternalNotify(instance, propertyName, oldValue, newValue, true);
        }

        public static bool Notify(this INotifyPropertyChanged instance, object sender, string propertyName)
        {
            return instance.InternalNotify<object>(sender, propertyName, null, null, false);
        }

        private static bool InternalNotify<T>(this INotifyPropertyChanged instance, object sender, string propertyName, T oldValue, T newValue,
            bool hasValues)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            // get the internal eventDelegate
            var objectType = instance.GetType();

            // search the base type, which contains the PropertyChanged event field.
            FieldInfo propChangedFieldInfo = null;
            while (objectType != null)
            {
                propChangedFieldInfo = objectType.GetField(nameof(instance.PropertyChanged), BindingFlags.Instance | BindingFlags.NonPublic);
                if (propChangedFieldInfo != null)
                {
                    break;
                }

                objectType = objectType.BaseType;
            }

            if (propChangedFieldInfo == null)
            {
                return false;
            }

            // get prop changed event field value
            var fieldValue = propChangedFieldInfo.GetValue(instance);
            if (fieldValue == null)
            {
                return false;
            }

            var eventDelegate = fieldValue as MulticastDelegate;
            if (eventDelegate == null)
            {
                return false;
            }

            // get invocation list
            var delegates = eventDelegate.GetInvocationList();

            // invoke each delegate
            foreach (var propertyChangedDelegate in delegates)
            {
                if (hasValues)
                {
                    propertyChangedDelegate.Method.Invoke(propertyChangedDelegate.Target,
                        new[] { sender, new PropertyChangedEventArgs<T>(propertyName, oldValue, newValue) });
                }
                else
                {
                    propertyChangedDelegate.Method.Invoke(propertyChangedDelegate.Target,
                        new[] { sender, new PropertyChangedEventArgs(propertyName) });
                }
            }

            return true;
        }

        public static bool Notify<T>(this INotifyPropertyChanged instance, object sender, string propertyName, T oldValue, T newValue)
        {
            return instance.InternalNotify(sender, propertyName, oldValue, newValue, true);
        }
    }
}