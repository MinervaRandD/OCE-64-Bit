using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class PartitionUnitSolution
    {
        public List<int[]> OptimalPaths;

        public double OptimalValue;

        public PartitionUnitSolution()
        {
            OptimalPaths = new List<int[]>();
            OptimalValue = 0;
        }
    }
}
