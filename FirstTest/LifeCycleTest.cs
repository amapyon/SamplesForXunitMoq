using System;
using Xunit;
using Xunit.Abstractions;

namespace FirstTest
{
    public class LifeCycleTest : IDisposable
    {
        private ITestOutputHelper _output;

        public LifeCycleTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("SetUp");
        }
        public void Dispose() => _output.WriteLine("TearDown");

        [Fact]public void TestMethod1() => _output.WriteLine("TestMethod1");
        [Fact]public void TestMethod2() => _output.WriteLine("TestMethod2");
        [Fact]public void TestMethod3() => _output.WriteLine("TestMethod3");
    }
}
