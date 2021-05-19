namespace System
{
    /// <summary>
    /// Provides extension methods for managing single dimensional arrays as though they are three dimensional arrays.
    /// </summary>
    public static class Array3DExtensionMethods
    {
        /// <summary>
        /// Sets the value in a single dimensional array that represents a three dimensional sequence.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array whose value is to be set.</param>
        /// <param name="width">The width of the three dimensional sequence.</param>
        /// <param name="height">The height of the three dimensional sequence.</param>
        /// <param name="x">The x index (0 to width - 1).</param>
        /// <param name="y">The y index (0 to height - 1).</param>
        /// <param name="z">The z index (0 to depth - 1).</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// width or height is less then 1.
        /// </exception>
        /// <remarks>
        /// <p>This method provides an alternative to working with three dimensional arrays "var value = new int[3,3,3];" by operating
        /// on a single dimensional array using a math formula to determine the index into the array.</p>
        /// <p>Think of a multi-layered image. Each image layer consists of a grid of cells defined by width * height.</p>
        /// <p>We can use the formula "layer * (width * height)" to get the starting index of the layer in the array.
        /// To get the index in the image we can use the formula "(y * width) + x".
        /// Combining these two formulas we can access any grid cell of any layer in the array like so "(layer * (width * height)) + ((y * width) + x)".</p>
        /// <p>This method does not perform range checking and will throw index out of range exceptions if invalid arguments are specified.</p>
        /// </remarks>
        public static void Set3D<T>(this T[] array, int width, int height, int x, int y, int z, T value)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            array[(z * (width * height)) + ((y * width) + x)] = value;
        }

        /// <summary>
        /// Gets the value in a single dimensional array that represents a three dimensional sequence.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array whose value is to be retrieves.</param>
        /// <param name="width">The width of the three dimensional sequence.</param>
        /// <param name="height">The height of the three dimensional sequence.</param>
        /// <param name="x">The x index (0 to width - 1).</param>
        /// <param name="y">The y index (0 to height - 1).</param>
        /// <param name="z">The z index (0 to depth - 1).</param>
        /// <returns>Returns the value stored in the array.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// width or height is less then 1.
        /// </exception>
        /// <remarks>
        /// <p>This method provides an alternative to working with three dimensional arrays "var value = new int[3,3,3];" by operating
        /// on a single dimensional array using a math formula to determine the index into the array.</p>
        /// <p>Think of a multi-layered image. Each image layer consists of a grid of cells defined by width * height.</p>
        /// <p>We can use the formula "layer * (width * height)" to get the starting index of the layer in the array.
        /// To get the index in the image we can use the formula "(y * width) + x".
        /// Combining these two formulas we can access any grid cell of any layer in the array like so "(layer * (width * height)) + ((y * width) + x)".</p>
        /// <p>This method does not perform range checking and will throw index out of range exceptions if invalid arguments are specified.</p></remarks>
        public static T Get3D<T>(this T[] array, int width, int height, int x, int y, int z)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            return array[(z * (width * height)) + ((y * width) + x)];
        }

        /// <summary>
        /// Sets the depth of the three dimensional sequence.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array whose value is to be retrieves.</param>
        /// <param name="width">The width of the three dimensional sequence.</param>
        /// <param name="height">The height of the three dimensional sequence.</param>
        /// <param name="value">The new depth of the three dimensional sequence.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// width or height is less then 1.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">'value' must be greater then 0.</exception>
        public static T[] Set3DDepth<T>(this T[] array, int width, int height, int value)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            if (value < 1)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            // resize the array preserving contents
            Array.Resize(ref array, value * (width * height));

            return array;
        }

        /// <summary>
        /// Inserts a source array into the three dimensional sequence.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array whose value is to be retrieves.</param>
        /// <param name="width">The width of the three dimensional sequence.</param>
        /// <param name="height">The height of the three dimensional sequence.</param>
        /// <param name="destinationArray">The destination array.</param>
        /// <param name="depth">The depth of the three dimensional sequence where insertion takes place.</param>
        /// <param name="sourceArray">The source array to be inserted.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// width or height is less then 1. Or depth is less then 0.
        /// </exception>
        /// <exception cref="ArgumentException">'sourceArray' must not be larger then '(width * height)'.</exception>
        /// <remarks>The length of the 'sourceArray' parameter cannot be larger then '(width * height)' otherwise the data it contains would overwrite any data
        /// stored at a deeper depth.</remarks>
        /// <returns>Returns the updated 'destinationArray'.</returns>
        public static T[] Insert3D<T>(this T[] destinationArray, int width, int height, int depth, T[] sourceArray)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            if (depth < 0)
            {
                throw new ArgumentOutOfRangeException("depth");
            }

            if (sourceArray == null || sourceArray.Length == 0)
            {
                return destinationArray;
            }

            // ensure source array is no greater then width * height
            if (sourceArray.Length > (width * height))
            {
                throw new ArgumentException("'sourceArray' must not be larger then '(width * height)'", "sourceArray");
            }

            if (sourceArray.Length < (width * height))
            {
                Array.Resize(ref sourceArray, width * height);
            }

            destinationArray = destinationArray.Insert(depth * (width * height), sourceArray);

            // return the updated array
            return destinationArray;
        }

        /// <summary>
        /// Gets the depth of the three dimensional sequence.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array whose value is to be retrieves.</param>
        /// <param name="width">The width of the three dimensional sequence.</param>
        /// <param name="height">The height of the three dimensional sequence.</param>
        /// <returns>Returns the depth of the three dimensional sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// width or height is less then 1.
        /// </exception>
        public static int Get3DDepth<T>(this T[] array, int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            return array.Length / (width * height);
        }

        /// <summary>
        /// Removes a depth from the three dimensional sequence.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array whose values are to be removed.</param>
        /// <param name="width">The width of the three dimensional sequence.</param>
        /// <param name="height">The height of the three dimensional sequence.</param>
        /// <param name="depth">The depth of to be removed from the three dimensional sequence.</param>
        /// <returns>Returns the updated three dimensional sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// width or height is less then 1. Or depth is less then 0 or greater then the depth of the three dimensional sequence.
        /// </exception>
        public static T[] Remove3DDepth<T>(this T[] array, int width, int height, int depth)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            var length = width * height;
            if (depth < 0 || depth > (array.Length / length))
            {
                throw new ArgumentOutOfRangeException("depth");
            }

            return array.RemoveRange(depth * length, length);
        }
    }
}