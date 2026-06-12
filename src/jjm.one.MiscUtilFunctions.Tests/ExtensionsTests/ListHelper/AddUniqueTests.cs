using System;
using System.Collections.Generic;
using System.Linq;
using jjm.one.MiscUtilFunctions.Extensions.ListHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.ListHelper
{
    /// <summary>
    /// Unit tests for <see cref="ListHelperExt.AddUnique{T}"/>.
    /// </summary>
    public class AddUniqueTests
    {
        #region List<string>

        [Fact]
        public void AddUnique_NewItem_AddsAndReturnsTrue()
        {
            var list = new List<string> { "a" };
            Assert.True(list.AddUnique("b"));
            Assert.Equal("b", list.Last());
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void AddUnique_DuplicateItem_DoesNotAddReturnsFalse()
        {
            var list = new List<string> { "a" };
            Assert.False(list.AddUnique("a"));
            Assert.Single(list);
        }

        [Fact]
        public void AddUnique_EmptyList_AlwaysAdds()
        {
            var list = new List<string>();
            Assert.True(list.AddUnique("x"));
            Assert.Single(list);
            Assert.Equal("x", list[0]);
        }

        [Fact]
        public void AddUnique_MultipleUniqueItems_AllAdded()
        {
            var list = new List<string>();
            Assert.True(list.AddUnique("a"));
            Assert.True(list.AddUnique("b"));
            Assert.True(list.AddUnique("c"));
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void AddUnique_NullItem_TreatedAsValue()
        {
            var list = new List<string?> { null };
            Assert.False(list.AddUnique(null));   // already in list
            Assert.Single(list);

            var list2 = new List<string?> { "a" };
            Assert.True(list2.AddUnique(null));   // not yet in list
            Assert.Equal(2, list2.Count);
        }

        #endregion

        #region List<int>

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(42, false)]   // 42 is pre-populated
        public void AddUnique_IntList_ReturnsExpected(int value, bool expectedAdded)
        {
            var list = new List<int> { 42 };
            Assert.Equal(expectedAdded, list.AddUnique(value));
        }

        #endregion

        #region IList<T> (works through the interface)

        [Fact]
        public void AddUnique_IList_WorksThroughInterface()
        {
            IList<int> list = new List<int> { 1, 2 };
            Assert.True(list.AddUnique(3));
            Assert.False(list.AddUnique(1));
            Assert.Equal(3, list.Count);
        }

        #endregion

        #region custom IEqualityComparer<T>

        [Fact]
        public void AddUnique_WithComparer_CaseInsensitive_DetectsDuplicate()
        {
            var list = new List<string> { "Hello" };
            var added = list.AddUnique("hello", StringComparer.OrdinalIgnoreCase);
            Assert.False(added);   // "hello" == "Hello" case-insensitively
            Assert.Single(list);
        }

        [Fact]
        public void AddUnique_WithComparer_CaseSensitive_AddsDistinct()
        {
            var list = new List<string> { "Hello" };
            var added = list.AddUnique("hello", StringComparer.Ordinal);
            Assert.True(added);    // "hello" != "Hello" case-sensitively
            Assert.Equal(2, list.Count);
        }

        #endregion
    }
}
