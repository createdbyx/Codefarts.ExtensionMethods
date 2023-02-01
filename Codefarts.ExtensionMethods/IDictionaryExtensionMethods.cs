namespace System
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Provides extension methods for the IDictionary interface.
    /// </summary>
    public static class IDictionaryExtensionMethods
    {
        /// <summary>Gets the value associated with the specified key.</summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
        /// <param name="dictionary">The dictionary to retrieve the value from.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <param name="defaultValue">The default value to return if unable to fetch using key.</param>
        public static bool TryGetValue<T, V>(this IDictionary<T, V> dictionary, T key, out V value, V defaultValue)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            try
            {
                value = dictionary[key];
            }
            catch
            {
                value = defaultValue;
                return false;
            }

            return true;
        }

        /// <summary>Gets the value associated with the specified key and casts the value to the desired type.</summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
        /// <param name="dictionary">The dictionary to retrieve the value from.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        public static bool TryGetValueCast<T, V, C>(this IDictionary<T, V> dictionary, T name, out C value)
        {
            return TryGetValueCast(dictionary, name, out value, default(C));
        }

        /// <summary>Gets the value associated with the specified key and casts the value to the desired type.</summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
        /// <param name="dictionary">The dictionary to retrieve the value from.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <param name="defaultValue">The default value to return if unable to fetch using key.</param>
        public static bool TryGetValueCast<T, V, C>(this IDictionary<T, V> dictionary, T key, out C value, C defaultValue)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            try
            {
                value = (C)Convert.ChangeType(dictionary[key], typeof(C), CultureInfo.InvariantCulture);
            }
            catch
            {
                value = defaultValue;
                return false;
            }

            return true;
        }

        /// <summary>Gets the value associated with the specified key and casts the value to the desired type.</summary>
        /// <param name="dictionary">The dictionary to retrieve the value from.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value to return if unable to fetch using key.</param>
        /// <returns>The value stored in the dictionary if it exists otherwise returns the value specified in the <see cref="defaultValue"/> argument.</returns>
        public static C TryGetValueCast<T, V, C>(this IDictionary<T, V> dictionary, T key, C defaultValue)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            C value;
            try
            {
                value = (C)Convert.ChangeType(dictionary[key], typeof(C), CultureInfo.InvariantCulture);
            }
            catch
            {
                value = defaultValue;
            }

            return value;
        }
    }
}