 #pragma warning disable 1584,1711,1572,1581,1580
namespace System.Collections.Generic
{
    /// <summary>
    /// Provides extension methods for the <see cref="HashSet`1"/> collection.
    /// </summary>
    public static class HashSetExtensionMethods
    {
        /// <summary>
        /// Adds a range of values to a <see cref="HashSet`1"/> collection.
        /// </summary>
        /// <param name="set">
        /// The collection that the values will be added to.
        /// </param>
        /// <param name="items">
        /// The items to be added.
        /// </param>
        /// <typeparam name="T">
        /// THe type of items to be added.
        /// </typeparam>
        /// <returns>
        /// The <see cref="int"/> representing hte number of items successfully added, otherwise returns 0.
        /// </returns>
        public static int AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            var result = 0;
            foreach (var item in items)
            {
                if (!set.Add(item))
                {
                    return result;
                }

                result++;
            }

            return result;
        }
    }
}