using Xunit;
using Xunit.Abstractions;

namespace FirstTest
{
    public class StdOutTest
    {
        private ITestOutputHelper _output;

        // コンストラクターで出力先のストリームを受け取る
        public StdOutTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public void TestStdOutTheory(int v)
        {
            _output.WriteLine($"TestStdOutTheory {v}");
        }

    }
}
