
namespace CombosOld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FloorMaterialEstimator.CanvasManager;
    using Geometry;
    using Utilities;

    public class CombosSelectionGenerator
    {
        private List<CanvasCut> cutList;

        private int nmbrElems;

        private double[,] wghtMtrx;

        private List<int[]> partitions;

        /// <summary>
        /// Generates a list of optimal cut combos for every possible way to partition the set of cuts into subsets
        /// </summary>
        /// <param name="layoutAreaList">List of canvas layout areas</param>
        public CombosSelectionGenerator(List<CanvasCut> cutList)
        {
            this.cutList = cutList;

            this.nmbrElems = cutList.Count;
        }

        public CombosSelectionGenerator(double[,] wghtMtrx)
        {
            this.wghtMtrx = wghtMtrx;

            this.nmbrElems = wghtMtrx.GetLength(0);
        }


        public void GenerateSelections()
        {
            if (wghtMtrx == null)
            {
                if (partitions == null)
                {
                    throw new NotImplementedException();
                }

                wghtMtrx = generateWghtMtrx();
            }
            partitions = generatePartitionList();

            foreach (int[] partition in partitions)
            {
                PartitionAllocator partitionAllocator = new PartitionAllocator(partition);

                List<ulong[]> allocationList = partitionAllocator.GenerateAllocations();
            }
        }

        private double[,] generateWghtMtrx()
        {
            double[,] rtrnMtrx = new double[nmbrElems, nmbrElems];

            for (int i1 = 0; i1 < nmbrElems; i1++)
            {
                DirectedPolygon polygon1 = (DirectedPolygon)cutList[i1].CutPolygon;

                for (int i2 = 0; i2 < nmbrElems; i2++)
                {
                    if (i2 == i1)
                    {
                        continue;
                    }

                    DirectedPolygon polygon2 = (DirectedPolygon)cutList[i2].CutPolygon;

                    PolygonDistanceGenerator distanceGenerator = new PolygonDistanceGenerator(polygon1, polygon2);

                    rtrnMtrx[i1, i2] = distanceGenerator.GenPolyDistance();
                }
            }

            return rtrnMtrx;
        }

        private List<int[]> generatePartitionList()
        {
            PartitionGenerator partitionGenerator = new PartitionGenerator(nmbrElems);

            List<int[]> partitionList = partitionGenerator.GeneratePartitions();

            return partitionList;
        }
    }
}
