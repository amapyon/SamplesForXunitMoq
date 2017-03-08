using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample
{
    public interface ICalc
    {
        int Sub(int v1, int v2);
    }

    public class Calc : ICalc
    {
        // Stub化されるメソッドは、オーバーライド可能(virtual)にしておく
        public virtual int Add(int v1, int v2)
        {
            return 100;
        }

        public int Sub(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }

}
