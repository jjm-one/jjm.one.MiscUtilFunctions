namespace jjm.one.MiscUtilFunctions
{
    public static partial class CastHelper
    {
        /// <summary>
        /// Try to cast an object into an object of a specific type.
        /// </summary>
        /// <typeparam name="Tin">The type of the input object.</typeparam>
        /// <typeparam name="Tout">The type to cast to.</typeparam>
        /// <param name="input">The object to cast.</param>
        /// <param name="output">The result of the cast.</param>
        /// <returns>True on success, else false.</returns>
        ///*
        public static bool TryCast<Tin, Tout>(this Tin input, out Tout? output) where Tout : new()
        {
            output = default;

            if (input is Tout t)
            {
                output = t;
                return true;
            }

            if (typeof(string).Equals(typeof(Tin)) && typeof(Tout).HasMethod("TryParse"))
            {
                var param = new object?[] { input?.ToString(), null };
                if (InvokeMethodClass.InvokeMethod<Tout, bool>(output, "TryParse", ref param) && param is not null)
                {
                    var res = param[1];
                    if (res is not null)
                    {
                        output = (Tout)res;
                        return true;
                    }
                }
            }

            try
            {
                output = (Tout?)Convert.ChangeType(input, typeof(Tout));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
