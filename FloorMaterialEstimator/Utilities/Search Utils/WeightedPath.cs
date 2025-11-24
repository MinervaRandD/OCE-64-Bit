

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WeightedPath
    {
        public double[,] WghtMtrx;

        public int NmbrElem;

        public int[] maxmPath;

        public double maxmWght;

        public WeightedPath(double [,] wghtMtrx)
        {
            this.WghtMtrx = wghtMtrx;
        }

        private int[] currPath;

        Dictionary<Tuple<byte, ulong>, OptimalSubpath> solvedSubpaths;

        public void MaximizePrimalAStar()
        {
            NmbrElem = WghtMtrx.GetLength(0);

            Debug.Assert(NmbrElem == WghtMtrx.GetLength(1));

            solvedSubpaths = new Dictionary<Tuple<byte, ulong>, OptimalSubpath>();

            OptimalSubpath optmSubPath = null;
            maxmWght = double.MinValue;

            ulong initialSet = 0;

            for (int i = 0; i < NmbrElem; i++)
            {
                initialSet |= BitUtils.encodedBits[i];
            }

            for (byte elem = 0; elem < NmbrElem; elem++)
            {
                OptimalSubpath subPath1 = solveFromState(elem, initialSet, (byte) NmbrElem);

                double currWght = subPath1.OptlWght;

                if (currWght > maxmWght)
                {
                    maxmWght = currWght;
                    optmSubPath = subPath1;
                }
            }

            maxmPath = new int[NmbrElem];

            for (int i = 0; i < NmbrElem; i++)
            {
                maxmPath[i] = optmSubPath[i];
            }
        }

        private OptimalSubpath solveFromState(byte frstElem, ulong remainingElements, byte nmbrElems)
        {
            Tuple<byte, ulong> key = new Tuple<byte, ulong>(frstElem, remainingElements);

            if (solvedSubpaths.ContainsKey(key))
            {
                return solvedSubpaths[key];
            }

            else
            {
                OptimalSubpath subPath;

                ulong remainingSubElements = BitUtils.Remove(remainingElements, frstElem);

                if (remainingSubElements == 0)  // Only one element remaining
                {
                    subPath = new OptimalSubpath()
                    {
                        NmbrElems = 1,
                        Elements = remainingElements,
                        FrstElem = frstElem,
                        Subpath = new byte[1] { frstElem },
                        OptlWght = 0.0
                    };

                    solvedSubpaths.Add(subPath.Key, subPath);

                    return subPath;
                }

                OptimalSubpath optmSubPath = null;
                double optmWght = double.MinValue;

                for (byte elem = 0; elem < NmbrElem; elem++)
                {
                    if (!BitUtils.Contains(remainingSubElements, elem))
                    {
                        continue;
                    }

                    OptimalSubpath subPath1 = solveFromState(elem, remainingSubElements, (byte) (nmbrElems-1));

                    double currWght = subPath1.OptlWght + WghtMtrx[frstElem, elem];

                    if (currWght > optmWght)
                    {
                        optmWght = currWght;
                        optmSubPath = subPath1;
                    }
                }

                byte[] optmPath = new byte[nmbrElems];

                for (int i = 0; i < nmbrElems - 1; i++)
                {
                    optmPath[i + 1] = optmSubPath.Subpath[i];
                }

                optmPath[0] = frstElem;

                subPath = new OptimalSubpath()
                {
                    NmbrElems = nmbrElems,
                    Elements = remainingElements,
                    FrstElem = frstElem,
                    Subpath = optmPath,
                    OptlWght = optmWght
                };

                solvedSubpaths.Add(subPath.Key, subPath);

                return subPath;
            }
        }

        public void MaximizePrimalGenAndTest()
        {
            NmbrElem = WghtMtrx.GetLength(0);

            Debug.Assert(NmbrElem == WghtMtrx.GetLength(1));

            maxmPath = new int[NmbrElem];
            currPath = new int[NmbrElem];

            HashSet<int> elemSet = new HashSet<int>();

            for (int i = 0; i < NmbrElem; i++)
            {
                elemSet.Add(i);
            }

            GenAndTest(elemSet, 0);
        }

        private void GenAndTest(HashSet<int> elemSet, int level)
        {
            if (level == NmbrElem)
            {
                double currWght = getWght(currPath);

                if (currWght > maxmWght)
                {
                    for (int i = 0;i < NmbrElem;i++)
                    {
                        maxmPath[i] = currPath[i];
                    }

                    maxmWght = currWght;
                }

                return;
            }

            List<int> elemList = new List<int>(elemSet);

            foreach (int elem in elemList)
            {
                currPath[level] = elem;

                elemSet.Remove(elem);

                GenAndTest(elemSet, level + 1);

                elemSet.Add(elem);
            }
        }

        private double getWght(int[] path)
        {
            double wght = 0.0;

            for (int i = 0; i < NmbrElem-1; i++)
            {
                wght += WghtMtrx[path[i], path[i + 1]];
            }

            return wght;
        }

    }
}
