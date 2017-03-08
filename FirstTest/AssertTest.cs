using Xunit;
using Xunit.Extensions;

namespace FirstTest
{
    public class AssertTest
    {
        [Fact(DisplayName = "標準的な表明メソッド")]
        public void TestAssert()
        {
            // 2つの値が等しいか、否か
            Assert.Equal(10, 0 + 10);
            Assert.Equal("10", "" + 10);
            Assert.NotEqual(0.3 + 0.6, 0.9);

            // 2つのオブジェクトが同一か、否か
            string s = "10";
            Assert.Same(s, "10");
            Assert.NotSame(s, "" + 10);

            // nullかどうか
            int? i = null;
            Assert.Null(i);
            i = 0;
            Assert.NotNull(i);

            // tureかfalseか
            Assert.True((0xFF00 ^ 0x00FF) == 0xFFFF);
            Assert.False((1 ^ 0) == -1);
        }

        [Fact(DisplayName = "文字列の表明メソッド")]
        public void TestStringAssert()
        {
            // 文字列を含むか、否か
            Assert.Contains("BC", "ABCDEFG");
            Assert.DoesNotContain("bc", "ABCDEFG");

            // 文字列が空かどうか
            Assert.Empty("");
            Assert.NotEmpty("*");

            // 指定した文字列で始まっているか
            Assert.StartsWith("AB", "ABCDEF");

            // 指定した文字列で終わっているか
            Assert.EndsWith("YZ", "UVWXYZ");

            // 正規表現を満たしているか、否か
            Assert.Matches(@"No\.[0-9]{3}$", "No.789");
            Assert.DoesNotMatch(@"No\.[0-9]{3}$", "No.7890");
        }


    }
        
}
