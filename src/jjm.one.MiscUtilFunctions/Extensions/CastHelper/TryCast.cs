using System;
using System.Reflection;

namespace jjm.one.MiscUtilFunctions.Extensions.CastHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for casting objects.
    /// </summary>
    public static partial class CastHelperExt
    {
        /// <summary>
        /// Try to cast an object into an object of a specific type.
        /// </summary>
        /// <typeparam name="TIn">The type of the input object.</typeparam>
        /// <typeparam name="TOut">The type to cast to.</typeparam>
        /// <param name="input">The object to cast.</param>
        /// <param name="output">The result of the cast.</param>
        /// <returns>True on success, else false.</returns>
        public static bool TryCast<TIn, TOut>(this TIn input, out TOut? output)
        {
            output = default;

            // Direct cast via pattern matching (handles same-type and inheritance)
            if (input is TOut t)
            {
                output = t;
                return true;
            }

            // String → TOut: try the static TryParse(string, out TOut) if available
            if (typeof(TIn) == typeof(string))
            {
                var targetType = Nullable.GetUnderlyingType(typeof(TOut)) ?? typeof(TOut);
                var tryParseMethod = targetType.GetMethod(
                    "TryParse",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(string), targetType.MakeByRefType() },
                    null);

                if (tryParseMethod != null)
                {
                    var args = new object?[] { input?.ToString(), null };
                    if (tryParseMethod.Invoke(null, args) is true)
                    {
                        output = (TOut?)args[1];
                        return true;
                    }
                    // Input was a valid string but TryParse rejected it — no point trying Convert
                    return false;
                }
            }

            // Fallback: Convert.ChangeType handles numeric widening/narrowing, bool↔int, etc.
            try
            {
                var targetType = Nullable.GetUnderlyingType(typeof(TOut)) ?? typeof(TOut);
                output = (TOut?)Convert.ChangeType(input, targetType);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
