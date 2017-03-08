using Moq;
using Xunit;

namespace MoqSample
{
    public class MoqStubSample
    {
        [Fact(DisplayName = "引数に値を指定してスタブ化")]
        public void TestStubExactMatch()
        {
            var calcMock = new Mock<Calc>();    // Calcクラスをラップする

            // CalcクラスのAddメソッドが、引数「1」「2」で呼ばれたら「3」を返す
            calcMock.Setup(m => m.Add(1, 2)).Returns(3);

            Calc c = calcMock.Object;     // Calc形のインスタンスを取り出す
            Assert.Equal(3, c.Add(1, 2)); // 指定した呼び出しなので、「3」を返す
            Assert.Equal(0, c.Add(2, 1)); // 設定した引数の組み合わせ以外は「0」を返す
        }

        [Fact(DisplayName = "1つの引数に値を指定してスタブ化")]
        public void TestStubAnyMatch()
        {
            var calcMock = new Mock<ICalc>();    // IClassインタフェースをラップ

            // 第1引数はなんでもよい、第2引数が2ならばSubメソッドは3を返す
            calcMock.Setup(m => m.Sub(It.IsAny<int>(), 2)).Returns(3);

            ICalc c = calcMock.Object;
            Assert.Equal(3, c.Sub(1, 2)); // 第2引数が2ならば3を返す
            Assert.Equal(3, c.Sub(2, 2)); // 第2引数が2ならば3を返す
            Assert.Equal(0, c.Sub(2, 1)); // 第2引数が2以外ならば0を返す
        }

        //[Fact(DisplayName = "1つの引数に値を指定してスタブ化")]
        //public void TestStubReturn()
        //{
        //    var calcMock = new Mock<Calc>();

        //    // 第1引数はなんでもよい、第2引数が2ならばAddメソッドは3を返す
        //    calcMock.Setup(
        //        m => m.Add(It.IsAny<int>(), It.IsAny<int>()))
        //        .Returns(
        //        (i1) => (i1));

        //    Calc c = calcMock.Object;
        //    Assert.Equal(3, c.Add(1, 2)); // 第2引数が2ならば3を返す
        //    Assert.Equal(3, c.Add(2, 2)); // 第2引数が2ならば3を返す
        //    Assert.Equal(0, c.Add(2, 1)); // 第2引数が2以外ならば0を返す
        //}

        [Fact(DisplayName = "Whenを使って、Setupの条件を指定する")]
        public void TestWhen()
        {
            int count = 0;
            var mock = new Mock<Calc>();

            // countが偶数の時は、引数の値が何であろうと、Addメソッドは0を返す
            mock.When(() => (count % 2 == 0)).Setup(
                (m) => m.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0);

            // countが奇数の時は、引数の値が何であろうと、Addメソッドは1を返す
            mock.When(() => (count % 2 != 0)).Setup(
                (m) => m.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1);

            count = 10;
            Assert.Equal(0, mock.Object.Add(0, 0)); // countが10(偶数)なので、0を返す

            count = 11;
            Assert.Equal(1, mock.Object.Add(0, 0)); // countが11(奇数)なので、1を返す
        }

        [Theory(DisplayName = "MoqのWhenと、XunitのInlineDataの組合せ")]
        [InlineData(10, 0)]
        [InlineData(11, 1)]
        [InlineData(-10, 0)]
        [InlineData(-11, 1)]
        public void TestWhenAndInlineData(int count, int expected)
        {
            var mock = new Mock<Calc>();

            // countが偶数の時は、引数の値が何であろうと、Addメソッドは0を返す
            mock.When(() => (count % 2 == 0)).Setup(
                (m) => m.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0);

            // countが奇数の時は、引数の値が何であろうと、Addメソッドは1を返す
            mock.When(() => (count % 2 != 0)).Setup(
                (m) => m.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1);

            Assert.Equal(expected, mock.Object.Add(0, 0));
        }
    }
}
