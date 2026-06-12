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
        /// Check if a specific type has a public method with the given name.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="methodName">The method name to look for.</param>
        /// <returns>True if the method exists, false otherwise.</returns>
        public static bool HasMethod(this Type type, string? methodName)
        {
            if (string.IsNullOrEmpty(methodName))
                return false;

            try
            {
                return type.GetMethod(methodName) is not null;
            }
            catch (AmbiguousMatchException)
            {
                // Multiple overloads with the same name → method exists
                return true;
            }
        }

        /// <summary>
        /// Check if a specific object's runtime type has a public method with the given name.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="methodName">The method name to look for.</param>
        /// <returns>True if the method exists, false otherwise.</returns>
        public static bool HasMethod(this object? obj, string? methodName)
        {
            if (obj is null)
                return false;

            return obj.GetType().HasMethod(methodName);
        }
    }
}
