using jjm.one.MiscUtilFunctions.Extensions.CastHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.CastHelper
{
    /// <summary>
    /// Unit tests for <see cref="CastHelperExt"/>.
    /// </summary>
    public class TryCastTests
    {
        #region bool ↔ int

        /// <summary>Casting <c>true</c> to <c>int</c> yields 1.</summary>
        [Fact]
        public void TryCast_BoolTrue_ToInt_Returns1()
        {
            const bool input = true;
            Assert.True(input.TryCast(out int result));
            Assert.Equal(1, result);
        }

        /// <summary>Casting <c>false</c> to <c>int</c> yields 0.</summary>
        [Fact]
        public void TryCast_BoolFalse_ToInt_Returns0()
        {
            const bool input = false;
            Assert.True(input.TryCast(out int result));
            Assert.Equal(0, result);
        }

        /// <summary>Casting various integers to <c>bool</c> yields the expected result.</summary>
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

        /// <summary>Casting a string to the same string type succeeds.</summary>
        [Fact]
        public void TryCast_SameType_String_Succeeds()
        {
            const string input = "hello";
            Assert.True(input.TryCast(out string? result));
            Assert.Equal("hello", result);
        }

        /// <summary>Casting an int to the same int type succeeds.</summary>
        [Fact]
        public void TryCast_SameType_Int_Succeeds()
        {
            const int input = 99;
            Assert.True(input.TryCast(out int result));
            Assert.Equal(99, result);
        }

        #endregion

        #region string → value type (TryParse path)

        /// <summary>Valid numeric strings are parsed to <c>int</c> via TryParse.</summary>
        [Theory]
        [InlineData("1234", 1234)]
        [InlineData("0", 0)]
        [InlineData("-42", -42)]
        public void TryCast_ValidStringInt_Succeeds(string input, int expected)
        {
            Assert.True(input.TryCast(out int result));
            Assert.Equal(expected, result);
        }

        /// <summary>Valid boolean strings are parsed to <c>bool</c> via TryParse.</summary>
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

        /// <summary>Valid floating-point strings are parsed to <c>double</c> via TryParse.</summary>
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

        /// <summary>A non-numeric string cast to <c>int</c> returns false.</summary>
        [Fact]
        public void TryCast_InvalidStringToInt_ReturnsFalse()
        {
            const string input = "not-a-number";
            Assert.False(input.TryCast(out int _));
        }

        /// <summary>A non-boolean string cast to <c>bool</c> returns false.</summary>
        [Fact]
        public void TryCast_InvalidStringToBool_ReturnsFalse()
        {
            const string input = "maybe";
            Assert.False(input.TryCast(out bool _));
        }

        /// <summary>An incompatible object cast to <c>int</c> returns false.</summary>
        [Fact]
        public void TryCast_ObjectToInt_Incompatible_ReturnsFalse()
        {
            object input = new object();
            Assert.False(input.TryCast(out int _));
        }

        #endregion

        #region Nullable<T> target

        /// <summary>Casting an int to <c>int?</c> succeeds.</summary>
        [Fact]
        public void TryCast_IntToNullableInt_Succeeds()
        {
            const int input = 7;
            Assert.True(input.TryCast(out int? result));
            Assert.Equal(7, result);
        }

        /// <summary>Casting a numeric string to <c>int?</c> succeeds.</summary>
        [Fact]
        public void TryCast_StringToNullableInt_Succeeds()
        {
            const string input = "42";
            Assert.True(input.TryCast(out int? result));
            Assert.Equal(42, result);
        }

        #endregion

        #region numeric widening / narrowing (Convert path)

        /// <summary>Casting <c>int</c> to <c>double</c> succeeds via Convert.</summary>
        [Fact]
        public void TryCast_IntToDouble_Succeeds()
        {
            const int input = 5;
            Assert.True(input.TryCast(out double result));
            Assert.Equal(5.0, result);
        }

        /// <summary>Casting <c>int</c> to <c>long</c> succeeds via Convert.</summary>
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
