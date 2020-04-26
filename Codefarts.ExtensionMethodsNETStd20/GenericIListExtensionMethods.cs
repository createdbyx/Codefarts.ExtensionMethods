namespace System.Collections
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Provides extension methods for the generic IList interface.
    /// </summary>
    public static class GenericIListExtensionMethods
    {
        /// <summary>Removes any items in a list that match a criteria</summary>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The callback used to determine weather or not the item in the list should be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is <see langword="null"/></exception>
        public static void RemoveAny<T>(this IList<T> list, Predicate<T> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (predicate(item))
                {
                    list.RemoveAt(i);
                }
            }
        }

        /// <summary>Builds a new list from the item that match a criteria</summary>
        /// <param name="list">The list to retrieve item from.</param>
        /// <param name="predicate">The callback used to determine weather or not the item in the list should be removed.</param>
        /// <param name="remove">If has a value and the value is true, items that the <see cref="predicate"/> argument filters out will be return in the new list.</param>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is <see langword="null"/></exception>
        /// <remarks>Items in the new list are in the same order that they were in the source list.</remarks>
        public static IList GetAny<T>(this IList<T> list, Predicate<object> predicate, bool? remove)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var newList = new List<T>();
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (predicate(item))
                {
                    newList.Insert(0, item); // maintains list order of item copied
                    if (remove.HasValue && remove.Value)
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            return newList;
        }

        /// <summary>Retrieves and removes an item from the list.</summary>
        /// <typeparam name="T">The type that the generic list contains.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to be pulled out of the list.</param>
        /// <returns>The item retrieved from the list.</returns>
        public static T PullItemAt<T>(this IList<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>Retrieves and removes an item from the list.</summary>
        /// <typeparam name="T">The type that the generic list contains.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to be pulled out of the list.</param>
        /// <param name="value">The value that was retrieved from the list.</param>
        /// <returns>true if succsesful; otherwise false.</returns>
        public static bool TryPullItemAt<T>(this IList<T> list, int index, out T value)
        {
            try
            {
                value = list[index];
                list.RemoveAt(index);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }

        /// <summary>
        /// Tries to get a value from the generic list.
        /// </summary>
        /// <typeparam name="T">The type that the generic list contains.</typeparam>
        /// <param name="list">The generic list reference.</param>
        /// <param name="predicate">The predicate used to find the desired element.</param>
        /// <param name="item">The item to be returned if the <see cref="predicate"/> argument finds a match.</param>
        /// <returns>true if a item was found; otherwise false.</returns>
        /// <remarks>This method uses Linq internaly.</remarks>
        public static bool TryGet<T>(this IList<T> list, Func<T, bool> predicate, out T item)
        {
            var result = list.Where(predicate).ToArray();
            item = result.FirstOrDefault();
            return result.Length != 0;
        }

        /// <summary>
        /// Tries to get a value from the generic list.
        /// </summary>
        /// <typeparam name="T">The type that the generic list contains.</typeparam>
        /// <typeparam name="S">The type that the selector function will return.</typeparam>
        /// <param name="list">The generic list reference.</param>
        /// <param name="predicate">The predicate used to find the desired element.</param>
        /// <param name="selector">The selector function used to retrieve a value from the matched item. determined by the <see cref="predicate"/> argument.</param>
        /// <param name="item">The item to be returned if the <see cref="predicate"/> argument finds a match.</param>
        /// <returns>true if a item was found; otherwise false.</returns>
        /// <remarks>This method uses Linq internaly.</remarks>
        public static bool TryGet<T, S>(this IList<T> list, Func<T, bool> predicate, Func<T, S> selector, out S item)
        {
            var result = list.Where(predicate).ToArray();
            item = default(S);
            item = result.Length > 0 ? selector(result.FirstOrDefault()) : item;
            return result.Length != 0;
        }

#if !PORTABLE
        /// <summary>Iterates through a <see cref="IList{T}"/> for.</summary>
        /// <typeparam name="T">The type stored in <paramref name="source"/>.</typeparam>
        /// <param name="source">The source list to process.</param>
        /// <param name="action">The action callback that will be called for each item.</param>
        /// <param name="startIndex">The start index where processing begins.</param>
        /// <param name="count">The number of items to process.</param>
        /// <param name="duration">The max duration in milliseconds to execute for before stopping iterating through the list.</param>
        /// <returns>The index of the last item that <paramref name="action" /> was called with. Otherwise -1 if no items were iterated over.</returns>
        public static int TimeManagedIterator<T>(this IList<T> source, Action<T> action, int startIndex, int count, double duration)
        {
            // make sure source is available
            if (source == null)
            {
                return -1;
            }

            // setup last index
            var endIndex = Math.Min(source.Count, startIndex + count);

            // validate start index
            if (startIndex < 0 || startIndex > endIndex)
            {
                return -1;
            }

            var currentDuration = 0L;
            var stopwatch = new Stopwatch();
            for (var i = startIndex; i < endIndex; i++)
            {
                var item = source[i];
                stopwatch.Start();
                action(item);
                stopwatch.Stop();
                var actionDuration = stopwatch.ElapsedTicks;
                currentDuration += actionDuration;
                stopwatch.Reset();
                if (TimeSpan.FromTicks(currentDuration).TotalMilliseconds > duration)
                {
                    return i;
                }
            }

            return -1;
        }
#endif
    }
}