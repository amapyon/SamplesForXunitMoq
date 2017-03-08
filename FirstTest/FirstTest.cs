using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace FirstTest
{
    public class FirstTest : IDisposable
    {
        private ITestOutputHelper _output;

        public FirstTest(ITestOutputHelper output)
        {
            Debug.WriteLine("SetUp");
            _output = output;
        }

        [Fact]
        public void TestEqual()
        {
            Debug.WriteLine("TestEqual");
            _output.WriteLine("TestEqual");
            Assert.Equal("A", "A");
        }

        // TearDown
        public void Dispose()
        {
            Debug.WriteLine("TearDown");
        }

        [Theory]
        [InlineData(100, 200, 300)]
        [InlineData(200, 400, 600)]
        [InlineData(300, 600, 900)]
        public void TestTheory(int v1, int v2, int expected)
        {
            Debug.WriteLine("TestTheory{0},{1},{2}", v1, v2, expected);
            Assert.Equal(expected, v1 + v2);
        }


        class People
        {
            public People(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; set; }
            public int Age { get; set; }

            public override string ToString()
            {
                return "[Name=" + Name + ",Age=" + Age + "]";
            }

        }


        [Fact]
        public void TestToString()
        {
            var p = new People("amano", 20);
            _output.WriteLine(p.ToString());
        }

        [Fact]
        public void TestException()
        {
            Assert.ThrowsAsync<StackOverflowException>(() =>
            {
                throw new StackOverflowException();
            });
        }

        [Fact]
        public void TestExceptionHireachey()
        {
            // Exceptionのサブクラスが発生すればOK
            Assert.ThrowsAsync<Exception>(() =>
            {
                throw new StackOverflowException();
            });
        }

        [Fact]
        public void TestExceptionWithReturn()
        {
            Task t = Assert.ThrowsAsync<StackOverflowException>(() =>
            {
                throw new StackOverflowException("Message");
            });
            Assert.Null(t.Exception);
        }


    }

}
