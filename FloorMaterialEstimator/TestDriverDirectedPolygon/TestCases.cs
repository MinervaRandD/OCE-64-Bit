using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Geometry;

namespace TestDriverClipperLibOps
{
    public class TestCases
    {
        Coordinate coord11 = new Coordinate(0, 0);
        Coordinate coord12 = new Coordinate(0, 10);
        Coordinate coord13 = new Coordinate(10, 10);
        Coordinate coord14 = new Coordinate(10, 0);

        Coordinate coord21 = new Coordinate(2, 2);
        Coordinate coord22 = new Coordinate(2, 8);
        Coordinate coord23 = new Coordinate(8, 8);
        Coordinate coord24 = new Coordinate(8, 2);

        Coordinate coord31 = new Coordinate(4, 0);
        Coordinate coord32 = new Coordinate(4, 10);
        Coordinate coord33 = new Coordinate(6, 10);
        Coordinate coord34 = new Coordinate(6, 0);

        public DirectedPolygon dp1()
        {
            return polyGen(coord11, coord12, coord13, coord14);
        }

        public DirectedPolygon dp2()
        {
            return polyGen(coord21, coord22, coord23, coord24);
        }

        public DirectedPolygon dp3()
        {
            return polyGen(coord31, coord32, coord33, coord34);
        }

        private DirectedPolygon polyGen(Coordinate coord1, Coordinate coord2, Coordinate coord3, Coordinate coord4)
        {
            DirectedLine l1 = new DirectedLine(coord1, coord2);
            DirectedLine l2 = new DirectedLine(coord2, coord3);
            DirectedLine l3 = new DirectedLine(coord3, coord4);
            DirectedLine l4 = new DirectedLine(coord4, coord1);

            return new DirectedPolygon(new List<DirectedLine>() { l1, l2, l3, l4 });
        }

    }
}
