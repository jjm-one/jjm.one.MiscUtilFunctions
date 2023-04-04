using jjm.one.MiscUtilFunctions.Functions.InvokeHelper;
using System;

namespace jjm.one.MiscUtilFunctions.Extensions.InvokeHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for invoking functions.
    /// </summary>
	public static partial class InvokeHelperExt
	{
        #region non void function to invoke

        /// <summary>
        /// Invoke a  non void method on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="paramList">The method parmeters as an object array.</param>
        /// <returns>The result of the invoked method.</returns>
        public static Tout? InvokeMethod<Tinstance, Tout>(this Tinstance instance,
			string methodName, ref object?[]? paramList)
		{
            return InvokeHelperFkt.InvokeMethod<Tinstance, Tout>(instance, methodName, ref paramList);
        }

        /// <summary>
        /// Invoke a  non void method on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public static Tout? InvokeMethod<Tinstance, Tout>(this Tinstance instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            return InvokeHelperFkt.InvokeMethod<Tinstance, Tout>(instance, methodName, ref param);
        }

        #endregion

        #region void function to invoke

        /// <summary>
        /// Invoke a  void method on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="paramList">The method parmeters as an object array.</param>
        public static void InvokeMethod<Tinstance>(this Tinstance instance,
            string methodName, ref object?[]? paramList)
        {
            InvokeHelperFkt.InvokeMethod(instance, methodName, ref paramList);
        }

        /// <summary>
        /// Invoke a  void method on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        public static void InvokeMethod<Tinstance>(this Tinstance instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            InvokeHelperFkt.InvokeMethod(instance, methodName, ref param);
        }

        #endregion
    }
}

