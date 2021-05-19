// <copyright file="PropertyChangedEventHandlerExtensions.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace System.ComponentModel
{
    using System.Linq.Expressions;
    using Codefarts.ExtensionMethods;

    /// <summary>
    /// Provides extension methods for the <see cref="PropertyChangedEventHandler"/> delegate.
    /// </summary>
    public static class PropertyChangedEventHandlerExtensions
    {
        public static void Notify<T>(this PropertyChangedEventHandler handler, string propertyName, T oldValue, T newValue)
        {
            if (handler != null)
            {
                handler(handler.Target, new PropertyChangedEventArgs<T>(propertyName, oldValue, newValue));
            }
        }

        public static void Notify<T>(this PropertyChangedEventHandler handler, object sender, string propertyName, T oldValue, T newValue)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs<T>(propertyName, oldValue, newValue));
            }
        }

        public static void Notify(this PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
                handler(handler.Target, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void Notify(this PropertyChangedEventHandler handler, object sender, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        } 
    }
}