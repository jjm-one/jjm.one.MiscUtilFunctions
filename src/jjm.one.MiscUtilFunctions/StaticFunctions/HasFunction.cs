using System;
using System.Reflection;

namespace jjm.one.MiscUtilFunctions
{
    /// <summary>
    /// A partial class containing multiple helper functions for invoking functions.
    /// </summary>
	public static partial class InvokeHelper
	{
        /// <summary>
        /// Check if a specific type of object has a specific member function.
        /// </summary>
        /// <param name="type">The specific type to check.</param>
        /// <param name="fctName">The specific function name.</param>
        /// <returns>True on success, esle false.</returns>
        public static bool HasFct(this Type type, string fctName)
        {
            try
            {
                return type.GetMethod(fctName) is not null;
            }
            catch (AmbiguousMatchException)
            {
                return true;
            }
        }

        /// <summary>
        /// Check if a specific object has a specific member function.
        /// </summary>
        /// <param name="obj">The specific object to check.</param>
        /// <param name="fctName"></param>
        /// <returns>True on success, esle false.</returns>
        public static bool HasFct(this object obj, string fctName)
        {
            return obj.GetType().HasFct(fctName);
        }
    }
}

