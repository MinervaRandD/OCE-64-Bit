using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public enum LayoutAreaType
    {
        Unknown = 0,
        Normal = 1,
        FixedWidth = 2,
        Remnant = 4,
        ColorOnly = 8,
        OversGenerator = 16,
        ZeroArea = 32
    }
}
