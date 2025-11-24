using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Graphics;
using Geometry;
using MaterialsLayout;
using FloorMaterialEstimator;

namespace TestDriverCombosDisplay
{
    public static class TestCases
    {
        public static GraphicsWindow window { get; set; }

        public static GraphicsPage page { get; set; }

        public static PartitionUnitSolution partitionUnitSolution0()
        {
            PartitionUnitSolution solution = new PartitionUnitSolution();

            List<int[]> optimalPaths = new List<int[]>();

            solution.OptimalPaths = optimalPaths;

            return solution;
        }

        public static PartitionUnitSolution partitionUnitSolution1()
        {
            PartitionUnitSolution solution = new PartitionUnitSolution();

            List<int[]> optimalPaths = new List<int[]>();

            optimalPaths.Add(new int[] { 0 });
            optimalPaths.Add(new int[] { 1 });
            optimalPaths.Add(new int[] { 2 });

            solution.OptimalPaths = optimalPaths;

            return solution;
        }

        public static PartitionUnitSolution partitionUnitSolution2()
        {
            PartitionUnitSolution solution = new PartitionUnitSolution();

            List<int[]> optimalPaths = new List<int[]>();

            optimalPaths.Add(new int[] { 0, 1 });
           
            solution.OptimalPaths = optimalPaths;

            return solution;
        }

        public static PartitionSolution partitionSolution1()
        {
            PartitionSolution solution = new PartitionSolution();

            solution.Partition = new int[] { 3, 0, 0 };

            solution.OptimalAllocation = new AllocationSolution(3)
            {
                Allocation = new ulong[] { 7, 0, 0 },
                Solution = new PartitionUnitSolution[] { partitionUnitSolution1(), partitionUnitSolution0(), partitionUnitSolution0() }
            };

            return solution;
        }
        
        public static PartitionSolution partitionSolution2()
        {
            PartitionSolution solution = new PartitionSolution();

            solution.Partition = new int[] { 0, 1, 0 };

            solution.OptimalAllocation = new AllocationSolution(2)
            {
                Allocation = new ulong[] { 0, 3, 0 },
                Solution = new PartitionUnitSolution[] { partitionUnitSolution0(), partitionUnitSolution2(), partitionUnitSolution0() }
            };

            return solution;
        }

        private static List<DirectedLine> lineList1 = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(0,0), new Coordinate(1,0)),
            new DirectedLine(new Coordinate(1,0), new Coordinate(0.8,1)),
            new DirectedLine(new Coordinate(0.8, 1), new Coordinate(0.2, 1)),
            new DirectedLine(new Coordinate(0.2, 1), new Coordinate(0,0))
        };

        private static List<DirectedLine> lineList2 = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(0,0), new Coordinate(2,0)),
            new DirectedLine(new Coordinate(2,0), new Coordinate(1.8,1)),
            new DirectedLine(new Coordinate(1.8, 1), new Coordinate(0.2, 1)),
            new DirectedLine(new Coordinate(0.2, 1), new Coordinate(0,0))
        };

        private static List<DirectedLine> lineList3 = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(0,0), new Coordinate(3,0)),
            new DirectedLine(new Coordinate(3,0), new Coordinate(2.8,1)),
            new DirectedLine(new Coordinate(2.8, 1), new Coordinate(0.2, 1)),
            new DirectedLine(new Coordinate(0.2, 1), new Coordinate(0,0))
        };

        private static List<DirectedLine> lineList4 = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(0.2,0), new Coordinate(.8,0)),
            new DirectedLine(new Coordinate(.8, 0), new Coordinate(1,1)),
            new DirectedLine(new Coordinate(1, 1), new Coordinate(0, 1)),
            new DirectedLine(new Coordinate(0, 1), new Coordinate(0.2,0))
        };


        private static GraphicsDirectedPolygon polygon1 => new GraphicsDirectedPolygon(window, page, lineList1);
        private static GraphicsDirectedPolygon polygon2 => new GraphicsDirectedPolygon(window, page, lineList2);
        private static GraphicsDirectedPolygon polygon3 => new GraphicsDirectedPolygon(window, page, lineList3);
        private static GraphicsDirectedPolygon polygon4 => new GraphicsDirectedPolygon(window, page, lineList4);

        private static GraphicsCut cut1 => new GraphicsCut(null, null) { Tag = 1, CutAngle = 0, CutPolygonList = new List<GraphicsDirectedPolygon>() { polygon1 } };
        private static GraphicsCut cut2 => new GraphicsCut(null, null) { Tag = 2, CutAngle = 0, CutPolygonList = new List<GraphicsDirectedPolygon>() { polygon2 } };
        private static GraphicsCut cut3 => new GraphicsCut(null, null) { Tag = 3, CutAngle = 0, CutPolygonList = new List<GraphicsDirectedPolygon>() { polygon3 } };
        private static GraphicsCut cut4 => new GraphicsCut(null, null) { Tag = 4, CutAngle = 0, CutPolygonList = new List<GraphicsDirectedPolygon>() { polygon4 } };

        public static List<GraphicsCut> CutList1 => new List<GraphicsCut> { cut1, cut2, cut3 };

        public static double[,] wghtMtrx1 = new double[3, 3]
        {
            { 0, 0.2, 0.4 },
            { 0.1, 0, 0.3 },
            { 0.3, 0.2, 0 }
        };


        public static List<GraphicsCut> CutList2 => new List<GraphicsCut> { cut1, cut4 };

        public static double[,] wghtMtrx2 = new double[2, 2]
        {
            { 0, 0.2},
            { 0.2, 0 }
        };

        public static ComboPartitionSolution comboPartitionSolution1 = null;// new ComboPartitionSolution(TestCases.partitionSolution1(), TestCases.CutList1.ToArray(), TestCases.wghtMtrx1);
        public static ComboPartitionSolution comboPartitionSolution2 = null;// new ComboPartitionSolution(TestCases.partitionSolution2(), TestCases.CutList1.ToArray(), TestCases.wghtMtrx2);
    }
}
