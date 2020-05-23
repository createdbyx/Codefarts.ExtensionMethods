// <copyright file="INotifyPropertyChangedExtensionMethods.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using System.Data;

namespace System.ComponentModel
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Codefarts.ExtensionMethods;

    /// <summary>
    /// Provides extension methods for the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public static class INotifyPropertyChangedExtensionMethods
    {
        ///// <summary>Attaches a callbackHooks into a event </summary>
        ///// <param propertyName="list">The list.</param>
        ///// <param propertyName="predicate">The callback used to determine weather or not the item in the list should be removed.</param>
        ///// <exception cref="ArgumentNullException"><paramref propertyName="predicate"/> is <see langword="null"/></exception>
        //public static void OnChanged(this INotifyPropertyChanged source, IEnumerable<string> names, Action<object, PropertyChangedEventArgs> handler)
        //{
        //    if (handler == null)
        //    {
        //        throw new ArgumentNullException(nameof(handler));
        //    }

        //    foreach (var propertyName in names)
        //    {
        //        OnChanged(source, propertyName, handler);
        //    }
        //}

        //public static void OnChanged(this INotifyPropertyChanged source, string propertyName, Action<object, PropertyChangedEventArgs> handler)
        //{
        //    if (handler == null)
        //    {
        //        throw new ArgumentNullException(nameof(handler));
        //    }

        //    source.PropertyChanged += (s, e) =>
        //    {
        //        if (e.PropertyName.Equals(propertyName))
        //        {
        //            handler(s, e);
        //        }
        //    };
        //}

        //public static void OnChanged<TProperty>(this INotifyPropertyChanged source,
        //                                        Expression<Func<TProperty>> property,
        //                                        Action<object, PropertyChangedEventArgs> handler,
        //                                        bool callImmediately)
        //{
        //    OnChanged(source, property, handler, callImmediately, null);
        //}

        //public static void OnChanged<TProperty>(this INotifyPropertyChanged source,
        //                                        Expression<Func<TProperty>> property,
        //                                        Action<object, PropertyChangedEventArgs> handler,
        //                                        bool callImmediately,
        //                                        object sender)
        //{
        //    OnChanged(source, property, handler);
        //    if (callImmediately)
        //    {
        //        handler(sender, new PropertyChangedEventArgs(Helpers.GetMemberInfo(property).Name));
        //    }
        //}

        //public static void OnChanged<TProperty>(this INotifyPropertyChanged source, Expression<Func<TProperty>> property, Action<object, PropertyChangedEventArgs> handler)
        //{
        //    if (source == null)
        //    {
        //        throw new ArgumentNullException(nameof(source));
        //    }

        //    if (handler == null)
        //    {
        //        throw new ArgumentNullException(nameof(handler));
        //    }

        //    source.PropertyChanged += (s, e) =>
        //    {
        //        var propertyName = e.PropertyName;
        //        if (string.Equals(propertyName, Helpers.GetMemberInfo(property).Name, StringComparison.Ordinal))
        //        {
        //            handler(s, e);
        //        }
        //    };
        //}

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

        private static bool InternalNotify<T>(this INotifyPropertyChanged instance, object sender, string propertyName, T oldValue, T newValue, bool hasValues)
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
                    propertyChangedDelegate.Method.Invoke(propertyChangedDelegate.Target, new[] { sender, new PropertyChangedEventArgs<T>(propertyName, oldValue, newValue) });
                }
                else
                {
                    propertyChangedDelegate.Method.Invoke(propertyChangedDelegate.Target, new[] { sender, new PropertyChangedEventArgs(propertyName) });
                }
            }

            return true;
        }

        public static bool Notify<T>(this INotifyPropertyChanged instance, object sender, string propertyName, T oldValue, T newValue)
        {
            return instance.InternalNotify(sender, propertyName, oldValue, newValue, true);
        }

        public static bool Notify(this INotifyPropertyChanged instance, object sender, Expression<Func<object>> property)
        {
            var memberInfo = Helpers.GetMemberInfo(property);
            return instance.InternalNotify<object>(sender, memberInfo.Name, null, null, false);
        }

        public static bool Notify(this INotifyPropertyChanged instance, Expression<Func<object>> property)
        {
            var memberInfo = Helpers.GetMemberInfo(property);
            return instance.InternalNotify<object>(instance, memberInfo.Name, null, null, false);
        }

        public static bool Notify<T>(this INotifyPropertyChanged instance, object sender, Expression<Func<T>> property, T oldValue, T newValue)
        {
            var memberInfo = Helpers.GetMemberInfo(property);
            return instance.InternalNotify(sender, memberInfo.Name, oldValue, newValue, true);
        }

        public static bool Notify<T>(this INotifyPropertyChanged instance, Expression<Func<T>> property, T oldValue, T newValue)
        {
            var memberInfo = Helpers.GetMemberInfo(property);
            return instance.InternalNotify(instance, memberInfo.Name, oldValue, newValue, true);
        }
    }
}