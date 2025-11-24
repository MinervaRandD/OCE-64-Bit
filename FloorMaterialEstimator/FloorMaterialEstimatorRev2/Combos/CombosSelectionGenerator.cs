
namespace FloorMaterialEstimator
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
        private int nmbrElems;

        private double[,] wghtMtrx;

        private List<int[]> partitions;

        public CombosSelectionGenerator(double[,] wghtMtrx)
        {
            this.wghtMtrx = wghtMtrx;

            this.nmbrElems = wghtMtrx.GetLength(0);
        }


        public void GenerateSelections()
        {
            if (wghtMtrx == null)
            {
                throw new NotImplementedException();

                //if (partitions == null)
                //{
                //    throw new NotImplementedException();
                //}

                //wghtMtrx = generateWghtMtrx();
            }

            partitions = generatePartitionList();

            foreach (int[] partition in partitions)
            {
                PartitionAllocator partitionAllocator = new PartitionAllocator(partition);

                List<ulong[]> allocationList = partitionAllocator.GenerateAllocations();
            }
        }

        //private double[,] generateWghtMtrx()
        //{
           
        //    double[,] rtrnMtrx = new double[nmbrElems, nmbrElems];

        //    for (int i1 = 0; i1 < nmbrElems; i1++)
        //    {

        //        DirectedPolygon polygon1 = (DirectedPolygon)cutList[i1].CutPolygon;

        //        for (int i2 = 0; i2 < nmbrElems; i2++)
        //        {
        //            if (i2 == i1)
        //            {
        //                continue;
        //            }

        //            DirectedPolygon polygon2 = (DirectedPolygon)cutList[i2].CutPolygon;

        //            PolygonDistanceGenerator distanceGenerator = new PolygonDistanceGenerator(polygon1, polygon2);

        //            rtrnMtrx[i1, i2] = distanceGenerator.GenPolyDistance();
        //        }
        //    }

        //    return rtrnMtrx;
        //}

        private List<int[]> generatePartitionList()
        {
            PartitionGenerator partitionGenerator = new PartitionGenerator(nmbrElems);

            List<int[]> partitionList = partitionGenerator.GeneratePartitions();

            return partitionList;
        }
    }
}
