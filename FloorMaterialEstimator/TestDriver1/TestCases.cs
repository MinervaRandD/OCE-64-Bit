using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace TestDriverPointClosestToLine
{
    public static class TestCases
    {
        public static DirectedLine line1 = new DirectedLine(new Coordinate(0, 0), new Coordinate(1, 0));
        public static DirectedLine line2 = new DirectedLine(new Coordinate(0, 0), new Coordinate(0, 1));
        public static DirectedLine line3 = new DirectedLine(new Coordinate(1, 0), new Coordinate(2, 1));
    }
}
