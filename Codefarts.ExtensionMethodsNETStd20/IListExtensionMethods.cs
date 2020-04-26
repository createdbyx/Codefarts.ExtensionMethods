// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>            
namespace System.Collections
{
    /// <summary>
    /// Provides extension methods for the IList interface.
    /// </summary>
    public static class IListExtensionMethods
    {
        /// <summary>Removes any items in a list that match a criteria</summary>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The callback used to determine weather or not the item in the list should be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is <see langword="null"/></exception>
        public static void RemoveAny(this IList list, Predicate<object> predicate)
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

        /// <summary>Moves the specified item in a list down one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to move down.</param>
        public static void MoveDown(this IList list, int index)
        {
            MoveDown(list, index, false);
        }

        /// <summary>Moves the specified item in a list down one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to move down.</param>
        /// <param name="remove">If set to <c>true</c> items will be removed and re-inserted.</param>
        public static void MoveDown(this IList list, int index, bool remove)
        {
            var nextIndex = index + 1 > list.Count - 1 ? list.Count - 1 : index + 1;
            if (nextIndex == index)
            {
                return;
            }

            Swap(list, index, nextIndex, remove);
        }

        /// <summary>Moves the specified item in a list down one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The the item to move down.</param>
        public static void MoveDown(this IList list, object item)
        {
            MoveDown(list, item, false);
        }

        /// <summary>Moves the specified item in a list down one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The the item to move down.</param>
        /// <param name="remove">If set to <c>true</c> items will be removed and re-inserted.</param>
        public static void MoveDown(this IList list, object item, bool remove)
        {
            var index = list.IndexOf(item);
            if (index == -1)
            {
                throw new IndexOutOfRangeException("'item' does not exist in the list.");
            }

            MoveDown(list, index, remove);
        }

        /// <summary>Moves the specified item in a list up one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The the item to move up.</param>
        public static void MoveUp(this IList list, object item)
        {
            MoveUp(list, item, false);
        }

        /// <summary>Moves the specified item in a list up one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The the item to move up.</param>
        /// <param name="remove">If set to <c>true</c> items will be removed and re-inserted.</param>
        public static void MoveUp(this IList list, object item, bool remove)
        {
            var index = list.IndexOf(item);
            if (index == -1)
            {
                throw new IndexOutOfRangeException("'item' does not exist in the list.");
            }

            MoveUp(list, index, remove);
        }

        /// <summary>Moves the specified item in a list up one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to move up.</param>
        public static void MoveUp(this IList list, int index)
        {
            MoveUp(list, index, false);
        }

        /// <summary>Moves the specified item in a list up one entry.</summary>
        /// <param name="list">The list.</param>
        /// <param name="index">The index of the item to move up.</param>
        /// <param name="remove">If set to <c>true</c> items will be removed and re-inserted.</param>
        public static void MoveUp(this IList list, int index, bool remove)
        {
            var prevIndex = index - 1 < 0 ? 0 : index - 1;
            if (prevIndex == index)
            {
                return;
            }

            Swap(list, index, prevIndex, remove);
        }

        /// <summary>Swaps the specified items in a list.</summary>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index of item A.</param>
        /// <param name="indexB">The index of item B.</param>
        /// <param name="remove">If set to <c>true</c> items will be removed and re-inserted.</param>
        public static void Swap(this IList list, int indexA, int indexB, bool remove)
        {
            if (indexA == indexB)
            {
                return;
            }

            indexA.IsInRange(0, list.Count - 1, true, "indexA is out of range.");
            indexB.IsInRange(0, list.Count - 1, true, "indexB is out of range.");

            if (remove)
            {
                var first = Math.Min(indexA, indexB);
                var second = Math.Max(indexA, indexB);

                var tempA = list[first];
                var tempB = list[second];

                list.RemoveAt(second);
                list.RemoveAt(first);

                list.Insert(first, tempB);
                list.Insert(second, tempA);
            }
            else
            {
                var temp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = temp;
            }
        }

        /// <summary>Swaps the specified items in a list.</summary>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index of item A.</param>
        /// <param name="indexB">The index of item B.</param>
        /// <remarks>Items are swapped and not removed or inserted.</remarks>
        public static void Swap(this IList list, int indexA, int indexB)
        {
            Swap(list, indexA, indexB, false);
        }

        /// <summary>Swaps the specified items in a list and return true if successful.</summary>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index of item A.</param>
        /// <param name="indexB">The index of item B.</param>
        /// <remarks>Items are swapped and not removed or inserted.</remarks>
        /// <returns>true if successful.</returns>
        public static bool TrySwap(this IList list, int indexA, int indexB)
        {
            try
            {
                Swap(list, indexA, indexB);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>Swaps the specified items in a list and return true if successful.</summary>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index of item A.</param>
        /// <param name="indexB">The index of item B.</param>
        /// <param name="remove">If set to <c>true</c> items will be removes and re-inserted.</param>
        /// <returns>true if successful.</returns>
        public static bool TrySwap(this IList list, int indexA, int indexB, bool remove)
        {
            try
            {
                Swap(list, indexA, indexB, remove);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
