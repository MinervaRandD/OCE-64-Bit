using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Geometry;
using ClipperLib;

namespace TestDriverClipperLibOps
{
    class Program
    {

        static TestCases testCases = new TestCases();

        static void Main(string[] args)
        {
            DirectedPolygon dp1 = testCases.dp1();
            DirectedPolygon dp2 = testCases.dp2();
            DirectedPolygon dp3 = testCases.dp2();

            Clipper clipper = new Clipper();

            List<List<IntPoint>> solutions = new List<List<IntPoint>>();

            List<IntPoint> pClip1 = GeometryUtils.DirectedPolygonToPolygon(dp1);
            List<IntPoint> pClip2 = GeometryUtils.DirectedPolygonToPolygon(dp2);
            List<IntPoint> pClip3 = GeometryUtils.DirectedPolygonToPolygon(dp3);

            clipper.AddPath(pClip1, PolyType.ptSubject, true);
            //clipper.AddPath(pClip2, PolyType.ptSubject, true);
            clipper.AddPath(pClip3, PolyType.ptClip, true);

            solutions.Clear();

            bool succeeded = clipper.Execute(ClipType.ctXor, solutions, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            
        }
    }
}
