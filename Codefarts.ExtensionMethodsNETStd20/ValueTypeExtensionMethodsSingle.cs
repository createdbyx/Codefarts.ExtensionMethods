// <copyright>
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>

namespace System
{
    using BaseType = System.Single;

    /// <summary>
    /// Provides extension methods for value types.
    /// </summary>
    public static partial class ValueTypeExtensionMethods
    {
        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static BaseType Clamp(this BaseType value, BaseType min, BaseType max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>Determines whether a value in within a certain range.</summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>True if the value is in range.</returns>
        public static bool IsInRange(this BaseType value, BaseType min, BaseType max)
        {
            return value >= min && value <= max;
        }

        /// <summary>Determines whether a value in within a certain range.</summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="throwException">if set to <c>true</c> will throw a <see cref="IndexOutOfRangeException"/> if the value is out of range.</param>
        /// <returns>True if the value is in range.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Is thrown if the value is out of range.</exception>
        public static bool IsInRange(this BaseType value, BaseType min, BaseType max, bool throwException)
        {
            if (!(value >= min && value <= max) && throwException)
            {
                throw new IndexOutOfRangeException();
            }

            return true;
        }

        /// <summary>Determines whether a value in within a certain range.</summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="throwException">if set to <c>true</c> will throw a <see cref="IndexOutOfRangeException"/> if the value is out of range.</param>
        /// <param name="message">The message for the <see cref="IndexOutOfRangeException"/> if it is thrown.</param>
        /// <returns>True if the value is in range.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Is thrown if the value is out of range.</exception>
        public static bool IsInRange(this BaseType value, BaseType min, BaseType max, bool throwException, string message)
        {
            if (!(value >= min && value <= max) && throwException)
            {
                throw new IndexOutOfRangeException(message);
            }

            return true;
        }
    }
}
