

namespace UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities;
    using Geometry;
    using System.Collections.Generic;

    [TestClass]
    public class GeometryUnitTests
    {
        [TestMethod]
        public void TestLineContainsCoord()
        {
            Coordinate coord11 = new Coordinate(0, 0);
            Coordinate coord12 = new Coordinate(1, 1);

            DirectedLine line_1 = new DirectedLine(coord11, coord12);

            bool test11 = line_1.Contains(new Coordinate(0, 0));
            bool test12 = line_1.Contains(new Coordinate(1, 1));

            Assert.IsTrue(test11);
            Assert.IsTrue(test12);

            bool test13 = line_1.ProperContains(new Coordinate(0, 0));
            bool test14 = line_1.ProperContains(new Coordinate(1, 1));

            Assert.IsFalse(test13);
            Assert.IsFalse(test14);

            bool test15 = line_1.Contains(new Coordinate(0.5, 0.5));
            bool test16 = line_1.ProperContains(new Coordinate(0.5, 0.5));

            Assert.IsTrue(test15);
            Assert.IsTrue(test16);

            bool test17 = line_1.Contains(new Coordinate(0.25, 0.5));
            bool test18 = line_1.ProperContains(new Coordinate(0.25, 0.5));

            Assert.IsFalse(test17);
            Assert.IsFalse(test18);

            Coordinate coord21 = new Coordinate(0, 0);
            Coordinate coord22 = new Coordinate(0, 1);

            DirectedLine line_2 = new DirectedLine(coord21, coord22);

            bool test19 = line_2.Contains(new Coordinate(0, 0.5));
            bool test20 = line_2.Contains(new Coordinate(0.25, 0.5));

            Assert.IsTrue(test19);
            Assert.IsFalse(test20);

            Coordinate coord31 = new Coordinate(0, 0);
            Coordinate coord32 = new Coordinate(1, 0);

            DirectedLine line_3 = new DirectedLine(coord31, coord32);

            bool test21 = line_3.Contains(new Coordinate(0.5, 0.0));
            bool test22 = line_3.Contains(new Coordinate(0.25, 0.5));

            Assert.IsTrue(test21);
            Assert.IsFalse(test22);
        }

        [TestMethod]
        public void TestLineSegmentsOverlap()
        {
            Coordinate coord11 = new Coordinate(0, 0);
            Coordinate coord12 = new Coordinate(1, 1);

            DirectedLine line_1 = new DirectedLine(coord11, coord12);

            Coordinate coord21 = new Coordinate(0.5, 0.5);
            Coordinate coord22 = new Coordinate(1.5, 1.5);

            DirectedLine line_2 = new DirectedLine(coord21, coord22);

            bool test12_1 = line_1.Overlaps(line_2);

            Assert.IsTrue(test12_1);

            bool test12_2 = line_1.ProperOverlaps(line_2);

            Assert.IsTrue(test12_2);

            bool test12_3 = line_2.Overlaps(line_1);

            Assert.IsTrue(test12_3);

            bool test12_4 = line_2.ProperOverlaps(line_1);

            Assert.IsTrue(test12_4);

            Coordinate coord31 = new Coordinate(1.5, 1.5);
            Coordinate coord32 = new Coordinate(2.5, 2.5);

            DirectedLine line_3 = new DirectedLine(coord31, coord32);

            bool test31_1 = line_1.Overlaps(line_3);

            Assert.IsFalse(test31_1);

            bool test31_2 = line_1.ProperOverlaps(line_3);

            Assert.IsFalse(test31_2);

            bool test31_3 = line_3.Overlaps(line_1);

            Assert.IsFalse(test31_1);

            bool test31_4 = line_3.ProperOverlaps(line_1);

            Assert.IsFalse(test31_2);

            Coordinate coord41 = new Coordinate(1, 1);
            Coordinate coord42 = new Coordinate(2, 2);

            DirectedLine line_4 = new DirectedLine(coord41, coord42);

            bool test41_1 = line_1.Overlaps(line_4);

            Assert.IsTrue(test41_1);

            bool test41_2 = line_1.ProperOverlaps(line_4);

            Assert.IsFalse(test41_2);

            bool test41_3 = line_4.Overlaps(line_1);

            Assert.IsTrue(test41_3);

            bool test41_4 = line_4.ProperOverlaps(line_1);

            Assert.IsFalse(test41_4);

            Coordinate coord51 = new Coordinate(2, 2);
            Coordinate coord52 = new Coordinate(1, 1);

            DirectedLine line_5 = new DirectedLine(coord51, coord52);

            bool test51_1 = line_1.Overlaps(line_5);

            Assert.IsTrue(test51_1);

            bool test51_2 = line_1.ProperOverlaps(line_5);

            Assert.IsFalse(test51_2);

            bool test51_3 = line_5.Overlaps(line_1);

            Assert.IsTrue(test51_3);

            bool test51_4 = line_5.ProperOverlaps(line_1);

            Assert.IsFalse(test51_4);
        }


        [TestMethod]
        public void TestDirectedPolylineValidPerimeter()
        {
            Coordinate coord1 = new Coordinate(0, 0);
            Coordinate coord2 = new Coordinate(1, 0);
            Coordinate coord3 = new Coordinate(1, 1);
            Coordinate coord4 = new Coordinate(0, 1);

            DirectedPolygon directedPolygon1 = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord3, coord4 });
            DirectedPolygon directedPolygon2 = new DirectedPolygon(new List<Coordinate>() { coord4, coord3, coord2, coord1 });

            bool test1 = directedPolygon1.HasValidPerimeter();
            bool test2 = directedPolygon2.HasValidPerimeter();

            Assert.IsTrue(test1);
            Assert.IsTrue(test2);

            DirectedLine line1 = new DirectedLine(coord1, coord2);
            DirectedLine line2 = new DirectedLine(coord2, coord3);
            DirectedLine line3 = new DirectedLine(coord3, coord4);
            DirectedLine line4 = new DirectedLine(coord4, coord1);

            DirectedLine line1b = new DirectedLine(coord2, coord1);
            DirectedLine line2b = new DirectedLine(coord3, coord2);
            DirectedLine line3b = new DirectedLine(coord4, coord3);
            DirectedLine line4b = new DirectedLine(coord1, coord4);

            DirectedPolygon directedPolygon3 = new DirectedPolygon(new List<DirectedLine>() { line1, line2, line3, line4 }, false); ;
            DirectedPolygon directedPolygon4 = new DirectedPolygon(new List<DirectedLine>() { line4b, line3b, line2b, line1b }, false);


            bool test3 = directedPolygon3.HasValidPerimeter();
            bool test4 = directedPolygon4.HasValidPerimeter();

            Assert.IsTrue(test3);
            Assert.IsTrue(test4);

            DirectedPolygon directedPolygon5 = new DirectedPolygon(new List<DirectedLine>() { line1, line1, line2, line2 }, false);
            DirectedPolygon directedPolygon6 = new DirectedPolygon(new List<DirectedLine>() { line4, line2, line1 }, false);

            bool test5 = directedPolygon5.HasValidPerimeter();
            bool test6 = directedPolygon6.HasValidPerimeter();

            Assert.IsFalse(test5);
            Assert.IsFalse(test6);

            DirectedPolygon directedPolygon7 = new DirectedPolygon(new List<Coordinate>() { coord1, coord1, coord1, coord1 });
            DirectedPolygon directedPolygon8 = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord2, coord1 });

            bool test7 = directedPolygon7.HasValidPerimeter();
            bool test8 = directedPolygon8.HasValidPerimeter();

            Assert.IsFalse(test7);
            Assert.IsFalse(test8);


        }

        [TestMethod]
        public void TestDirectedLineIntersection()
        {
            Coordinate coord1 = new Coordinate(0, 0);
            Coordinate coord2 = new Coordinate(1, 0);
            Coordinate coord3 = new Coordinate(1, 1);
            Coordinate coord4 = new Coordinate(0, 1);

            DirectedLine line1 = new DirectedLine(coord1, coord2);
            DirectedLine line2 = new DirectedLine(coord2, coord3);
            DirectedLine line3 = new DirectedLine(coord3, coord4);
            DirectedLine line4 = new DirectedLine(coord4, coord1);

            Coordinate testCoord12 = line1.Intersect(line2, true);
            Coordinate testCoord13 = line1.Intersect(line3, true);

            Assert.IsTrue(testCoord12 == new Coordinate(1, 0));
            Assert.IsTrue(Coordinate.IsNullCoordinate(testCoord13));

            Coordinate coord5 = new Coordinate(0, 0.5);
            Coordinate coord6 = new Coordinate(1, 0.5);

            DirectedLine line5 = new DirectedLine(coord5, coord6);
            DirectedLine line6 = new DirectedLine(coord6, coord5);

            Coordinate testCoord25 = line2.Intersect(line5, true);
            Coordinate testCoord26 = line2.Intersect(line6, true);

            Assert.IsTrue(testCoord25 == new Coordinate(1, 0.5));
            Assert.IsTrue(testCoord25 == new Coordinate(1, 0.5));
        }

        [TestMethod]
        public void TestDirectedPolylineIntersectingLines()
        {
            Coordinate coord1 = new Coordinate(0, 0);
            Coordinate coord2 = new Coordinate(1, 0);
            Coordinate coord3 = new Coordinate(1, 1);
            Coordinate coord4 = new Coordinate(0, 1);

            DirectedPolygon directedPolygon1 = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord3, coord4 });
            DirectedPolygon directedPolygon2 = new DirectedPolygon(new List<Coordinate>() { coord4, coord3, coord2, coord1 });

            bool test1 = directedPolygon1.HasIntersectingLines();
            bool test2 = directedPolygon2.HasIntersectingLines();

            Assert.IsFalse(test1);
            Assert.IsFalse(test2);
#if false
            DirectedLine line1 = new DirectedLine(coord1, coord2);
            DirectedLine line2 = new DirectedLine(coord2, coord3);
            DirectedLine line3 = new DirectedLine(coord3, coord4);
            DirectedLine line4 = new DirectedLine(coord4, coord1);

            DirectedLine line1b = new DirectedLine(coord2, coord1);
            DirectedLine line2b = new DirectedLine(coord3, coord2);
            DirectedLine line3b = new DirectedLine(coord4, coord3);
            DirectedLine line4b = new DirectedLine(coord1, coord4);

            DirectedPolygon directedPolygon3 = new DirectedPolygon(new List<DirectedLine>() { line1, line2, line3, line4 }, false); ;
            DirectedPolygon directedPolygon4 = new DirectedPolygon(new List<DirectedLine>() { line4b, line3b, line2b, line1b }, false);


            bool test3 = directedPolygon3.HasValidPerimeter();
            bool test4 = directedPolygon4.HasValidPerimeter();

            Assert.IsTrue(test3);
            Assert.IsTrue(test4);

            DirectedPolygon directedPolygon5 = new DirectedPolygon(new List<DirectedLine>() { line1, line1, line2, line2 }, false);
            DirectedPolygon directedPolygon6 = new DirectedPolygon(new List<DirectedLine>() { line4, line2, line1 }, false);

            bool test5 = directedPolygon5.HasValidPerimeter();
            bool test6 = directedPolygon6.HasValidPerimeter();

            Assert.IsFalse(test5);
            Assert.IsFalse(test6);

            DirectedPolygon directedPolygon7 = new DirectedPolygon(new List<Coordinate>() { coord1, coord1, coord1, coord1 });
            DirectedPolygon directedPolygon8 = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord2, coord1 });

            bool test7 = directedPolygon7.HasValidPerimeter();
            bool test8 = directedPolygon8.HasValidPerimeter();

            Assert.IsFalse(test7);
            Assert.IsFalse(test8);
#endif

        }
    }
}
