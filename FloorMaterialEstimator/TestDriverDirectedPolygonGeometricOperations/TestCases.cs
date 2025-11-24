

namespace TestDriverDirectedPolygonGeometricOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;

    public static class TestCases
    {
        static Coordinate t1c1e = new Coordinate(0, 0);
        static Coordinate t1c2e = new Coordinate(0, 10);
        static Coordinate t1c3e = new Coordinate(10, 10);
        static Coordinate t1c4e = new Coordinate(10, 0);

        static DirectedLine t1l1e = new DirectedLine(t1c1e, t1c2e);
        static DirectedLine t1l2e = new DirectedLine(t1c2e, t1c3e);
        static DirectedLine t1l3e = new DirectedLine(t1c3e, t1c4e);
        static DirectedLine t1l4e = new DirectedLine(t1c4e, t1c1e);

        public static DirectedPolygon t1pe => new DirectedPolygon(new List<DirectedLine>() { t1l1e, t1l2e, t1l3e, t1l4e });

        static Coordinate t1c1i1 = new Coordinate(2, 2);
        static Coordinate t1c2i1 = new Coordinate(2, 4);
        static Coordinate t1c3i1 = new Coordinate(4, 4);
        static Coordinate t1c4i1 = new Coordinate(4, 2);

        static DirectedLine t1l1i1 = new DirectedLine(t1c1i1, t1c2i1);
        static DirectedLine t1l2i1 = new DirectedLine(t1c2i1, t1c3i1);
        static DirectedLine t1l3i1 = new DirectedLine(t1c3i1, t1c4i1);
        static DirectedLine t1l4i1 = new DirectedLine(t1c4i1, t1c1i1);

        public static DirectedPolygon t1pi1 => new DirectedPolygon(new List<DirectedLine>() { t1l1i1, t1l2i1, t1l3i1, t1l4i1 });

        static Coordinate t1c1i2 = new Coordinate(6, 6);
        static Coordinate t1c2i2 = new Coordinate(6, 8);
        static Coordinate t1c3i2 = new Coordinate(8, 8);
        static Coordinate t1c4i2 = new Coordinate(8, 6);

        static DirectedLine t1l1i2 = new DirectedLine(t1c1i2, t1c2i2);
        static DirectedLine t1l2i2 = new DirectedLine(t1c2i2, t1c3i2);
        static DirectedLine t1l3i2 = new DirectedLine(t1c3i2, t1c4i2);
        static DirectedLine t1l4i2 = new DirectedLine(t1c4i2, t1c1i2);

        public static DirectedPolygon t1pi2 => new DirectedPolygon(new List<DirectedLine>() { t1l1i2, t1l2i2, t1l3i2, t1l4i2 });

        static Coordinate t2c1i = new Coordinate(4, 4);
        static Coordinate t2c2i = new Coordinate(4, 6);
        static Coordinate t2c3i = new Coordinate(10, 6);
        static Coordinate t2c4i = new Coordinate(10, 4);

        static DirectedLine t2l1i = new DirectedLine(t2c1i, t2c2i);
        static DirectedLine t2l2i = new DirectedLine(t2c2i, t2c3i);
        static DirectedLine t2l3i = new DirectedLine(t2c3i, t2c4i);
        static DirectedLine t2l4i = new DirectedLine(t2c4i, t2c1i);

        public static DirectedPolygon t2pi => new DirectedPolygon(new List<DirectedLine>() { t2l1i, t2l2i, t2l3i, t2l4i });

        static Coordinate txc1i = new Coordinate(4.99999, 0);
        static Coordinate txc2i = new Coordinate(4.99999, 10);
        static Coordinate txc3i = new Coordinate(5.00001, 10);
        static Coordinate txc4i = new Coordinate(5.00001, 0);

        static DirectedLine txl1i = new DirectedLine(txc1i, txc2i);
        static DirectedLine txl2i = new DirectedLine(txc2i, txc3i);
        static DirectedLine txl3i = new DirectedLine(txc3i, txc4i);
        static DirectedLine txl4i = new DirectedLine(txc4i, txc1i);

        public static DirectedPolygon tx1pi => new DirectedPolygon(new List<DirectedLine>() { txl1i, txl2i, txl3i, txl4i });

        static Coordinate tx2c1i = new Coordinate(-1, 5);
        static Coordinate tx2c2i = new Coordinate(11, 5);

        static Coordinate tx2c3i = new Coordinate(10, 5);
        static Coordinate tx2c4i = new Coordinate(10, 4);

        static Coordinate tx2c5i = new Coordinate(10, 4);
        static Coordinate tx2c6i = new Coordinate(-1, 5);

        static DirectedLine tx2i1 = new DirectedLine(tx2c1i, tx2c2i);
        static DirectedLine tx2i2 = new DirectedLine(tx2c3i, tx2c4i);
        static DirectedLine tx2i3 = new DirectedLine(tx2c5i, tx2c6i);

        public static DirectedPolygon tx2pi => new DirectedPolygon(new List<DirectedLine>() { tx2i1, tx2i2, tx2i3 });
    }
}
