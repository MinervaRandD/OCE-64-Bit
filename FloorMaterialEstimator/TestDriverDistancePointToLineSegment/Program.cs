using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace TestDriverDistancePointToLineSegment
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectedLine line1 = new DirectedLine(1, 1, 2, 2);

            Coordinate point1 = new Coordinate(0, 2);

            double dist1 = GeometryUtils.DistancePointToLineSegment(point1, line1);

            DirectedLine line2 = new DirectedLine(1, 1, 1, 2);

            Coordinate point2 = new Coordinate(2, 1);

            double dist2 = GeometryUtils.DistancePointToLineSegment(point2, line2);

            DirectedLine line3 = new DirectedLine(1, 1, 1, 2);

            Coordinate point3 = new Coordinate(0, 0);

            double dist3 = GeometryUtils.DistancePointToLineSegment(point3, line3);

            DirectedLine line4 = new DirectedLine(1, 1, 1, 2);

            Coordinate point4 = new Coordinate(0, 3);

            double dist4 = GeometryUtils.DistancePointToLineSegment(point4, line4);

            DirectedLine line5 = new DirectedLine(2, 2, 1, 1);

            Coordinate point5 = new Coordinate(0, 3);

            double dist5 = GeometryUtils.DistancePointToLineSegment(point5, line5);
        }
    }
}
