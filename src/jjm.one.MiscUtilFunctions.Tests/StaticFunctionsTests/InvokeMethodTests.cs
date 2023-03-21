using jjm.one.MiscUtilFunctions;

namespace jjm.one.MiscUtilFunctions.Tests.StaticFunctionsTests
{
    public class InvokeMethodTests
    {
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

        private A _instanceOfA;

        public InvokeMethodTests()
        {
            _instanceOfA = new A();
        }

        [Fact]
        public void InvokeMethodTest0()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(new Action(() =>
                _instanceOfA.ThisInvokeMethod<A>(nameof(_instanceOfA.M0))));
        }

        [Fact]
        public void InvokeMethodTest1()
        {
            Assert.Equal(42, _instanceOfA.ThisInvokeMethod<A,int>(
                nameof(_instanceOfA.M1)));
        }

        [Fact]
        public void InvokeMethodTest2()
        {
            var p = new object?[] { 42 };
            Assert.Equal(42, _instanceOfA.ThisInvokeMethod<A, int>(
                nameof(_instanceOfA.M2), ref p));
        }

        [Fact]
        public void InvokeMethodTest3()
        {
            var p1 = new object?[] { 40, 2 };
            Assert.Equal(42, _instanceOfA.ThisInvokeMethod<A, int>(
                nameof(_instanceOfA.M3), ref p1));

            var p2 = new object?[] { 42, true };
            Assert.Equal(42, _instanceOfA.ThisInvokeMethod<A, int>(
                nameof(_instanceOfA.M3), ref p2));
        }

        [Fact]
        public void InvokeMethodTest5()
        {
            var p = new object?[] { 42 };
            Assert.Equal(0, _instanceOfA.ThisInvokeMethod<A, int>(
                nameof(_instanceOfA.M1), ref p));

            Assert.Equal(0, _instanceOfA.ThisInvokeMethod<A, int>("SomeNotExistingFunction"));
        }
    }
}

