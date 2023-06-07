using System;
using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.InvokeHelper
{
    /// <summary>
    /// This class contains unit-tests for the "InvokeMethod" functions.
    /// </summary>
    public class InvokeMethodTests
    {
        #region private util classes

        /// <summary>
        /// This is a private util class for the "InvokeMethod" tests.
        /// </summary>
        private class A
        {
            public static void M0()
            {
                throw new Exception();
            }

            public static int M1()
            {
                return 42;
            }

            public static int M2(int i)
            {
                return i;
            }

            public static int M3(int i1, int i2)
            {
                return i1 + i2;
            }

            public static int M3(int i, bool b)
            {
                if (i.Equals(0))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(i), "i can't be 0!");
                }

                return b ? i : -i;
            }

            public static bool M5(int input, out int output)
            {
                output = input;
                return true;
            }
        }

        #endregion

        #region private members

        private readonly A _instanceOfA;

        #endregion

        #region ctor

        /// <summary>
        /// Default constructor for the <see cref="InvokeMethodTests"/> class.
        /// </summary>
        public InvokeMethodTests()
        {
            _instanceOfA = new A();
        }

        #endregion

        #region tests

        /// <summary>
        /// 1. test for the "InvokeMethod" functions.
        /// </summary>
        [Fact]
        public void InvokeMethodTest0()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(new Action(() =>
                _instanceOfA.InvokeMethod<A>(nameof(_instanceOfA.M0))));
        }

        /// <summary>
        /// 2. test for the "InvokeMethod" functions.
        /// </summary>
        [Fact]
        public void InvokeMethodTest1()
        {
            Assert.Equal(42, _instanceOfA.InvokeMethod<A, int>(
                nameof(_instanceOfA.M1)));
        }

        /// <summary>
        /// 3. test for the "InvokeMethod" functions.
        /// </summary>
        [Fact]
        public void InvokeMethodTest2()
        {
            var p = new object?[] { 42 };
            Assert.Equal(42, _instanceOfA.InvokeMethod<A, int>(
                nameof(_instanceOfA.M2), ref p));
        }

        /// <summary>
        /// 4. test for the "InvokeMethod" functions.
        /// </summary>
        [Fact]
        public void InvokeMethodTest3()
        {
            var p1 = new object?[] { 40, 2 };
            Assert.Equal(42, _instanceOfA.InvokeMethod<A, int>(
                nameof(_instanceOfA.M3), ref p1));

            var p2 = new object?[] { 42, true };
            Assert.Equal(42, _instanceOfA.InvokeMethod<A, int>(
                nameof(_instanceOfA.M3), ref p2));
        }

        /// <summary>
        /// 5. test for the "InvokeMethod" functions.
        /// </summary>
        [Fact]
        public void InvokeMethodTest5()
        {
            var p = new object?[] { 42 };
            Assert.Equal(0, _instanceOfA.InvokeMethod<A, int>(
                nameof(_instanceOfA.M1), ref p));

            Assert.Equal(0, _instanceOfA.InvokeMethod<A, int>("SomeNotExistingFunction"));
        }

        #endregion
    }
}

