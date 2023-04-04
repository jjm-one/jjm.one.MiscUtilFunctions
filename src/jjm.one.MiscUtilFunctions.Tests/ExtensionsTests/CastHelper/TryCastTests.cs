using jjm.one.MiscUtilFunctions.Extensions.CastHelper;

namespace jjm.one.MiscUtilFunctions.Tests.StaticFunctionsTests
{
	public class TryCastTests
	{
		[Fact]
		public void TryCastBoolInt1()
        {
			bool input = true;

            bool resState = input.TryCast(out int resVal);

            Assert.True(input);

			Assert.Equal(1, resVal);
			Assert.True(resState);
		}

        [Fact]
        public void TryCastBoolInt2()
        {
            bool input = false;

            bool resState = input.TryCast(out int resVal);

            Assert.False(input);

            Assert.Equal(0, resVal);
            Assert.True(resState);
        }


        [Fact]
        public void TryCastIntBool1()
        {
            int input = 1;

            bool resState = input.TryCast(out bool resVal);

            Assert.Equal(1, input);

            Assert.True(resVal);
            Assert.True(resState);
        }

        [Fact]
        public void TryCastIntBool2()
        {
            int input = 0;

            bool resState = input.TryCast(out bool resVal);

            Assert.Equal(0, input);

            Assert.False(resVal);
            Assert.True(resState);
        }

        [Fact]
        public void TryCastIntBool3()
        {
            int input = 2;

            bool resState = input.TryCast(out bool resVal);

            Assert.Equal(2, input);

            Assert.True(resVal);
            Assert.True(resState);
        }

        [Fact]
        public void TryCastStringInt()
        {
            string input = "1234";

            bool resState = input.TryCast(out int resVal);

            Assert.Equal("1234", input);

            Assert.Equal(1234, resVal);
            Assert.True(resState);
        }
    }
}

