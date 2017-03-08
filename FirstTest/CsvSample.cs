﻿using Xunit;
using Xunit.Abstractions;

namespace FirstTest
{
    public class CsvSample
    {
        private ITestOutputHelper _output;

        public CsvSample(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [CsvData("../../TestData.csv", false)]
        public void TestUseCsvFile(int v1, int v2, int result)
        {
            _output.WriteLine(string.Format($"{v1},{v2},{result}"));
            Assert.Equal(result, v1 + v2);
        }
    }
}
