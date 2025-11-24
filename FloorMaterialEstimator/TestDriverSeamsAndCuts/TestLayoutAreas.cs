

namespace TestDriverRolloutOversAndUnders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Graphics;
    using MaterialsLayout;

    public static class TestLayoutAreas
    {
        // Test 1
        public static List<Coordinate> Test1CoordList = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(2,0),
            new Coordinate(2,8),
            new Coordinate(0,8)
        };

        // Test 2
        public static List<Coordinate> Test2CoordList = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(0,8),
            new Coordinate(2,8),
            new Coordinate(2,0)
        };

        // Test 3
        public static List<Coordinate> Test3CoordList = new List<Coordinate>()
        {
            new Coordinate(2,2),
            new Coordinate(12,2),
            new Coordinate(12,12),
            new Coordinate(2,12)
        };

        // Test 4
        public static List<Coordinate> Test4CoordList = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(0,12),
            new Coordinate(2,12),
            new Coordinate(2,9),
            new Coordinate(4,6),
            new Coordinate(2,3),
            new Coordinate(2,0)
        };

        // Test 5
        public static List<Coordinate> Test5CoordList = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(0,12),
            new Coordinate(2,12),
            new Coordinate(2,8),
            new Coordinate(4,10),
            new Coordinate(3.5,4),
            new Coordinate(2,6),
            new Coordinate(2,3),
            new Coordinate(3,3),
            new Coordinate(3,1),
            new Coordinate(2,1),
            new Coordinate(2,0)
        };

        // Test 6
        public static List<Coordinate> Test6CoordList = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(0,8),
            new Coordinate(2,8),
            new Coordinate(6,4),
            new Coordinate(2,6),
            new Coordinate(2,0)
        };

        // Test 7
        public static List<Coordinate> Test7CoordList = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(12,0),
            new Coordinate(6,8)
        };

        public static List<List<Coordinate>> TestCoordLists = new List<List<Coordinate>>()
        {
            Test1CoordList,
            Test2CoordList,
            Test3CoordList,
            Test4CoordList,
            Test5CoordList,
            Test6CoordList,
            Test7CoordList
        };

        private static GraphicsLayoutArea TestCaseGraphicsLayoutArea(GraphicsWindow window, GraphicsPage page, int testNmbr, double scale)
        {
            List<Coordinate> coordList = genScaledCoordList(TestCoordLists[testNmbr], scale);

            GraphicsDirectedPolygon directedPolygon = new GraphicsDirectedPolygon(window, page, coordList);

            return new GraphicsLayoutArea(window, page, directedPolygon);
        }

        private static List<Coordinate> genScaledCoordList(List<Coordinate> inptCoordList, double scale)
        {
           return new List<Coordinate>(inptCoordList.ConvertAll(l => scale * l));
        }

        // Get test by number

        public static GraphicsLayoutArea TestGraphicsLayoutArea(GraphicsWindow window, GraphicsPage page, int testNumber, double scale = 1)
        {
            if (testNumber < 1 || testNumber > TestCoordLists.Count)
            {
                return null;
            }

            return TestCaseGraphicsLayoutArea(window, page, testNumber - 1, scale);
        }

    }
}
