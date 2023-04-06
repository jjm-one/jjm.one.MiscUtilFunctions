using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;
using System;

namespace jjm.one.MiscUtilFunctions.Extensions.CastHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for casting
    /// objects.
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

            if (input is TOut t)
            {
                output = t;
                return true;
            }

            if (typeof(string) == typeof(TIn) &&
                typeof(TOut).HasMethod("TryParse"))
            {
                var param = new object?[] { input?.ToString(), null };
                if (output != null && output.InvokeMethod<TOut, bool>
                        ("TryParse", ref param) && param is not null)
                {
                    var res = param[1];
                    if (res is not null)
                    {
                        output = (TOut)res;
                        return true;
                    }
                }
            }

            try
            {
                output = (TOut?)Convert.ChangeType(input, typeof(TOut));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
