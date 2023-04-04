using System;
using System.Reflection;

namespace jjm.one.MiscUtilFunctions.Extensions.InvokeHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for invoking
    /// functions and methods.
    /// </summary>
	public static partial class InvokeHelperExt
	{
        /// <summary>
        /// Check if a specific type of object has a specific method.
        /// </summary>
        /// <param name="type">The specific type to check.</param>
        /// <param name="methodName">The specific method name.</param>
        /// <returns>True on success, else false.</returns>
        public static bool HasMethod(this Type type, string methodName)
        {
            try
            {
                return type.GetMethod(methodName) is not null;
            }
            catch (AmbiguousMatchException)
            {
                return true;
            }
        }

        /// <summary>
        /// Check if a specific object has a specific method.
        /// </summary>
        /// <param name="obj">The specific object to check.</param>
        /// <param name="methodName">The specific method name.</param>
        /// <returns>True on success, else false.</returns>
        public static bool HasMethod(this object obj, string methodName)
        {
            return obj.GetType().HasMethod(methodName);
        }
    }
}

