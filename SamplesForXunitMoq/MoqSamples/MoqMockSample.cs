using System;
using Xunit;
using Moq;
using System.IO;


namespace MoqSample
{
    public class MoqMockSample
    {
        [Fact(DisplayName = "Callbackの使い方。整形してファイルに出力しているかを確認する")]
        public void TestCallback()
        {
            var mock = new Mock<TextWriter>();
            mock.Setup((m) => m.Write(It.IsAny<string>()))
                .Callback<string>(s =>
                {
                    Assert.Equal(@"{""name"":""テス太郎""}", s);    // (1)
                });
            mock.Setup((m) => m.Write(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<int>()))
                .Callback<string, object, object>((format, name, value) =>
                {
                    Assert.Equal("age", name);  // (2)
                    Assert.Equal(13, value);
                });
            mock.Setup((m) => m.Write(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<double>()))
                .Callback<string, object, object>((format, name, value) =>
                {
                    Assert.Equal("height", name); // (3)
                    Assert.Equal(175.5, value);
                });

            TextWriter tw = mock.Object;
            var writer = new JsonWriter(tw);
            writer.Write("name", "テス太郎");   // (1)が呼ばれる
            writer.Write("age", 13);            // (2)が呼ばれる
            writer.Write("height", 175.5);      // (3)が呼ばれる
        }

        [Fact(DisplayName = "Throwsの使い方")]
        public void TestThrows()
        {
            var mock = new Mock<TextWriter>();
            mock.Setup(
                (m) => m.Write(It.IsAny<string>())) // Writeメソッドを呼ばれると
                .Throws<FileNotFoundException>();   // IOExceptionの派生クラスの
                                                    //FileNotFoundExeptionを発生させる

            var writer = new JsonWriter(mock.Object);
            Assert.ThrowsAny<IOException>(
                () =>
                {
                    writer.Write("name", "テス太郎");
                });
        }


        [Fact(DisplayName = "Verifyの使い方")]
        public void TestVerify()
        {
            var mock = new Mock<TextWriter>();

            var writer = new JsonWriter(mock.Object);
            writer.Write("name", "テス太郎");   // 内部でstring, string, stringが呼ばれる。1回目

            // Write(string, string, int)は1回も呼ばれていない
            mock.Verify(
                (m) => m.Write(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never());

            // Write(string, string, int)は最大でも1回しか呼ばれていない(実際は0回)
            mock.Verify(
                (m) => m.Write(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.AtMostOnce());

            // Write(string, string, int)は最大でも3回しか呼ばれていない(実際は0回)
            mock.Verify(
                (m) => m.Write(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.AtMost(3));

            // Write(string)は一回は呼ばれている
            mock.Verify(
                (m) => m.Write(It.IsAny<string>()), Times.AtLeastOnce());

            // Write(string)は1回だけ呼ばれている
            mock.Verify(
                (m) => m.Write(It.IsAny<string>()), Times.Once());

            writer.Write("name", "テス太郎");   // 内部でstring, string, stringが呼ばれる。2回目

            // Write(string)は2回だけ呼ばれている
            mock.Verify(
                (m) => m.Write(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact(DisplayName = "VerifyAllの使い方")]
        public void TestVerifyAll()
        {
            var mock = new Mock<TextWriter>();
            mock.Setup(
                (m) => m.Write(It.IsAny<string>()));

            var writer = new JsonWriter(mock.Object);

            // SetUpした箇所がすべて呼ばれていないときにMockException発生
            Assert.ThrowsAny<MockException>(
                () =>
                {
                    mock.VerifyAll();
                });
        }


    }
}
