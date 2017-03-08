using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace FirstTest
{
    public class ExceptionTest
    {
        [Fact]
        public void TestException()
        {
            // 例外が発生することを期待
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                throw new ArgumentNullException();
            });

            
        }

    }
}
