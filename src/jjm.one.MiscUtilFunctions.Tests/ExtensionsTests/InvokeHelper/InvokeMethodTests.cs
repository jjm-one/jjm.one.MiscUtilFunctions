using System;
using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.InvokeHelper
{
    /// <summary>
    /// Unit tests for the <see cref="InvokeHelperExt"/> InvokeMethod extension overloads.
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

        /// <summary>Initialises the shared <see cref="A"/> instance used across tests.</summary>
        public InvokeMethodTests()
        {
            _a = new A();
        }

        #region void methods

        /// <summary>A void method that throws propagates the exception as <see cref="System.Reflection.TargetInvocationException"/>.</summary>
        [Fact]
        public void InvokeMethod_VoidThrows_PropagatesAsTargetInvocationException()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(
                () => _a.InvokeMethod<A>(nameof(A.M0)));
        }

        /// <summary>A no-parameter void method executes without throwing.</summary>
        [Fact]
        public void InvokeMethod_VoidNoParam_Succeeds()
        {
            _a.InvokeMethod<A>(nameof(A.M6));
        }

        /// <summary>Invoking a void method by a non-existent name is a no-op.</summary>
        [Fact]
        public void InvokeMethod_VoidWithParam_Succeeds()
        {
            _a.InvokeMethod<A>("MethodThatDoesNotExist");
        }

        #endregion

        #region non-void methods — no params

        /// <summary>A parameterless method returns its expected value.</summary>
        [Fact]
        public void InvokeMethod_NoParams_ReturnsCorrectValue()
        {
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M1)));
        }

        #endregion

        #region non-void methods — with params

        /// <summary>A single-param method echoes the supplied argument.</summary>
        [Fact]
        public void InvokeMethod_SingleParam_ReturnsEchoedValue()
        {
            var p = new object?[] { 42 };
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M2), ref p));
        }

        /// <summary>A two-param method returns the sum of its arguments.</summary>
        [Fact]
        public void InvokeMethod_TwoIntParams_ReturnsSum()
        {
            var p = new object?[] { 40, 2 };
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M3), ref p));
        }

        /// <summary>The overload resolution picks the (int, bool) overload and returns a positive value when <c>b=true</c>.</summary>
        [Fact]
        public void InvokeMethod_OverloadedMethod_IntBool_ReturnsPositive()
        {
            var p = new object?[] { 42, true };
            Assert.Equal(42, _a.InvokeMethod<A, int>(nameof(A.M3), ref p));
        }

        /// <summary>The overload resolution picks the (int, bool) overload and returns a negative value when <c>b=false</c>.</summary>
        [Fact]
        public void InvokeMethod_OverloadedMethod_IntBool_ReturnsNegative()
        {
            var p = new object?[] { 42, false };
            Assert.Equal(-42, _a.InvokeMethod<A, int>(nameof(A.M3), ref p));
        }

        #endregion

        #region no-match cases → default

        /// <summary>Passing the wrong number of parameters returns default.</summary>
        [Fact]
        public void InvokeMethod_WrongParamCount_ReturnsDefault()
        {
            var p = new object?[] { 42 };
            Assert.Equal(0, _a.InvokeMethod<A, int>(nameof(A.M1), ref p));
        }

        /// <summary>Invoking a non-existing method name returns default.</summary>
        [Fact]
        public void InvokeMethod_NonExistingMethod_ReturnsDefault()
        {
            Assert.Equal(0, _a.InvokeMethod<A, int>("SomeNonExistingMethod"));
        }

        /// <summary>Requesting a return type that does not match the method signature returns default.</summary>
        [Fact]
        public void InvokeMethod_WrongReturnType_ReturnsDefault()
        {
            Assert.Null(_a.InvokeMethod<A, string>(nameof(A.M1)));
        }

        #endregion

        #region null / polymorphism

        /// <summary>A null instance falls back to the declared type and can invoke static methods.</summary>
        [Fact]
        public void InvokeMethod_NullInstance_StaticMethod_ReturnsValue()
        {
            A? nullA = null;
            // null-forgiving: intentionally passing null to test the static-method fallback path
            Assert.Equal(42, nullA!.InvokeMethod<A, int>(nameof(A.M1)));
        }

        /// <summary>An interface-typed instance dispatches to its runtime type's implementation.</summary>
        [Fact]
        public void InvokeMethod_PolymorphicInstance_UsesRuntimeType()
        {
            IFoo foo = new FooImpl();
            Assert.Equal(99, foo.InvokeMethod<IFoo, int>(nameof(IFoo.Bar)));
        }

        #endregion

        #region instance methods

        /// <summary>An instance method with one parameter returns the correct value.</summary>
        [Fact]
        public void InvokeMethod_InstanceMethod_WithParam_ReturnsCorrectValue()
        {
            var p = new object?[] { 5 };
            Assert.Equal(10, _a.InvokeMethod<A, int>(nameof(A.M7), ref p));
        }

        #endregion
    }
}
