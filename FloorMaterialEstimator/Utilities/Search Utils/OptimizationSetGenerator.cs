using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class OptimizationSetGenerator
    {
        int nmbrBaseElems;

        double[,] wghtMtrx;

        public OptimizationSetGenerator(int nmbrBaseElems, double[,] wghtMtrx)
        {
            this.nmbrBaseElems = nmbrBaseElems;
            this.wghtMtrx = wghtMtrx;
        }

        public Dictionary<ulong, Tuple<int[], double>> GenerateSolutionSet()
        {
            Dictionary<ulong, Tuple<int[], double>> rtrnDict = new Dictionary<ulong, Tuple<int[], double>>();

            ulong limit = (ulong) 1 << nmbrBaseElems;

            rtrnDict.Add(0, new Tuple<int[], double>(new int[0], 0));

            for (ulong i = 1; i < limit; i++)
            {
                if (BitUtils.Count(i) == 1)
                {
                    rtrnDict.Add(i, new Tuple<int[], double>(new int[1] { (int)i }, 0));
                }

                else
                {
                    rtrnDict.Add(i, generateSubsetOptimalSolution(i));
                }
            }

            return rtrnDict;
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

    }
}
