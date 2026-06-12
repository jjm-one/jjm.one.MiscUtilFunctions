using jjm.one.MiscUtilFunctions.Extensions.CastHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.CastHelper
{
    /// <summary>
    /// Unit tests for <see cref="CastHelperExt.TryCast{TIn,TOut}"/>.
    /// </summary>
    public class TryCastTests
    {
        #region bool ↔ int

        [Fact]
        public void TryCast_BoolTrue_ToInt_Returns1()
        {
            const bool input = true;
            Assert.True(input.TryCast(out int result));
            Assert.Equal(1, result);
        }

        [Fact]
        public void TryCast_BoolFalse_ToInt_Returns0()
        {
            const bool input = false;
            Assert.True(input.TryCast(out int result));
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(2, true)]
        [InlineData(-1, true)]
        public void TryCast_Int_ToBool_ReturnsExpected(int input, bool expected)
        {
            Assert.True(input.TryCast(out bool result));
            Assert.Equal(expected, result);
        }

        #endregion

        #region same-type cast (pattern-match path)

        [Fact]
        public void TryCast_SameType_String_Succeeds()
        {
            const string input = "hello";
            Assert.True(input.TryCast(out string? result));
            Assert.Equal("hello", result);
        }

        [Fact]
        public void TryCast_SameType_Int_Succeeds()
        {
            const int input = 99;
            Assert.True(input.TryCast(out int result));
            Assert.Equal(99, result);
        }

        #endregion

        #region string → value type (TryParse path)

        [Theory]
        [InlineData("1234", 1234)]
        [InlineData("0", 0)]
        [InlineData("-42", -42)]
        public void TryCast_ValidStringInt_Succeeds(string input, int expected)
        {
            Assert.True(input.TryCast(out int result));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("True", true)]
        [InlineData("False", false)]
        public void TryCast_ValidStringBool_Succeeds(string input, bool expected)
        {
            Assert.True(input.TryCast(out bool result));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("3.14")]
        [InlineData("0.0")]
        [InlineData("-1.5")]
        public void TryCast_ValidStringDouble_Succeeds(string input)
        {
            Assert.True(input.TryCast(out double result));
            Assert.True(double.TryParse(input, out var expected));
            Assert.Equal(expected, result);
        }

        #endregion

        #region failing casts

        [Fact]
        public void TryCast_InvalidStringToInt_ReturnsFalse()
        {
            const string input = "not-a-number";
            Assert.False(input.TryCast(out int _));
        }

        [Fact]
        public void TryCast_InvalidStringToBool_ReturnsFalse()
        {
            const string input = "maybe";
            Assert.False(input.TryCast(out bool _));
        }

        [Fact]
        public void TryCast_ObjectToInt_Incompatible_ReturnsFalse()
        {
            object input = new object();
            Assert.False(input.TryCast(out int _));
        }

        #endregion

        #region Nullable<T> target

        [Fact]
        public void TryCast_IntToNullableInt_Succeeds()
        {
            const int input = 7;
            Assert.True(input.TryCast(out int? result));
            Assert.Equal(7, result);
        }

        [Fact]
        public void TryCast_StringToNullableInt_Succeeds()
        {
            const string input = "42";
            Assert.True(input.TryCast(out int? result));
            Assert.Equal(42, result);
        }

        #endregion

        #region numeric widening / narrowing (Convert path)

        [Fact]
        public void TryCast_IntToDouble_Succeeds()
        {
            const int input = 5;
            Assert.True(input.TryCast(out double result));
            Assert.Equal(5.0, result);
        }

        [Fact]
        public void TryCast_IntToLong_Succeeds()
        {
            const int input = 100;
            Assert.True(input.TryCast(out long result));
            Assert.Equal(100L, result);
        }

        #endregion
    }
}
