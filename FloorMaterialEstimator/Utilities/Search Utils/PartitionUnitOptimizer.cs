using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class PartitionUnitOptimizer
    {
        private int allocUnitSize;

        private int nmbrAllocUnits;

        private int nmbrBaseElems;

        private ulong elemSubset;

        double[,] wghtMtrx;

        private Dictionary<ulong, Tuple<int[], double>> optimalSubsetSoln = new Dictionary<ulong, Tuple<int[], double>>();
        /// <summary>
        /// Generates an optimized solution for a specific element size.
        /// </summary>
        /// <param name="allocUnitSize">The size of the allocation unit, meaning the number of cuts to be mated as a unit</param>
        /// <param name="elemSubset">The subset of elements (cuts) to be considered</param>
        /// <param name="nmbrBaseElems">the number of elements in the underlying problem</param>
        /// <param name="wghtMtrx">The weight matrix giving the benefit for mating two cuts in a specific order</param>
        public PartitionUnitOptimizer(int allocUnitSize, ulong elemSubset, int nmbrBaseElems, double[,] wghtMtrx)
        {
            this.elemSubset = elemSubset;
            this.allocUnitSize = allocUnitSize;

            this.nmbrBaseElems = nmbrBaseElems;

            this.wghtMtrx = wghtMtrx;

            Debug.Assert(BitUtils.Count(elemSubset) % allocUnitSize == 0); // It is necessary that the number of elements be divisible by the the element size
            Debug.Assert(wghtMtrx.GetLength(0) == wghtMtrx.GetLength(1)); // Must be a square matrix;
            Debug.Assert(wghtMtrx.GetLength(0) == nmbrBaseElems);

            nmbrAllocUnits = BitUtils.Count(elemSubset) / allocUnitSize;
        }

        /// <summary>
        /// This routine finds the best cut combination for a specific element size. By partition element size, what is meant
        /// by partition element size is the number of cuts that will be mated together. A partition element size may be 2 but
        /// but there may be 4 cuts assigned to it, so we are looking to find 2 combinations of 2 cuts each.
        /// </summary>
        public PartitionUnitSolution OptimizeUnit()
        {
            if (elemSubset == 0)
            {
                return new PartitionUnitSolution();
            }

            // Generate all possible combinations of subsets of size (nmbrElems / elementSize)

            ComboGenerator comboGenerator = new ComboGenerator(elemSubset, nmbrBaseElems);

            List<ulong> subsets = comboGenerator.GenerateCombos(allocUnitSize);

            // Generate the optimal solution for each subset of size (nmbrElems / elementSize), and save them in a dictionary.

            foreach (ulong subset in subsets)
            {
                //WeightedPath wghtPath = new WeightedPath(wghtMtrx);

                //wghtPath.MaximizePrimalAStar();

                optimalSubsetSoln.Add(subset, generateSubsetOptimalSolution(subset));
            }

            generateMaxSubsetList();

            return new PartitionUnitSolution() { OptimalPaths = maxmSubsetList, OptimalValue = maxmSubsetListValue };
        }

        private Tuple<int[], double> generateSubsetOptimalSolution(ulong subset)
        {
            int count = BitUtils.Count(subset);

            int[] subsetMap = new int[count];

            for (int i = 0, j = 0; i < nmbrBaseElems; i++)
            {
                if (BitUtils.Contains(subset, i))
                {
                    subsetMap[j++] = i;
                }
            }

            double[,] subWghtMtrx = new double[count, count];

            for (int i1 = 0; i1 < count; i1++)
            {
                for (int i2 = 0; i2 < count; i2++)
                {
                    if (i1 == i2)
                    {
                        continue;
                    }

                    subWghtMtrx[i1, i2] = wghtMtrx[subsetMap[i1], subsetMap[i2]];
                }
            }

            WeightedPath wghtPath = new WeightedPath(subWghtMtrx);

            wghtPath.MaximizePrimalAStar();

            int[] optmPath = new int[count];

            for (int i = 0; i < count; i++)
            {
                optmPath[i] = subsetMap[wghtPath.maxmPath[i]];
            }

            return new Tuple<int[], double>(optmPath, wghtPath.maxmWght);
        }

        private double maxmSubsetListValue;
        private List<int[]> maxmSubsetList;

        /// <summary>
        /// Generates a list of (nmbrElems / elementSize) subsets of size elementSize  that maximizes the benefit. The trick/constraint is that
        /// there can be no overlap of basic elements (cuts) between two subsets in the list.
        /// </summary>
        /// <returns></returns>
        private void generateMaxSubsetList()
        {
            maxmSubsetListValue = double.MinValue;
            maxmSubsetList = new List<int[]>();

            List<int> subsetList = new List<int>();

            for (int i = 0; i < optimalSubsetSoln.Count - nmbrAllocUnits + 1; i++)
            {
                double valu = optimalSubsetSoln.Values.ElementAt(i).Item2;

                subsetList.Add(i);

                generateMaxSublist(i + 1, valu, subsetList, 1, nmbrAllocUnits);

                subsetList.RemoveAt(0);
            }
        }

        private void generateMaxSublist(int iStart, double valu, List<int> subsetList, int level, int nmbrAllocUnits)
        {
            if (level == nmbrAllocUnits)
            {
                if (valu > maxmSubsetListValue)
                {
                    maxmSubsetListValue = valu;

                    maxmSubsetList.Clear();

                    foreach (int i in subsetList)
                    {
                        maxmSubsetList.Add(optimalSubsetSoln.Values.ElementAt(i).Item1);
                    }
                }

                return;
            }

            for (int i = iStart; i < optimalSubsetSoln.Count - nmbrAllocUnits + 1 + level; i++)
            {
                ulong subset = optimalSubsetSoln.Keys.ElementAt(i);

                if (intersects(subset, subsetList))
                {
                    continue;
                }

                subsetList.Add(i);

                double incrValu = optimalSubsetSoln.Values.ElementAt(i).Item2;

                generateMaxSublist(i + 1, valu + incrValu, subsetList, level + 1, nmbrAllocUnits);

                subsetList.RemoveAt(level);
            }
        }

        private bool intersects(ulong subset1, List<int> subsetList)
        {
            foreach (int i in subsetList)
            {
                ulong subset2 = optimalSubsetSoln.Keys.ElementAt(i);

                if ((subset1 & subset2) != 0)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
