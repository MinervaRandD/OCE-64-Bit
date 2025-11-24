
namespace FloorMaterialEstimator.Test_and_Debug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Seams_And_Cuts;

    public static class TestOverages
    {
        public static void Test1()
        {
            CutLineSegment cls1 = new CutLineSegment(null, 1, new Coordinate(0, 0), new Coordinate(6, 0));
            CutLineSegment cls2 = new CutLineSegment(null, 2, new Coordinate(6, 0), new Coordinate(10, -1));
            CutLineSegment cls3 = new CutLineSegment(null, 3, new Coordinate(0, -1), new Coordinate(10, -1));
            CutLineSegment cls4 = new CutLineSegment(null, 3, new Coordinate(0, 0), new Coordinate(0, -1));

            List<CutLineSegment> cutLineSegmentList = new List<CutLineSegment>()
            {
                cls1, cls2, cls3, cls4
            };

            CutPolygon cutPolygon = new CutPolygon(cutLineSegmentList);

            cutPolygon.GenerateBoundaries(0, -1);

            Cut cut = new Cut(cutPolygon, 0, -1);

            HorizontalOverageGenerator hog = new HorizontalOverageGenerator(cut, 0.25);

            hog.GenerateOverages();
        }

        public static void Test2()
        {
            CutLineSegment cls1 = new CutLineSegment(null, 1, new Coordinate(0, 0), new Coordinate(1, -1));
            CutLineSegment cls2 = new CutLineSegment(null, 2, new Coordinate(1, -1), new Coordinate(2, 0));
            CutLineSegment cls3 = new CutLineSegment(null, 3, new Coordinate(2, 0), new Coordinate(2, -1));
            CutLineSegment cls4 = new CutLineSegment(null, 4, new Coordinate(2, -1), new Coordinate(0, -1));
            CutLineSegment cls5 = new CutLineSegment(null, 5, new Coordinate(0, -1), new Coordinate(0, 0));

            List<CutLineSegment> cutLineSegmentList = new List<CutLineSegment>()
            {
                cls1, cls2, cls3, cls4, cls5
            };

            CutPolygon cutPolygon = new CutPolygon(cutLineSegmentList);

            cutPolygon.GenerateBoundaries(0, -1);

            Cut cut = new Cut(cutPolygon, 0, -1);

            HorizontalOverageGenerator hog = new HorizontalOverageGenerator(cut, 0.25);

            hog.GenerateOverages();
        }
    }
}
