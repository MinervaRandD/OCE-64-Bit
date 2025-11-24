

namespace TestDriverDirectedPolygonIntersectionOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;

    class Program
    {
        static void Main(string[] args)
        {
            List<DirectedPolygon> results = GeometryUtils.Intersection(TestCases.dp2, TestCases.dp1);
        }
    }
}
