using System;

namespace jjm.one.MiscUtilFunctions
{
    /// <summary>
    /// A partial class containing multiple helper functions for invoking functions.
    /// </summary>
	public static partial class InvokeHelper
	{
        #region non void function to invoke

        /// <summary>
        /// Invoke a  non void function on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        /// <param name="paramList">The function parmeters as an object array.</param>
        /// <returns>The result of the invoked function.</returns>
        public static Tout? InvokeFct<Tinstance, Tout>(Tinstance? instance,
			string fctName, ref object?[]? paramList)
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
                if (mI.Name.Contains(fctName))
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
        /// Invoke a  non void function on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        /// <returns>The result of the invoked function.</returns>
        public static Tout? InvokeFct<Tinstance, Tout>(Tinstance? instance,
            string fctName)
        {
            var param = Array.Empty<object?>();
            return InvokeFct<Tinstance, Tout>(instance, fctName, ref param);
        }

        /// <summary>
        /// Invoke a  non void function on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        /// <param name="paramList">The function parmeters as an object array.</param>
        /// <returns>The result of the invoked function.</returns>
        public static Tout? ThisInvokeFct<Tinstance, Tout>(this Tinstance instance,
			string fctName, ref object?[]? paramList)
		{
            return InvokeFct<Tinstance, Tout>(instance, fctName, ref paramList);
        }

        /// <summary>
        /// Invoke a  non void function on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <typeparam name="Tout">The return type.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        /// <returns>The result of the invoked function.</returns>
        public static Tout? ThisInvokeFct<Tinstance, Tout>(this Tinstance instance,
            string fctName)
        {
            var param = Array.Empty<object?>();
            return InvokeFct<Tinstance, Tout>(instance, fctName, ref param);
        }

        #endregion

        #region void function to invoke

        /// <summary>
        /// Invoke a  non void function on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        /// <param name="paramList">The function parmeters as an object array.</param>
        public static void InvokeFct<Tinstance>(Tinstance? instance,
            string fctName, ref object?[]? paramList)
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
                if (mI.Name.Contains(fctName))
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
        /// Invoke a  non void function on an object.
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        public static void InvokeFct<Tinstance>(Tinstance? instance,
            string fctName)
        {
            var param = Array.Empty<object?>();
            InvokeFct<Tinstance>(instance, fctName, ref param);
        }

        /// <summary>
        /// Invoke a  void function on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        /// <param name="paramList">The function parmeters as an object array.</param>
        public static void ThisInvokeFct<Tinstance>(this Tinstance instance,
            string fctName, ref object?[]? paramList)
        {
            InvokeFct<Tinstance>(instance, fctName, ref paramList);
        }

        /// <summary>
        /// Invoke a  void function on an object (as extension).
        /// </summary>
        /// <typeparam name="Tinstance">The type of the object.</typeparam>
        /// <param name="instance">The instance if the object.</param>
        /// <param name="fctName">The name of the function.</param>
        public static void ThisInvokeFct<Tinstance>(this Tinstance instance,
            string fctName)
        {
            var param = Array.Empty<object?>();
            InvokeFct<Tinstance>(instance, fctName, ref param);
        }

        #endregion
    }
}

