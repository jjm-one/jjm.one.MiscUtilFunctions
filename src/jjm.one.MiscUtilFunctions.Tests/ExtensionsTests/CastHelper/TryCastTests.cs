using jjm.one.MiscUtilFunctions.Extensions.CastHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.CastHelper
{
    /// <summary>
    /// This class contains unit test for the CastHelper functions.
    /// </summary>
	public class TryCastTests
	{
		/// <summary>
		/// Test <see cref="CastHelperExt.TryCast{T1,T2}"/> boot to int (successful) 1 .
		/// </summary>
		[Fact]
		public void TryCastBoolInt1()
        {
			const bool input = true;

            var resState = input.TryCast(out int resVal);

            Assert.True(input);

			Assert.Equal(1, resVal);
			Assert.True(resState);
		}

        /// <summary>
        /// Test <see cref="CastHelperExt.TryCast{T1,T2}"/> boot to int (successful) 2.
        /// </summary>
        [Fact]
        public void TryCastBoolInt2()
        {
            const bool input = false;

            var resState = input.TryCast(out int resVal);

            Assert.False(input);

            Assert.Equal(0, resVal);
            Assert.True(resState);
        }


        /// <summary>
        /// Test <see cref="CastHelperExt.TryCast{T1,T2}"/> int to bool (successful) 1.
        /// </summary>
        [Fact]
        public void TryCastIntBool1()
        {
            const int input = 1;

            var resState = input.TryCast(out bool resVal);

            Assert.Equal(1, input);

            Assert.True(resVal);
            Assert.True(resState);
        }

        /// <summary>
        /// Test <see cref="CastHelperExt.TryCast{T1,T2}"/> int to bool (successful) 2.
        /// </summary>
        [Fact]
        public void TryCastIntBool2()
        {
            const int input = 0;

            var resState = input.TryCast(out bool resVal);

            Assert.Equal(0, input);

            Assert.False(resVal);
            Assert.True(resState);
        }

        /// <summary>
        /// Test <see cref="CastHelperExt.TryCast{T1,T2}"/> int to bool (successful) 3.
        /// </summary>
        [Fact]
        public void TryCastIntBool3()
        {
            const int input = 2;

            var resState = input.TryCast(out bool resVal);

            Assert.Equal(2, input);

            Assert.True(resVal);
            Assert.True(resState);
        }

        /// <summary>
        /// Test <see cref="CastHelperExt.TryCast{T1,T2}"/> string to int (successful) 1.
        /// </summary>
        [Fact]
        public void TryCastStringInt1()
        {
            const string input = "1234";

            var resState = input.TryCast(out int resVal);

            Assert.Equal("1234", input);

            Assert.Equal(1234, resVal);
            Assert.True(resState);
        }
    }
}

