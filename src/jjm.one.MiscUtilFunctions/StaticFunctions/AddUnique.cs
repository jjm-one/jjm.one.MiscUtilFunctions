using System.Collections.Generic;

namespace jjm.one.MiscUtilFunctions
{
    /// <summary>
    /// A partial class containing multiple helper functions for <see cref="List{T}"/>.
    /// </summary>
    public static partial class ListHelper
    {
        /// <summary>
        /// Adds an object to a list, only if the list does not contain the object allready.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="obj">The object which should be added to the list.</param>
        /// <returns>True if the object was added to the list, else false.</returns>
        public static bool AddUnique<T>(this List<T> list, T obj)
        {
            if (!list.Contains(obj))
            {
                list.Add(obj);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
