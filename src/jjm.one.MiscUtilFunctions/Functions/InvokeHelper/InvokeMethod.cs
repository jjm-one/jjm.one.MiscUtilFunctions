using System;

namespace jjm.one.MiscUtilFunctions.Functions.InvokeHelper
{
    /// <summary>
    /// A partial class containing multiple helper functions for invoking
    /// functions and methods.
    /// </summary>
	public static partial class InvokeHelperFkt
	{
        #region non void function to invoke

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="paramList">The method parmeters as an object array.</param>
        /// <returns>The result of the invoked method.</returns>
        public static Tout? InvokeMethod<Tinstance, Tout>(Tinstance? instance,
			string methodName, ref object?[]? paramList)
		{
			if (!typeof(Tinstance).Equals(instance?.GetType()))
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
                if (mI.Name.Contains(methodName))
                {
                    if (!mI.ReturnType.Equals(typeof(Tout)))
                    {
                        continue;
                    }

                    var methodParamInfos = mI.GetParameters();

                    if (!methodParamInfos.Length.Equals(paramList?.Length))
                    {
                        continue;
                    }

                    var missmatch = false;

                    for (var i = 0; i < methodParamInfos.Length; i++)
                    {
                        if (paramList[i] is not null &&
                            !methodParamInfos[i].ParameterType.Equals(
                            paramList[i]?.GetType()))
                        {
                            missmatch = true;
                            break;
                        }
                    }

                    if (missmatch)
                    {
                        continue;
                    }

                    return (Tout?)mI.Invoke(instance, paramList);
                }
            }

            return default;
        }

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The result of the invoked method.</returns>
        public static Tout? InvokeMethod<Tinstance, Tout>(Tinstance? instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            return InvokeMethod<Tinstance, Tout>(instance, methodName, ref param);
        }

        #endregion

        #region void function to invoke

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="paramList">The method parmeters as an object array.</param>
        public static void InvokeMethod<Tinstance>(Tinstance? instance,
            string methodName, ref object?[]? paramList)
        {
            if (!typeof(Tinstance).Equals(instance?.GetType()))
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
                if (mI.Name.Contains(methodName))
                {
                    if (!mI.ReturnType.Equals(typeof(void)))
                    {
                        continue;
                    }

                    var methodParamInfos = mI.GetParameters();

                    if (!methodParamInfos.Length.Equals(paramList?.Length))
                    {
                        continue;
                    }

                    var missmatch = false;

                    for (var i = 0; i < methodParamInfos.Length; i++)
                    {
                        if (paramList[i] is not null &&
                            !methodParamInfos[i].ParameterType.Equals(
                            paramList[i]?.GetType()))
                        {
                            missmatch = true;
                            break;
                        }
                    }

                    if (missmatch)
                    {
                        continue;
                    }

                    mI.Invoke(instance, paramList);
                }
            }
        }

        /// <summary>
        /// Invoke a  non void method on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="methodName">The name of the method.</param>
        public static void InvokeMethod<Tinstance>(Tinstance? instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            InvokeMethod(instance, methodName, ref param);
        }

        #endregion
    }
}

