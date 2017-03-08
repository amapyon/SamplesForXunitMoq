using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace FirstTest
{
    public class TheoryTest
    {

        [Theory]
        [InlineData("AB", 1, "AB1")]
        [InlineData("ABC", 100, "ABC100")]
        public void TestTheory(string v1, int v2, string result)
        {
            Assert.Equal(result, v1 + v2);
        }
    }
}
