using System;
using System.IO;

namespace MoqSample
{
    public class JsonWriter : IDisposable
    {
        private TextWriter _writer;

        public JsonWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public void Dispose() => _writer.Close();

        public void Write(string key, string value)
        {
            string s = string.Format(@"{{""{0}"":""{1}""}}", key, value);
            _writer.Write(s); // 引数の型はstring
        }

        public void Write(string key, int value)
        {
            _writer.Write(@"{{""{0}"":{1}}}", key, value); // 引数の型はstring, string, int
        }

        public void Write(string key, double value)
        {
            _writer.Write(@"{{""{0}"":{1}}}", key, value); // 引数の型はstring, string, double
        }
    }
}
