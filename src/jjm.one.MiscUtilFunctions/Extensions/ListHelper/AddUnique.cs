using System.Collections.Generic;

namespace jjm.one.MiscUtilFunctions.Extensions.ListHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for <see cref="IList{T}"/>.
    /// </summary>
    public static partial class ListHelperExt
    {
        /// <summary>
        /// Adds an object to the list only if it is not already present,
        /// using the default equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="obj">The object to add.</param>
        /// <returns>True if the object was added, false if it was already present.</returns>
        public static bool AddUnique<T>(this IList<T> list, T obj)
        {
            return list.AddUnique(obj, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Adds an object to the list only if it is not already present,
        /// using the specified equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="obj">The object to add.</param>
        /// <param name="comparer">The equality comparer to use.</param>
        /// <returns>True if the object was added, false if it was already present.</returns>
        public static bool AddUnique<T>(this IList<T> list, T obj, IEqualityComparer<T> comparer)
        {
            foreach (var item in list)
            {
                if (comparer.Equals(item, obj))
                    return false;
            }

            list.Add(obj);
            return true;
        }
    }
}
