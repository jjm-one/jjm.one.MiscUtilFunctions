namespace jjm.one.MiscUtilFunctions.Tests.StaticFunctionsTests
{
    public class AddUniqueTests
    {
        /// <summary>
        /// Private string list for the tests.
        /// </summary>
        private readonly List<string> _list = new();
        
        /// <summary>
        /// Ctor of the <see cref="AddUniqueTests"/> class.
        /// </summary>
        public AddUniqueTests()
        {
            // add a default object to the list.
            _list.Add("a");
        }

        /// <summary>
        /// Test tries to add an unique object to a list.
        /// </summary>
        [Fact]
        public void CanAddTest()
        {
            Assert.True(_list.AddUnique("b"));
            Assert.Equal("b", _list.Last());
        }

        /// <summary>
        /// Test tries to add a non unique object to a list.
        /// </summary>
        [Fact]
        public void CanNotAddTest()
        {
            Assert.False(_list.AddUnique("a"));
            Assert.Equal("a", _list.Last());
        }

    }
}
