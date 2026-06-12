using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;
using System;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.InvokeHelper
{
    /// <summary>
    /// Unit tests for <see cref="InvokeHelperExt"/>.
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

        /// <summary>HasMethod returns true for an existing method on a type.</summary>
        [Fact]
        public void HasMethod_OnType_ExistingMethod_ReturnsTrue()
        {
            Assert.True(typeof(A).HasMethod("M1"));
        }

        /// <summary>HasMethod returns false for a method that does not exist on the type.</summary>
        [Fact]
        public void HasMethod_OnType_NonExistingMethod_ReturnsFalse()
        {
            Assert.False(typeof(A).HasMethod("DoesNotExist"));
        }

        /// <summary>HasMethod returns false when the method name is null.</summary>
        [Fact]
        public void HasMethod_OnType_NullName_ReturnsFalse()
        {
            Assert.False(typeof(A).HasMethod(null));
        }

        /// <summary>HasMethod returns false when the method name is an empty string.</summary>
        [Fact]
        public void HasMethod_OnType_EmptyName_ReturnsFalse()
        {
            Assert.False(typeof(A).HasMethod(string.Empty));
        }

        /// <summary>HasMethod returns true for methods inherited from <see cref="object"/>.</summary>
        [Fact]
        public void HasMethod_OnType_InheritedMethod_ReturnsTrue()
        {
            Assert.True(typeof(A).HasMethod("ToString"));
            Assert.True(typeof(A).HasMethod("GetType"));
        }

        /// <summary>HasMethod returns true when multiple overloads share the same name (AmbiguousMatchException path).</summary>
        [Fact]
        public void HasMethod_OnType_AmbiguousOverloads_ReturnsTrue()
        {
            Assert.True(typeof(B).HasMethod("Overloaded"));
        }

        #endregion

        #region object.HasMethod

        /// <summary>HasMethod returns true for an existing method called on an object instance.</summary>
        [Fact]
        public void HasMethod_OnObject_ExistingMethod_ReturnsTrue()
        {
            var a = new A();
            Assert.True(a.HasMethod("M1"));
        }

        /// <summary>HasMethod returns false for a non-existing method called on an object instance.</summary>
        [Fact]
        public void HasMethod_OnObject_NonExistingMethod_ReturnsFalse()
        {
            var a = new A();
            Assert.False(a.HasMethod("M2"));
        }

        /// <summary>HasMethod returns false when the method name is null on an object instance.</summary>
        [Fact]
        public void HasMethod_OnObject_NullName_ReturnsFalse()
        {
            var a = new A();
            Assert.False(a.HasMethod(null));
        }

        /// <summary>HasMethod returns false and does not throw when called on a null object.</summary>
        [Fact]
        public void HasMethod_OnNullObject_ReturnsFalse()
        {
            object? obj = null;
            Assert.False(obj.HasMethod("ToString"));
        }

        #endregion
    }
}
