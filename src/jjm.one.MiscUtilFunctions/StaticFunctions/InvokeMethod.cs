using System.Reflection;

namespace jjm.one.MiscUtilFunctions
{
	public static partial class InvokeHerlper
	{
        #region Tout? return type

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

        public static Tout? InvokeMethod<Tinstance, Tout>(Tinstance? instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            return InvokeMethod<Tinstance, Tout>(instance, methodName, ref param);
        }


        public static Tout? ThisInvokeMethod<Tinstance, Tout>(this Tinstance instance,
			string methodName, ref object?[]? paramList)
		{
            return InvokeMethod<Tinstance, Tout>(instance, methodName, ref paramList);
        }

        public static Tout? ThisInvokeMethod<Tinstance, Tout>(this Tinstance instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            return InvokeMethod<Tinstance, Tout>(instance, methodName, ref param);
        }

        #endregion

        #region void return type

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

        public static void InvokeMethod<Tinstance>(Tinstance? instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            InvokeMethod<Tinstance>(instance, methodName, ref param);
        }


        public static void ThisInvokeMethod<Tinstance>(this Tinstance instance,
            string methodName, ref object?[]? paramList)
        {
            InvokeMethod<Tinstance>(instance, methodName, ref paramList);
        }

        public static void ThisInvokeMethod<Tinstance>(this Tinstance instance,
            string methodName)
        {
            var param = Array.Empty<object?>();
            InvokeMethod<Tinstance>(instance, methodName, ref param);
        }

        #endregion
    }
}

