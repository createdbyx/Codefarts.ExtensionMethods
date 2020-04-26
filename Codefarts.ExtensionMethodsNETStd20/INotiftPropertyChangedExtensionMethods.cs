namespace System.Collections
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public static class INotiftPropertyChangedExtensionMethods
    {
        /// <summary>Attaches a callbackHooks into a event </summary>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The callback used to determine weather or not the item in the list should be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is <see langword="null"/></exception>
        public static void OnChanged(this INotifyPropertyChanged source, IEnumerable<string> names, Action<object, PropertyChangedEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            foreach (var name in names)
            {
                OnChanged(source, name, handler);
            }
        }

        public static void OnChanged(this INotifyPropertyChanged source, string name, Action<object, PropertyChangedEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            source.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(name))
                {
                    handler(s, e);
                }
            };
        }

        public static void OnChanged<TProperty>(this INotifyPropertyChanged source,
            Expression<Func<TProperty>> property, Action<object, PropertyChangedEventArgs> handler,
            bool callImmediatley)
        {
            OnChanged(source, property, handler, callImmediatley, null);
        }

        public static void OnChanged<TProperty>(this INotifyPropertyChanged source,
            Expression<Func<TProperty>> property, Action<object, PropertyChangedEventArgs> handler, bool callImmediatley, object sender)
        {
            OnChanged(source, property, handler);
            if (callImmediatley)
            {
                handler(sender, new PropertyChangedEventArgs(GetMemberInfo(property).Name));
            }
        }

        public static void OnChanged<TProperty>(this INotifyPropertyChanged source, Expression<Func<TProperty>> property, Action<object, PropertyChangedEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            source.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(GetMemberInfo(property).Name))
                {
                    handler(s, e);
                }
            };
        }

        private static MemberInfo GetMemberInfo(Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
    }
}