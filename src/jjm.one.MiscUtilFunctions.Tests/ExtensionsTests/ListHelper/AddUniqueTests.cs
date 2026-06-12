using System;
using System.Collections.Generic;
using System.Linq;
using jjm.one.MiscUtilFunctions.Extensions.ListHelper;

namespace jjm.one.MiscUtilFunctions.Tests.ExtensionsTests.ListHelper
{
    /// <summary>
    /// Unit tests for the <see cref="ListHelperExt"/> AddUnique overloads.
    /// </summary>
    public class AddUniqueTests
    {
        #region List<string>

        /// <summary>Adding an item not yet in the list succeeds and returns true.</summary>
        [Fact]
        public void AddUnique_NewItem_AddsAndReturnsTrue()
        {
            var list = new List<string> { "a" };
            Assert.True(list.AddUnique("b"));
            Assert.Equal("b", list.Last());
            Assert.Equal(2, list.Count);
        }

        /// <summary>Adding a duplicate item does not change the list and returns false.</summary>
        [Fact]
        public void AddUnique_DuplicateItem_DoesNotAddReturnsFalse()
        {
            var list = new List<string> { "a" };
            Assert.False(list.AddUnique("a"));
            Assert.Single(list);
        }

        /// <summary>An empty list always accepts the first item.</summary>
        [Fact]
        public void AddUnique_EmptyList_AlwaysAdds()
        {
            var list = new List<string>();
            Assert.True(list.AddUnique("x"));
            Assert.Single(list);
            Assert.Equal("x", list[0]);
        }

        /// <summary>Multiple distinct items are all added successfully.</summary>
        [Fact]
        public void AddUnique_MultipleUniqueItems_AllAdded()
        {
            var list = new List<string>();
            Assert.True(list.AddUnique("a"));
            Assert.True(list.AddUnique("b"));
            Assert.True(list.AddUnique("c"));
            Assert.Equal(3, list.Count);
        }

        /// <summary>A null item is treated as a regular value: duplicate nulls are rejected, a first null is accepted.</summary>
        [Fact]
        public void AddUnique_NullItem_TreatedAsValue()
        {
            var list = new List<string?> { null };
            Assert.False(list.AddUnique(null));
            Assert.Single(list);

            var list2 = new List<string?> { "a" };
            Assert.True(list2.AddUnique(null));
            Assert.Equal(2, list2.Count);
        }

        #endregion

        #region List<int>

        /// <summary>Integer list correctly accepts new values and rejects duplicates.</summary>
        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(42, false)]
        public void AddUnique_IntList_ReturnsExpected(int value, bool expectedAdded)
        {
            var list = new List<int> { 42 };
            Assert.Equal(expectedAdded, list.AddUnique(value));
        }

        #endregion

        #region IList<T> (works through the interface)

        /// <summary>AddUnique works when the list is typed as <see cref="IList{T}"/>.</summary>
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

        /// <summary>Case-insensitive comparer treats differently-cased strings as duplicates.</summary>
        [Fact]
        public void AddUnique_WithComparer_CaseInsensitive_DetectsDuplicate()
        {
            var list = new List<string> { "Hello" };
            Assert.False(list.AddUnique("hello", StringComparer.OrdinalIgnoreCase));
            Assert.Single(list);
        }

        /// <summary>Case-sensitive comparer treats differently-cased strings as distinct.</summary>
        [Fact]
        public void AddUnique_WithComparer_CaseSensitive_AddsDistinct()
        {
            var list = new List<string> { "Hello" };
            Assert.True(list.AddUnique("hello", StringComparer.Ordinal));
            Assert.Equal(2, list.Count);
        }

        #endregion
    }
}
