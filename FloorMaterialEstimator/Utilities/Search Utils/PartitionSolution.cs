

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PartitionSolution
    {
        public int[] Partition;

        public AllocationSolution OptimalAllocation;
        public PartitionUnitSolution[] OptimalSolution => OptimalAllocation.Solution;
        public double SolutionValue => OptimalAllocation.SolutionValue;

        public string Key()
        {
            return string.Join(":", Partition.Select(i => i.ToString("00")));
        }

        //public string PartitionDefinition()
        //{
        //    List<string> labelList = new List<string>();

        //    for (int i = Partition.Length; i > 0; i--)
        //    {
        //        int count = Partition[i - 1];

        //        if (count == 0)
        //        {
        //            continue;
        //        }

        //        string lbl = i.ToString();

        //        for (int j = 0; j < count; j++)
        //        {
        //            labelList.Add(lbl);
        //        }
        //    }

        //    return string.Join(",", labelList);
        //}
    }
}
