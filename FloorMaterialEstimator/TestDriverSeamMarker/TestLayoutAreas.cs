

namespace TestDriverSeamMarker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using TracerLib;

    public static class TestLayoutAreas
    {
        // Test 1
        public static List<Coordinate> Test1CoordList = new List<Coordinate>()
        {
            new Coordinate(4,0),
            new Coordinate(4,6),
            new Coordinate(8,6),
            new Coordinate(8,0)
        };

        // Test 2
        public static List<Coordinate> Test2CoordList = new List<Coordinate>()
        {
            new Coordinate(4,0),
            new Coordinate(4,3.5),
            new Coordinate(8,3.5),
            new Coordinate(8,0)
        };

        // Test 3
        public static List<Coordinate> Test3CoordList = new List<Coordinate>()
        {
            new Coordinate(4,0),
            new Coordinate(4,2.5),
            new Coordinate(8,2.5),
            new Coordinate(8,0)
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
            new Coordinate(4, 0),
            new Coordinate(4, 3.5),
            new Coordinate(6, 3.5),
            new Coordinate(6, 4),
            new Coordinate(8, 4),
            new Coordinate(8, 2.5),
            new Coordinate(10, 2.5),
            new Coordinate(10, 0)
        };

        // Test 6
        public static List<Coordinate> Test6CoordList = new List<Coordinate>()
        {
            new Coordinate(1,0),
            new Coordinate(1,5),
            new Coordinate(2,3.5),
            new Coordinate(4,3.5),
            new Coordinate(5,5),
            new Coordinate(5,0)
        };

        // Test 7
        public static List<Coordinate> Test7CoordList = new List<Coordinate>()
        {
            new Coordinate(1,0),
            new Coordinate(1,5),
            new Coordinate(2,3),
            new Coordinate(4,3),
            new Coordinate(5,5),
            new Coordinate(5,0)
        };

        // Test 8
        public static List<Coordinate> Test8CoordList = new List<Coordinate>()
        {
            new Coordinate(1, 0),
            new Coordinate(1, 5),
            new Coordinate(2, 2.5),
            new Coordinate(4, 2.5),
            new Coordinate(5, 5),
            new Coordinate(5, 0)
        };

        // Test 9
        public static List<Coordinate> Test9CoordList = new List<Coordinate>()
        {
            new Coordinate(1,0),
            new Coordinate(1,5),
            new Coordinate(2,3),
            new Coordinate(4,3),
            new Coordinate(5,5),
            new Coordinate(6,1),
            new Coordinate(7,1),
            new Coordinate(7,2),
            new Coordinate(8,2.5),
            new Coordinate(8,1),
            new Coordinate(9,1),
            new Coordinate(9, 5),
            new Coordinate(10, 3.5),
            new Coordinate(11, 3.5),
            new Coordinate(12,2),
            new Coordinate(12,0)
        };

        // Test 10
        public static List<Coordinate> Test10CoordList = new List<Coordinate>()
        {
            new Coordinate(2,0),
            new Coordinate(2,5),
            new Coordinate(3,5),
            new Coordinate(5,2.5),
            new Coordinate(5,1),
            new Coordinate(6,1),
            new Coordinate(6,2.5),
            new Coordinate(4,5),
            new Coordinate(7,5),
            new Coordinate(7,0)
        };


        // Test 11
        public static List<Coordinate> Test11CoordList = new List<Coordinate>()
        {
            new Coordinate(1,1),
            new Coordinate(1,5),
            new Coordinate(8,5),
            new Coordinate(8,3.75),
            new Coordinate(4,3.75),
            new Coordinate(4,2.25),
            new Coordinate(8,2.25),
            new Coordinate(8,1)
        };

        // Test 12
        public static List<Coordinate> Test12CoordList = new List<Coordinate>()
        {
            new Coordinate(1,1),
            new Coordinate(1,2.25),
            new Coordinate(6,2.5),
            new Coordinate(10,2.5),
            new Coordinate(10,3.25),
            new Coordinate(8,3.25),
            new Coordinate(8,5),
            new Coordinate(14,5),
            new Coordinate(14,1)
        }; 

        public static List<List<Coordinate>> TestCoordLists = new List<List<Coordinate>>()
        {
            Test1CoordList,
            Test2CoordList,
            Test3CoordList,
            Test4CoordList,
            Test5CoordList,
            Test6CoordList,
            Test7CoordList,
            Test8CoordList,
            Test9CoordList,
            Test10CoordList,
            Test11CoordList,
            Test12CoordList
        };

        private static GraphicsLayoutArea TestCaseGraphicsLayoutArea(GraphicsWindow window, GraphicsPage page, int testNmbr, double scale)
        {
            List<Coordinate> coordList = genScaledCoordList(TestCoordLists[testNmbr], scale);

            GraphicsDirectedPolygon directedPolygon = new GraphicsDirectedPolygon(window, page, coordList);

            return new GraphicsLayoutArea(
                window
                , page
                , directedPolygon);
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
