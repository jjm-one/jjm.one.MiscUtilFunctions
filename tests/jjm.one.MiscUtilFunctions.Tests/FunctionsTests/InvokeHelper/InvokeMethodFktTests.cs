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

        [Fact]
        public void InvokeMethod_StaticNoParam_ReturnsValue()
        {
            var calc = new Calc();
            var param = Array.Empty<object?>();
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Constant), ref param));
        }

        [Fact]
        public void InvokeMethod_StaticTwoParams_ReturnsSum()
        {
            var calc = new Calc();
            var param = new object?[] { 3, 4 };
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Add), ref param));
        }

        [Fact]
        public void InvokeMethod_InstanceMethod_ReturnsCorrectValue()
        {
            var calc = new Calc();
            var param = new object?[] { 6 };
            Assert.Equal(12, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Double), ref param));
        }

        [Fact]
        public void InvokeMethod_NoParamShorthand_ReturnsValue()
        {
            var calc = new Calc();
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Constant)));
        }

        [Fact]
        public void InvokeMethod_NonMatchingReturnType_ReturnsDefault()
        {
            var calc = new Calc();
            // Constant() returns int, requesting string → no match
            Assert.Null(InvokeHelperFkt.InvokeMethod<Calc, string>(calc, nameof(Calc.Constant)));
        }

        [Fact]
        public void InvokeMethod_WrongParamCount_ReturnsDefault()
        {
            var calc = new Calc();
            var param = new object?[] { 1 };    // Add needs 2 params
            Assert.Equal(0, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, nameof(Calc.Add), ref param));
        }

        [Fact]
        public void InvokeMethod_NonExistingMethod_ReturnsDefault()
        {
            var calc = new Calc();
            Assert.Equal(0, InvokeHelperFkt.InvokeMethod<Calc, int>(calc, "Ghost"));
        }

        [Fact]
        public void InvokeMethod_DifferentReturnType_StringMethod_ReturnsValue()
        {
            var calc = new Calc();
            var param = new object?[] { 42 };
            Assert.Equal("42", InvokeHelperFkt.InvokeMethod<Calc, string>(calc, nameof(Calc.Stringify), ref param));
        }

        #endregion

        #region non-void — null instance (static methods)

        [Fact]
        public void InvokeMethod_NullInstance_StaticMethod_ReturnsValue()
        {
            Calc? nullCalc = null;
            var param = Array.Empty<object?>();
            Assert.Equal(7, InvokeHelperFkt.InvokeMethod<Calc, int>(nullCalc, nameof(Calc.Constant), ref param));
        }

        #endregion

        #region non-void — polymorphism

        [Fact]
        public void InvokeMethod_InterfaceTypedInstance_UsesRuntimeType()
        {
            IShape shape = new Square(4);
            Assert.Equal(16, InvokeHelperFkt.InvokeMethod<IShape, int>(shape, nameof(IShape.Area)));
        }

        #endregion

        #region void overloads

        [Fact]
        public void InvokeMethod_Void_ThrowingMethod_PropagatesException()
        {
            var calc = new Calc();
            var param = Array.Empty<object?>();
            Assert.Throws<TargetInvocationException>(
                () => InvokeHelperFkt.InvokeMethod<Calc>(calc, nameof(Calc.Boom), ref param));
        }

        [Fact]
        public void InvokeMethod_Void_NonExistingMethod_IsNoOp()
        {
            var calc = new Calc();
            // Should not throw
            InvokeHelperFkt.InvokeMethod<Calc>(calc, "GhostVoid");
        }

        [Fact]
        public void InvokeMethod_VoidShorthand_NonExistingMethod_IsNoOp()
        {
            var calc = new Calc();
            InvokeHelperFkt.InvokeMethod<Calc>(calc, "GhostVoid");
        }

        [Fact]
        public void InvokeMethod_Void_NullInstance_StaticMethod_InvokesWithoutThrow()
        {
            Calc? nullCalc = null;
            var param = Array.Empty<object?>();
            // Boom is static, so null instance is fine for reflection
            Assert.Throws<TargetInvocationException>(
                () => InvokeHelperFkt.InvokeMethod<Calc>(nullCalc, nameof(Calc.Boom), ref param));
        }

        #endregion
    }
}
