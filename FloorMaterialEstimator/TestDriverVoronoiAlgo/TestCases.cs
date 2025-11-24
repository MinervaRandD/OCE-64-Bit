using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using Graphics;
using System.Drawing;

namespace TestDriverVoronoiAlgo
{
    public static class TestCases
    {
        public static List<Coordinate> TestCase1Coords = new List<Coordinate>()
        {
            new Coordinate(1,1), new Coordinate(9,1), new Coordinate(9,9), new Coordinate(1,9)
        };

        public static List<DirectedLine> TestCase1DirectedLines = new List<DirectedLine>();

        public static List<Coordinate> TestCase2Coords = new List<Coordinate>()
        {
            new Coordinate(1,1), new Coordinate(12,1), new Coordinate(8,9), new Coordinate(4,9)
        };

        public static List<DirectedLine> TestCase2DirectedLines = new List<DirectedLine>();

        public static List<Coordinate> TestCase3Coords = new List<Coordinate>()
        {
            new Coordinate(1,1), new Coordinate(13,1), new Coordinate(13,9), new Coordinate(9,6), new Coordinate(5,10), new Coordinate(1,3)
        };

        public static List<DirectedLine> TestCase3DirectedLines = new List<DirectedLine>();

        public static List<Coordinate> TestCase4Coords = new List<Coordinate>()
        {
            new Coordinate(5.65,2), new Coordinate(5.65,8), new Coordinate(8,8), new Coordinate(8,2)
        };

        public static List<DirectedLine> TestCase4DirectedLines = new List<DirectedLine>();

        public static void SetupTestCases()
        {
            SetupTestCase(TestCase1Coords, TestCase1DirectedLines);
            SetupTestCase(TestCase2Coords, TestCase2DirectedLines);
            SetupTestCase(TestCase3Coords, TestCase3DirectedLines);
            SetupTestCase(TestCase4Coords, TestCase4DirectedLines);
        }

        private static void SetupTestCase(List<Coordinate> testCaseCoords, List<DirectedLine> testCaseDirectedLines)
        {
            testCaseDirectedLines.Clear();

            int count = testCaseCoords.Count;

            for (int i = 0; i < count; i++)
            {
                Coordinate coord1 = testCaseCoords[i];
                Coordinate coord2 = testCaseCoords[(i + 1) % count];

                testCaseDirectedLines.Add(new DirectedLine(coord1, coord2));
            }
        }
    }
}
