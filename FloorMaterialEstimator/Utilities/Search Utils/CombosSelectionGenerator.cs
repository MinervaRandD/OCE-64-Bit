
namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class CombosSelectionGenerator
    {
       
        private int nmbrElems;

        private double[,] wghtMtrx;

        private List<int[]> partitions;

        public CombosSelectionGenerator(double[,] wghtMtrx)
        {
            this.wghtMtrx = wghtMtrx;

            this.nmbrElems = wghtMtrx.GetLength(0);
        }

        public List<PartitionSolution> GenerateSelections()
        {
            List<PartitionSolution> rtrnList = new List<PartitionSolution>();

            partitions = generatePartitionList();

            for (int p = 0; p < partitions.Count; p++)
            {
                int[] partition = partitions[p];

                double maxmSolnValu = double.MinValue;
                AllocationSolution maxmSoln = null;

                // An 'allocation' is an assignment of elements to each unit in a partition

                PartitionAllocator partitionAllocator = new PartitionAllocator(partition);

                List<ulong[]> allocationList = partitionAllocator.GenerateAllocations();

                // The result of the following loop is the optimal allocation and the value associated with it.

                foreach (ulong[] allocation in allocationList)
                {
                    AllocationSolution solution = new AllocationSolution(allocation.Length);

                    solution.Allocation = allocation;

                    for (int i = 0; i < allocation.Length; i++)
                    {
                        PartitionUnitOptimizer partitionElementOptimizer = new PartitionUnitOptimizer(i+1, allocation[i], nmbrElems, wghtMtrx);
                        solution.Solution[i] = partitionElementOptimizer.OptimizeUnit();
                    }

                    if (solution.SolutionValue > maxmSolnValu)
                    {
                        maxmSoln = solution;
                        maxmSolnValu = solution.SolutionValue;
                    }
                    
                }

                PartitionSolution partitionSolution = new PartitionSolution()
                {
                    OptimalAllocation = maxmSoln,
                    Partition = partition
                };

                rtrnList.Add(partitionSolution);
            }

            return rtrnList;
        }

        private List<int[]> generatePartitionList()
        {
            PartitionGenerator partitionGenerator = new PartitionGenerator(nmbrElems);

            List<int[]> partitionList = partitionGenerator.GeneratePartitions();

            return partitionList;
        }
    }
}
