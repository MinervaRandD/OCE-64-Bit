using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class AllocationSolution
    {
        public ulong[] Allocation;

        public PartitionUnitSolution[] Solution;

        public AllocationSolution(int maxmElem)
        {
            Solution = new PartitionUnitSolution[maxmElem];

            for (int i = 0; i < maxmElem;i++)
            {
                Solution[i] = new PartitionUnitSolution();
            }
        }

        public double SolutionValue => Solution.Sum(s => s.OptimalValue);

        public PartitionUnitSolution this[int i]
        {
            get
            {
                return Solution[i];
            }
        }
    }
}
