using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class OptimalSubpath
    {
        public Tuple<byte, ulong> Key => new Tuple<byte, ulong>(FrstElem, Elements);

        public byte NmbrElems;

        public ulong Elements;

        public byte FrstElem;

        public byte[] Subpath;

        public double OptlWght;

        public int this[int i]
        {
            get
            {
                return (int)Subpath[i];
            }
        }
    }
}
