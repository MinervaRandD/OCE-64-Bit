

namespace TestDriverDirectedPolygonIntersectionOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    

    public static class TestCases
    {
        static List<Coordinate> cl1 = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(1,0),
            new Coordinate(1,2),
            new Coordinate(0,2),
            new Coordinate(1,1),
            new Coordinate(0,0)
        };

        public static DirectedPolygon dp1 = new DirectedPolygon(GenLineList(cl1));

        public static List<DirectedLine> GenLineList(List<Coordinate> coordList)
        {
            List<DirectedLine> outpList = new List<DirectedLine>();

            for (int i = 0; i < coordList.Count-1; i++)
            {
                outpList.Add(new DirectedLine(coordList[i], coordList[i + 1]));
            }

            return outpList;
        }

        static List<Coordinate> cl2 = new List<Coordinate>()
        {
            new Coordinate(0,0),
            new Coordinate(.999999,0),
            new Coordinate(.999999,2),
            new Coordinate(0,2),
            new Coordinate(0,0)
        };

        public static DirectedPolygon dp2 = new DirectedPolygon(GenLineList(cl2));
    }
}
