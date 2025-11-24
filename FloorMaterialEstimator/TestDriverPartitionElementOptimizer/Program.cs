

namespace TestDriverPartitionElementOptimizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Utilities;

    class Program
    {
        static void Main(string[] args)
        {
            PartitionUnitOptimizer peo = new PartitionUnitOptimizer(3, 63, 6, TestCases.WghtMtrx1);

            peo.OptimizeUnit();
        }
    }
}
