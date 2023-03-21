using System;
using System.Reflection;

namespace jjm.one.MiscUtilFunctions
{
	public static partial class InvokeHelper
	{
        public static bool HasMethod(this Type t, string methodName)
        {
            try
            {
                return t.GetMethod(methodName) is not null;
            }
            catch (AmbiguousMatchException)
            {
                return true;
            }
        }

        public static bool HasMethod(this object o, string methodName)
        {
            return o.GetType().HasMethod(methodName);
        }
    }
}

