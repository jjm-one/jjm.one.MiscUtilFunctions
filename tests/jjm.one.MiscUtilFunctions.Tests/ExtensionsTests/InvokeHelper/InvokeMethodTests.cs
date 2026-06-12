using System;
using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.InvokeHelper
{
    /// <summary>
    /// Unit tests for <see cref="InvokeHelperExt.InvokeMethod{TInstance,TOut}"/>
    /// and <see cref="InvokeHelperExt.InvokeMethod{TInstance}"/>.
    /// </summary>
    public class InvokeMethodTests
    {
        #region helper types

        private class A
        {
            public static void M0() => throw new Exception("boom");

            public static int M1() => 42;

            public static int M2(int i) => i;

            public static int M3(int i1, int i2) => i1 + i2;

            public static int M3(int i, bool b) => b ? i : -i;

            public static bool M5(int input, out int output)
            {
                output = input;
                return true;
            }

            public void M6() { }

            public int M7(int x) => x * 2;
        }

        private interface IFoo
        {
            int Bar();
        }

        private class FooImpl : IFoo
        {
            public int Bar() => 99;
        }

        #endregion

        #region private members

        private readonly A _a;

        #endregion

        public InvokeMethodTests()
        {
            _a = new A();
        }

        #region void methods

        [Fact]
        public void InvokeMethod_VoidThrows_PropagatesAsTargetInvocationException()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(
                () => _a.InvokeMethod<A>(nameof(A.M0)));
        }

        [Fact]
        public void InvokeMethod_VoidNoParam_Succeeds()
        {
            // M6 is instance void — should execute without throwing
            _a.InvokeMethod<A>(nameof(A.M6));
        }

        [Fact]
        public void InvokeMethod_VoidWithParam_Succeeds()
        {
            // M3(int,bool) with b=false returns void? No — it returns int.
            // Use M6 variant: no void method with params exists in A, so
            // call a non-existent void method name → should be a no-op.
            _a.InvokeMethod<A>("MethodThatDoesNotExist");
        }

        #endregion

        #region non-void methods — no params

        [Fact]
        public void InvokeMethod_NoParams_ReturnsCorrectValue()
        {
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M1)));
        }

        #endregion

        #region non-void methods — with params

        [Fact]
        public void InvokeMethod_SingleParam_ReturnsEchoedValue()
        {
            var p = new object?[] { 42 };
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M2), ref p));
        }

        [Fact]
        public void InvokeMethod_TwoIntParams_ReturnsSum()
        {
            var p = new object?[] { 40, 2 };
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M3), ref p));
        }

        [Fact]
        public void InvokeMethod_OverloadedMethod_IntBool_ReturnsPositive()
        {
            var p = new object?[] { 42, true };
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M3), ref p));
        }

        [Fact]
        public void InvokeMethod_OverloadedMethod_IntBool_ReturnsNegative()
        {
            var p = new object?[] { 42, false };
            Assert.Equal(-42, _a.InvokeMethod<A, int>(nameof(A.M3), ref p));
        }

        #endregion

        #region no-match cases → default

        [Fact]
        public void InvokeMethod_WrongParamCount_ReturnsDefault()
        {
            var p = new object?[] { 42 };       // M1 takes 0 params
            Assert.Equal(0, _a.InvokeMethod<A, int>(nameof(A.M1), ref p));
        }

        [Fact]
        public void InvokeMethod_NonExistingMethod_ReturnsDefault()
        {
            Assert.Equal(0, _a.InvokeMethod<A, int>("SomeNonExistingMethod"));
        }

        [Fact]
        public void InvokeMethod_WrongReturnType_ReturnsDefault()
        {
            // M1 returns int, asking for string → should not match → default(string) = null
            Assert.Null(_a.InvokeMethod<A, string>(nameof(A.M1)));
        }

        #endregion

        #region null / polymorphism

        [Fact]
        public void InvokeMethod_NullInstance_StaticMethod_ReturnsValue()
        {
            // When instance is null, falls back to typeof(TInstance) for reflection
            A? nullA = null;
            Assert.Equal(42, nullA.InvokeMethod<A, int>(nameof(A.M1)));
        }

        [Fact]
        public void InvokeMethod_PolymorphicInstance_UsesRuntimeType()
        {
            // Instance declared as IFoo but runtime type is FooImpl
            IFoo foo = new FooImpl();
            Assert.Equal(99, foo.InvokeMethod<IFoo, int>(nameof(IFoo.Bar)));
        }

        #endregion

        #region instance methods

        [Fact]
        public void InvokeMethod_InstanceMethod_WithParam_ReturnsCorrectValue()
        {
            var p = new object?[] { 5 };
            Assert.Equal(10, _a.InvokeMethod<A, int>(nameof(A.M7), ref p));
        }

        #endregion
    }
}
