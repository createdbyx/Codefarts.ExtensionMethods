/*
Source: http://hugoware.net/blog/enumeration-extensions-2-0
*/
namespace System
{
    using System.ComponentModel;

    /// <summary>
    /// Extension methods to make working with Enum values easier
    /// </summary>
    public static class EnumerationExtensions
    {
        #region Extension Methods

#if !PORTABLE && !WINDOWS_UWP
        /// <summary>
        /// Gets the description attribute value for a enum.
        /// </summary>
        /// <param name="value">The value to get the description for.</param>
        /// <returns>The description value.</returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        } 
#endif

        /// <summary>
        /// Includes an enumerated type and returns the new value
        /// </summary>
        public static T Include<T>(this Enum value, T append)
        {
            var type = value.GetType();

            // determine the values
            object result = value;
            var parsed = new EnumValueHelper(append, type);
            if (parsed.Signed is long)
            {
                result = Convert.ToInt64(value) | (long)parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(value) | (ulong)parsed.Unsigned;
            }

            // return the final value
            return (T)Enum.Parse(type, result.ToString(), true);
        }

        /// <summary>
        /// Removes an enumerated type and returns the new value
        /// </summary>
        public static T Remove<T>(this Enum value, T remove)
        {
            var type = value.GetType();

            // determine the values
            object result = value;
            var parsed = new EnumValueHelper(remove, type);
            if (parsed.Signed is long)
            {
                result = Convert.ToInt64(value) & ~(long)parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(value) & ~(ulong)parsed.Unsigned;
            }

            // return the final value
            return (T)Enum.Parse(type, result.ToString(), true);
        }

        /// <summary>
        /// Checks if an enumerated type contains a value
        /// </summary>
        public static bool Has<T>(this Enum value, T check)
        {
            var type = value.GetType();

            // determine the values
            object result = value;
            var parsed = new EnumValueHelper(check, type);
            if (parsed.Signed is long)
            {
                return (Convert.ToInt64(value) & (long)parsed.Signed) == parsed.Signed;
            }

            if (parsed.Unsigned is ulong)
            {
                return (Convert.ToUInt64(value) &
                        (ulong)parsed.Unsigned) == parsed.Unsigned;
            }

            return false;
        }

        /// <summary>
        /// Checks if an enumerated type is missing a value
        /// </summary>
        public static bool Missing<T>(this Enum obj, T value)
        {
            return !Has<T>(obj, value);
        }

        /// <summary>
        /// Determines whether an enum has been marked Obsolete.
        /// </summary>
        /// <param name="value">The enum value to check against.</param>
        /// <returns>
        ///   <c>true</c> if marked obsolete; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEnumObsolete(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (ObsoleteAttribute[])fi.GetCustomAttributes(typeof(ObsoleteAttribute), false);
            return attributes.Length > 0;
        }

        #endregion

        #region Helper Classes

        // class to simplify narrowing values between 
        // a ulong and long since either value should
        // cover any lesser value
        private class EnumValueHelper
        {
            // cached comparisons for tye to use
            private static Type _UInt64 = typeof(ulong);
            private static Type _UInt32 = typeof(long);

            public long? Signed;
            public ulong? Unsigned;

            public EnumValueHelper(object value, Type type)
            {

                // make sure it is even an enum to work with
                if (!type.IsEnum)
                {
                    throw new ArgumentException("Value provided is not an enumerated type!");
                }

                // then check for the enumerated value
                var compare = Enum.GetUnderlyingType(type);

                // if this is an unsigned long then the only
                // value that can hold it would be a ulong
                if (compare.Equals(_UInt32) || compare.Equals(_UInt64))
                {
                    this.Unsigned = Convert.ToUInt64(value);
                }
                else
                {
                    // otherwise, a long should cover anything else
                    this.Signed = Convert.ToInt64(value);
                }
            }
        }

        #endregion   
    }
}