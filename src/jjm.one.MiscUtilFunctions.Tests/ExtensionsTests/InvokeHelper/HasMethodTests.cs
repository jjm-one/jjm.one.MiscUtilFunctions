using jjm.one.MiscUtilFunctions.Extensions.InvokeHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.InvokeHelper
{
    /// <summary>
    /// This class contains unit-tests for the "HasMethod" functions.
    /// </summary>
    public class HasMethodTests
    {
        #region private util classes

        /// <summary>
        /// This is a private util class for the "InvokeMethod" tests.
        /// </summary>
        private class A
        {
            public static int M1()
            {
                return 42;
            }
        }

        #endregion

        #region tests

        /// <summary>
        /// 1. test for the "HasMethod" functions.
        /// </summary>
        [Fact]
        public void HasMethodTest1()
        {
            Assert.True(typeof(A).HasMethod("M1"));
            Assert.False(typeof(A).HasMethod("M2"));
        }

        /// <summary>
        /// 2. test for the "HasMethod" functions.
        /// </summary>
        [Fact]
        public void HasMethodTest2()
        {
            var a = new A();

            Assert.True(a.HasMethod("M1"));
            Assert.False(a.HasMethod("M2"));
        }

        #endregion
    }
}
