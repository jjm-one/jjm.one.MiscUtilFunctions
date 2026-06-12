using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;
using System;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.InvokeHelper
{
    /// <summary>
    /// Unit tests for <see cref="InvokeHelperExt.HasMethod(Type,string)"/>
    /// and <see cref="InvokeHelperExt.HasMethod(object,string)"/>.
    /// </summary>
    public class HasMethodTests
    {
        #region helper types

        private class A
        {
            public static int M1() => 42;
        }

        private class B
        {
            public void Overloaded() { }
            public void Overloaded(int x) { _ = x; }
        }

        #endregion

        #region Type.HasMethod

        [Fact]
        public void HasMethod_OnType_ExistingMethod_ReturnsTrue()
        {
            Assert.True(typeof(A).HasMethod("M1"));
        }

        [Fact]
        public void HasMethod_OnType_NonExistingMethod_ReturnsFalse()
        {
            Assert.False(typeof(A).HasMethod("DoesNotExist"));
        }

        [Fact]
        public void HasMethod_OnType_NullName_ReturnsFalse()
        {
            Assert.False(typeof(A).HasMethod(null));
        }

        [Fact]
        public void HasMethod_OnType_EmptyName_ReturnsFalse()
        {
            Assert.False(typeof(A).HasMethod(string.Empty));
        }

        [Fact]
        public void HasMethod_OnType_InheritedMethod_ReturnsTrue()
        {
            // ToString, GetType, etc. are inherited from object
            Assert.True(typeof(A).HasMethod("ToString"));
            Assert.True(typeof(A).HasMethod("GetType"));
        }

        [Fact]
        public void HasMethod_OnType_AmbiguousOverloads_ReturnsTrue()
        {
            // GetMethod would throw AmbiguousMatchException; HasMethod must return true
            Assert.True(typeof(B).HasMethod("Overloaded"));
        }

        #endregion

        #region object.HasMethod

        [Fact]
        public void HasMethod_OnObject_ExistingMethod_ReturnsTrue()
        {
            var a = new A();
            Assert.True(a.HasMethod("M1"));
        }

        [Fact]
        public void HasMethod_OnObject_NonExistingMethod_ReturnsFalse()
        {
            var a = new A();
            Assert.False(a.HasMethod("M2"));
        }

        [Fact]
        public void HasMethod_OnObject_NullName_ReturnsFalse()
        {
            var a = new A();
            Assert.False(a.HasMethod(null));
        }

        [Fact]
        public void HasMethod_OnNullObject_ReturnsFalse()
        {
            object? obj = null;
            Assert.False(obj.HasMethod("ToString"));
        }

        #endregion
    }
}
