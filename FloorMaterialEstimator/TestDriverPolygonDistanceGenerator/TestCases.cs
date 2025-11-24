using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Geometry;

namespace TestDriverPolygonDistanceGenerator
{
    public class TestCases
    {
        static Coordinate t1c1 = new Coordinate(4, 0);
        static Coordinate t1c2 = new Coordinate(0, 10);
        static Coordinate t1c3 = new Coordinate(6, 10);
        static Coordinate t1c4 = new Coordinate(10, 0);

        static DirectedLine t1l1 = new DirectedLine(t1c1, t1c2);
        static DirectedLine t1l2 = new DirectedLine(t1c2, t1c3);
        static DirectedLine t1l3 = new DirectedLine(t1c3, t1c4);
        static DirectedLine t1l4 = new DirectedLine(t1c4, t1c1);

        public static DirectedPolygon t1p1 => new DirectedPolygon(new List<DirectedLine>() { t1l1, t1l2, t1l3, t1l4 });

        static Coordinate t2c1 = new Coordinate(2, 0);
        static Coordinate t2c2 = new Coordinate(0, 10);
        static Coordinate t2c3 = new Coordinate(8, 10);
        static Coordinate t2c4 = new Coordinate(10, 0);

        static DirectedLine t2l1 = new DirectedLine(t2c1, t2c2);
        static DirectedLine t2l2 = new DirectedLine(t2c2, t2c3);
        static DirectedLine t2l3 = new DirectedLine(t2c3, t2c4);
        static DirectedLine t2l4 = new DirectedLine(t2c4, t2c1);

        public static DirectedPolygon t1p2 => new DirectedPolygon(new List<DirectedLine>() { t2l1, t2l2, t2l3, t2l4 });

        static Coordinate t3c1 = new Coordinate(2, 0);
        static Coordinate t3c2 = new Coordinate(1, 1);
        static Coordinate t3c3 = new Coordinate(0, 2);
        static Coordinate t3c4 = new Coordinate(0, 10);
        static Coordinate t3c5 = new Coordinate(6, 10);
        static Coordinate t3c6 = new Coordinate(4, 0);

        static DirectedLine t3l1 = new DirectedLine(t3c1, t3c2);
        static DirectedLine t3l2 = new DirectedLine(t3c2, t3c3);
        static DirectedLine t3l3 = new DirectedLine(t3c3, t3c4);
        static DirectedLine t3l4 = new DirectedLine(t3c4, t3c5);
        static DirectedLine t3l5 = new DirectedLine(t3c5, t3c6);
        static DirectedLine t3l6 = new DirectedLine(t3c6, t3c1);

        public static DirectedPolygon t1p3 => new DirectedPolygon(new List<DirectedLine>() { t3l1, t3l2, t3l3, t3l4, t3l5, t3l6 });

        static Coordinate t4c1 = new Coordinate(4, 0);
        static Coordinate t4c2 = new Coordinate(0, 5);
        static Coordinate t4c3 = new Coordinate(5, 10);
        static Coordinate t4c4 = new Coordinate(8, 10);
        static Coordinate t4c5 = new Coordinate(6, 6);
        static Coordinate t4c6 = new Coordinate(9, 1);
        static Coordinate t4c7 = new Coordinate(10, 0);

        static DirectedLine t4l1 = new DirectedLine(t4c1, t4c2);
        static DirectedLine t4l2 = new DirectedLine(t4c2, t4c3);
        static DirectedLine t4l3 = new DirectedLine(t4c3, t4c4);
        static DirectedLine t4l4 = new DirectedLine(t4c4, t4c5);
        static DirectedLine t4l5 = new DirectedLine(t4c5, t4c6);
        static DirectedLine t4l6 = new DirectedLine(t4c6, t4c7);
        static DirectedLine t4l7 = new DirectedLine(t4c7, t4c1);

        public static DirectedPolygon t1p4 => new DirectedPolygon(new List<DirectedLine>() { t4l1, t4l2, t4l3, t4l4, t4l5, t4l6, t4l7 });
    }
}
