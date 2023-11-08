using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace jjm.one.MiscUtilFunctions.Extensions.FlagHelper;

public static partial class FlagHelperExt
{
    public static T AddFalg<T>(this T flag, T additionalFlag) where T : Enum
    {
        if (typeof(T).GetRuntimeProperties().Count(p => p.GetCustomAttributes<FlagsAttribute>(true).Any())
            <= 0)
        {
            throw new NotSupportedException(
                $"The type {typeof(T)} is not supported by this function! " + 
                "The type must have a attribute of the {typeof(FlagsAttribute)} type.");
        }

        return Operators.OrObject();
    }
}