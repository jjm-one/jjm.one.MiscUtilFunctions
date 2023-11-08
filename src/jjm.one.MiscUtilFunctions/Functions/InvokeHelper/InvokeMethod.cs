using System;

namespace jjm.one.MiscUtilFunctions.Functions.InvokeHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for invoking
    /// functions and methods.
    /// </summary>
	public static class InvokeHelperFkt
	{
        #region non void function to invoke

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="TInstance">The type of the object.</typeparam>
        /// <typeparam name="TOut">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="paramList">The method parameters as an object array.</param>
        /// <returns>The result of the invoked method.</returns>
        public static TOut? InvokeMethod<TInstance, TOut>(TInstance? instance,
			string methodName, ref object?[]? paramList)
		{
			if (typeof(TInstance) != instance?.GetType())
			{
				return default;
			}

			var typeMethodInfoList = instance?.GetType().GetMethods();

            if (typeMethodInfoList is null)
            {
                return default;
            }

            foreach (var mI in typeMethodInfoList)
            {
                if (!mI.Name.Contains(methodName))
                {
                    continue;
                }

                if (mI.ReturnType != typeof(TOut))
                {
                    continue;
                }

                var methodParamInfos = mI.GetParameters();

                if (!methodParamInfos.Length.Equals(paramList?.Length))
                {
                    continue;
                }

                var mismatch = false;

                for (var i = 0; i < methodParamInfos.Length; i++)
                {
                    if (paramList[i] is null ||
                        methodParamInfos[i].ParameterType == paramList[i]?.GetType())
                    {
                        continue;
                    }

                    mismatch = true;
                    break;
                }

                if (mismatch)
                {
                    continue;
                }

                return (TOut?)mI.Invoke(instance, paramList);
            }

            return default;
        }

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="TInstance">The type of the object.</typeparam>
        /// <typeparam name="TOut">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public static TOut? InvokeMethod<TInstance, TOut>(TInstance? instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            return InvokeMethod<TInstance, TOut>(instance, methodName, ref param);
        }

        #endregion

        #region void function to invoke

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="TInstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="paramList">The method parameters as an object array.</param>
        public static void InvokeMethod<TInstance>(TInstance? instance,
            string methodName, ref object?[]? paramList)
        {
            if (typeof(TInstance) != instance?.GetType())
            {
                return;
            }

            var typeMethodInfoList = instance?.GetType().GetMethods();

            if (typeMethodInfoList is null)
            {
                return;
            }

            foreach (var mI in typeMethodInfoList)
            {
                if (!mI.Name.Contains(methodName))
                {
                    continue;
                }

                if (mI.ReturnType != typeof(void))
                {
                    continue;
                }

                var methodParamInfos = mI.GetParameters();

                if (!methodParamInfos.Length.Equals(paramList?.Length))
                {
                    continue;
                }

                var mismatch = false;

                for (var i = 0; i < methodParamInfos.Length; i++)
                {
                    if (paramList[i] is null ||
                        methodParamInfos[i].ParameterType == paramList[i]?.GetType())
                    {
                        continue;
                    }

                    mismatch = true;
                    break;
                }

                if (mismatch)
                {
                    continue;
                }

                mI.Invoke(instance, paramList);
            }
        }

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="TInstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        public static void InvokeMethod<TInstance>(TInstance? instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            InvokeMethod(instance, methodName, ref param);
        }

        #endregion
    }
}

