using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        unsafe private struct IndicesAndValue
        {
            public double Value;
            public fixed int Indices[8];
        }

        public static unsafe void MakeIndicesAndValue()
        {
            var theStruct = new IndicesAndValue();
            theStruct.Value = 9.9;
            theStruct.Indices[0] = 8;
        }

        static void Main(string[] args)
        {
            MakeIndicesAndValue();
        }
    }
}
