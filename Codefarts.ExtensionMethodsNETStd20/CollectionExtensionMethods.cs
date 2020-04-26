namespace System.Collections
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides extension methods for the <see cref="Collection{T}"/> class.
    /// </summary>
    public static class CollectionExtensionMethods
    {
        /// <summary>Removes any items in a generic collection that match a criteria</summary>
        /// <typeparam name="T">The generic type that the generic collection stores.</typeparam>
        /// <param name="list">The generic collection to remove item from.</param>
        /// <param name="predicate">The callback used to determine weather or not the item in the list should be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is <see langword="null"/></exception>
        public static void RemoveAny<T>(this Collection<T> list, Predicate<T> predicate)
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

        /// <summary>
        /// Adds items to a <see cref="Collection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The generic type that the generic collection stores.</typeparam>
        /// <param name="list">The collection where items will be added to.</param>
        /// <param name="items">The items to be added.</param>
        public static void AddRange<T>(this Collection<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Quick sorts a <see cref="Collection{T}"/> using a comparison delegate.
        /// </summary>
        /// <typeparam name="T">The generic type that the generic collection stores.</typeparam>
        /// <param name="list">The collection where items will be added to.</param>
        /// <param name="compare">The compare delegate used to compare entries.</param>
        public static void QuickSort<T>(this Collection<T> list, Comparison<T> compare)
        {
            QuickSort(list, compare, 0, list.Count - 1);
        }

        /// <summary>
        /// Quick sorts a <see cref="Collection{T}" /> using a comparison delegate.
        /// </summary>
        /// <typeparam name="T">The generic type that the generic collection stores.</typeparam>
        /// <param name="list">The collection where items will be added to.</param>
        /// <param name="compare">The compare delegate used to compare entries.</param>
        /// <param name="left">The left index.</param>
        /// <param name="right">The right index.</param>
        /// <exception cref="System.ArgumentNullException">compare</exception>
        public static void QuickSort<T>(this Collection<T> list, Comparison<T> compare, int left, int right)
        {
            var i = left;
            var j = right;
            var pivot = list[(left + right) / 2];

            while (i <= j)
            {
                while (compare(list[i], pivot) < 0)
                {
                    i++;
                }

                while (compare(list[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    var tmp = list[i];
                    list[i] = list[j];
                    list[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                QuickSort(list, compare, left, j);
            }

            if (i < right)
            {
                QuickSort(list, compare, i, right);
            }
        }
    }
}