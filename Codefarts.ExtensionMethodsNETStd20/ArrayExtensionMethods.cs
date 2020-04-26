// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>

namespace System
{
    /// <summary>
        /// Provides various array extension methods for managing single dimensional arrays.
        /// </summary>
        public static class ArrayExtensionMethods
    {
        /// <summary>
        /// Inserts one array into another array at the specified index.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The index in the destination array where insertion takes place.</param>
        /// <param name="sourceArray">The source array that will be inserted.</param>
        /// <returns>Returns the resized and updated destination array.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If 'index' is out of bounds of the destination array.
        /// </exception>
        public static T[] Insert<T>(this T[] array, int index, T[] sourceArray)
        {
            if (array == null || sourceArray == null || sourceArray.Length == 0)
            {
                return array;
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index > array.Length + 1)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            Array.Resize(ref array, array.Length + sourceArray.Length);
            Array.Copy(array, index, array, index + sourceArray.Length, array.Length - sourceArray.Length - index);
            sourceArray.CopyTo(array, index);
            return array;
        }

        /// <summary>
        /// Replaces a sequence of array entries at the specified index.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The index in the destination array where replacement takes place.</param>
        /// <param name="sourceArray">The source array that will replace the existing entries.</param>
        /// <returns>Returns the resized and updated destination array.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If 'index' is out of bounds of the destination array.
        /// </exception>
        public static T[] Replace<T>(this T[] array, int index, T[] sourceArray)
        {
            if (array == null || sourceArray == null || sourceArray.Length == 0)
            {
                return array;
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index > array.Length - 1)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var expandSize = (index + sourceArray.Length) - array.Length;
            if (expandSize > 0)
            {
                Array.Resize(ref array, array.Length + expandSize);
            }

            sourceArray.CopyTo(array, index);
            return array;
        }

        /// <summary>
        /// Adds the specified source array to the end of the destination array.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="sourceArray">The source array that will be added to the end of the destination array.</param>
        /// <returns>Returns the resized and updated destination array.</returns>
        public static T[] Add<T>(this T[] array, T[] sourceArray)
        {
            if (array == null || sourceArray == null || sourceArray.Length == 0)
            {
                return array;
            }

            Array.Resize(ref array, array.Length + sourceArray.Length);
            sourceArray.CopyTo(array, array.Length - sourceArray.Length);
            return array;
        }

        /// <summary>
        /// Adds a new extry to the end of the destination array.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="value">The item that will be added to the end of the destination array.</param>
        /// <returns>Returns the resized and updated destination array.</returns>
        public static T[] Add<T>(this T[] array, T value)
        {
            if (array == null)
            {
                return array;
            }

            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = value;
            return array;
        }

        /// <summary>
        /// Crops an array to a specified length.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="length">The length that the destination array will be set to.</param>
        /// <returns>Returns the resized and updated destination array.</returns>
        public static T[] Crop<T>(this T[] array, int length)
        {
            if (array == null)
            {
                return array;
            }

            Array.Resize(ref array, Math.Max(length, 0));
            return array;
        }

        /// <summary>
        /// Crops an array to a specified length.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The index in the destination array where cropping begins at.</param>
        /// <param name="length">The length that the destination array will be set to.</param>
        /// <returns>
        /// Returns the resized and updated destination array.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">length;'length' argument must be greater then 0.</exception>
        public static T[] Crop<T>(this T[] array, int index, int length)
        {
            if (array == null)
            {
                return array;
            }

            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length", "'length' argument must be greater then 0.");
            }

            if (index < 0 || index > array.Length - 1)
            {
                return array;
            }

            length = Math.Min(length, array.Length - index);
            if (index > 0)
            {
                Array.Copy(array, index, array, 0, length);
            }

            Array.Resize(ref array, length);
            return array;
        }

        /// <summary>
        /// Moves the specified entries in the array by a set ammount.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The start index where entries will be moved from.</param>
        /// <param name="length">The number of entries to be moved.</param>
        /// <param name="shift">The ammount and direction to move the specified entries.</param>
        /// <returns>
        /// Returns the resized and updated destination array.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">length;'length' argument must be greater then 0.</exception>
        /// <remarks><p>To move entries to the left (towards 0) specify a negative shift value and a positive shift value to move entries to the right.</p>
        /// <example>
        /// <code>
        /// var items = new[] { 0, 1, 2, 3, 4 };
        /// items = items.Move(3, 2, -1);
        /// </code>
        /// Result should be { 0, 1, 3, 4, 4 }
        /// </example></remarks>
        public static T[] Move<T>(this T[] array, int index, int length, int shift)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("length", "'length' argument must be greater then 0.");
            }

            if (shift > 0 && index + length + shift > array.Length - 1)
            {
                Array.Resize(ref array, array.Length + (index + length + shift - array.Length));
            }

            if (index + shift < 0)
            {
                length += index + shift;
                index = -(index + shift);
            }

            length = Math.Min(array.Length - index, length);
            if (length > 0)
            {
                Array.Copy(array, index, array, index + shift, length);
            }

            return array;
        }

        /// <summary>
        /// Removes a range of entries inside an array.
        /// </summary>
        /// <typeparam name="T">Specifies the generic type of the array.</typeparam>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The start index where entries will be removed from.</param>
        /// <param name="length">The number of entries to be removed.</param>
        /// <returns>
        /// Returns the resized and updated destination array.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">index</exception>
        public static T[] RemoveRange<T>(this T[] array, int index, int length)
        {
            if (length < 1)
            {
                return array;
            }

            if (index < 0 || index > array.Length - 1)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index + length > array.Length - 1)
            {
                Array.Resize(ref array, index);
                return array;
            }

            var endLength = Math.Max(0, Math.Min(array.Length - index, array.Length - (index + length)));
            var tempArray = new T[endLength];
            Array.Copy(array, index + length, tempArray, 0, endLength);
            Array.Resize(ref array, array.Length - length);
            tempArray.CopyTo(array, array.Length - endLength);
            return array;
        }
    }
}