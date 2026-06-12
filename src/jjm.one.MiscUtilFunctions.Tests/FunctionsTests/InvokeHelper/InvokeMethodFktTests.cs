using System;
using System.Reflection;
using jjm.one.MiscUtilFunctions.Functions.InvokeHelper;

namespace jjm.one.MiscUtilFunctions.Tests.FunctionsTests.InvokeHelper
{
    /// <summary>
    /// Unit tests for <see cref="InvokeHelperFkt"/> — the underlying static helper
    /// that the extension methods delegate to.
    /// </summary>
    public class InvokeMethodFktTests
    {
        #region helper types

        private class Calc
        {
            public static int Add(int a, int b) => a + b;
            public static int Constant() => 7;
            public static void Boom() => throw new InvalidOperationException("test");
            public int Double(int x) => x * 2;
            public static string Stringify(int n) => n.ToString();
        }

        private interface IShape
        {
            int Area();
        }

        private class Square : IShape
        {
            private readonly int _side;
            public Square(int side) { _side = side; }
            public int Area() => _side * _side;
        }

        #endregion

        #region non-void — basic

        /// <summary>A parameterless static method returns its expected value.</summary>
        [Fact]
        public void InvokeMethod_StaticNoParam_ReturnsValue()
        {
            var calc = new Calc();
            var param = Array.Empty<object?>();
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Constant), ref param));
        }

        /// <summary>A static method with two parameters returns their sum.</summary>
        [Fact]
        public void InvokeMethod_StaticTwoParams_ReturnsSum()
        {
            var calc = new Calc();
            var param = new object?[] { 3, 4 };
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Add), ref param));
        }

        /// <summary>An instance method with one parameter returns the correct computed value.</summary>
        [Fact]
        public void InvokeMethod_InstanceMethod_ReturnsCorrectValue()
        {
            var calc = new Calc();
            var param = new object?[] { 6 };
            Assert.Equal(12, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Double), ref param));
        }

        /// <summary>The no-param shorthand overload also resolves the method correctly.</summary>
        [Fact]
        public void InvokeMethod_NoParamShorthand_ReturnsValue()
        {
            var calc = new Calc();
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Constant)));
        }

        /// <summary>Requesting a return type that does not match any overload returns default.</summary>
        [Fact]
        public void InvokeMethod_NonMatchingReturnType_ReturnsDefault()
        {
            var calc = new Calc();
            Assert.Null(InvokeHelperFkt.InvokeMethod<Calc, string>(calc, nameof(Calc.Constant)));
        }

        /// <summary>Passing fewer parameters than required returns default without throwing.</summary>
        [Fact]
        public void InvokeMethod_WrongParamCount_ReturnsDefault()
        {
            var calc = new Calc();
            var param = new object?[] { 1 };
            Assert.Equal(0, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Add), ref param));
        }

        /// <summary>Invoking a non-existing method name returns default.</summary>
        [Fact]
        public void InvokeMethod_NonExistingMethod_ReturnsDefault()
        {
            var calc = new Calc();
            Assert.Equal(0, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, "Ghost"));
        }

        /// <summary>A static method with a string return type returns the correctly formatted value.</summary>
        [Fact]
        public void InvokeMethod_DifferentReturnType_StringMethod_ReturnsValue()
        {
            var calc = new Calc();
            var param = new object?[] { 42 };
            Assert.Equal("42", InvokeHelperFkt.InvokeMethod<Calc, string>(calc, nameof(Calc.Stringify), ref param));
        }

        #endregion

        #region non-void — null instance (static methods)

        /// <summary>A null instance falls back to <c>typeof(TInstance)</c>, allowing static methods to be resolved.</summary>
        [Fact]
        public void InvokeMethod_NullInstance_StaticMethod_ReturnsValue()
        {
            Calc? nullCalc = null;
            var param = Array.Empty<object?>();
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(nullCalc, nameof(Calc.Constant), ref param));
        }

        #endregion

        #region non-void — polymorphism

        /// <summary>An interface-typed instance dispatches via its runtime type, not the declared interface.</summary>
        [Fact]
        public void InvokeMethod_InterfaceTypedInstance_UsesRuntimeType()
        {
            IShape shape = new Square(4);
            Assert.Equal(16, InvokeHelperFkt.InvokeMethod<IShape, int>(shape, nameof(IShape.Area)));
        }

        #endregion

        #region void overloads

        /// <summary>A void method that throws propagates the exception as <see cref="TargetInvocationException"/>.</summary>
        [Fact]
        public void InvokeMethod_Void_ThrowingMethod_PropagatesException()
        {
            var calc = new Calc();
            var param = Array.Empty<object?>();
            Assert.Throws<TargetInvocationException>(
                () => InvokeHelperFkt.InvokeMethod<Calc>(calc, nameof(Calc.Boom), ref param));
        }

        /// <summary>Invoking a void method by a non-existing name is a no-op.</summary>
        [Fact]
        public void InvokeMethod_Void_NonExistingMethod_IsNoOp()
        {
            var calc = new Calc();
            InvokeHelperFkt.InvokeMethod<Calc>(calc, "GhostVoid");
        }

        /// <summary>The void shorthand overload with a non-existing name is also a no-op.</summary>
        [Fact]
        public void InvokeMethod_VoidShorthand_NonExistingMethod_IsNoOp()
        {
            var calc = new Calc();
            InvokeHelperFkt.InvokeMethod<Calc>(calc, "GhostVoid");
        }

        /// <summary>A null instance can still invoke a static void method (which throws), confirming the static-fallback path.</summary>
        [Fact]
        public void InvokeMethod_Void_NullInstance_StaticMethod_InvokesWithoutThrow()
        {
            Calc? nullCalc = null;
            var param = Array.Empty<object?>();
            Assert.Throws<TargetInvocationException>(
                () => InvokeHelperFkt.InvokeMethod<Calc>(nullCalc, nameof(Calc.Boom), ref param));
        }

        #endregion
    }
}
