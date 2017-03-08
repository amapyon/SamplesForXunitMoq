using Xunit;

namespace FirstTest
{
    /*public*/ class FactTest
    {
        [Fact(DisplayName = "初めてのテスト",
            Skip = "スキップする理由")]
        public int TestFact()
        {
            Assert.Equal("a", "b");
            return 0;
        }
    }
}
