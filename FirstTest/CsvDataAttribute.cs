using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;
using System.Reflection;
using System.IO;

namespace FirstTest
{
    // 独自の属性の定義
    // 属性の対象はメソッド
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CsvDataAttribute : DataAttribute // クラス名から「Attribute」を除いた文字列が属性名になる
    {
        private string _filename;

        // 1つの値をとるコンストラクタ
        public CsvDataAttribute(string filename)
        {
            _filename = filename;
        }
        public CsvDataAttribute(string filename, bool hasHeadder)
        {
            _filename = filename;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            ParameterInfo[] pars = testMethod.GetParameters();
            return DataSource(pars.Select((par) => par.ParameterType).ToArray());
        }

        private IEnumerable<object[]> DataSource(Type[] parameterTypes)
        {
            using (var reader = new StreamReader(_filename))
            {
                string line = reader.ReadLine(); // 1行目はヘッダーなので読み飛ばす
                for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    var rowData = line.Split(','); // 単純にカンマで区切って配列にする

                    yield return ConvertParameters(rowData, parameterTypes);
                }
            }
        }

        private static object[] ConvertParameters(object[] values, Type[] parameterTypes)
        {
            object[] result = new object[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                result[i] = ConvertParameter(values[i], i >= parameterTypes.Length ? null : parameterTypes[i]);
            }

            return result;
        }

        private static object ConvertParameter(object parameter, Type parameterType)
        {
            if ((parameter is double || parameter is float) &&
                (parameterType == typeof(int) || parameterType == typeof(int?)))
            {
                int intValue;
                string floatValueAsString = parameter.ToString();

                if (Int32.TryParse(floatValueAsString, out intValue))
                {
                    return intValue;
                }
            }

            return parameter;
        }
    }
}
