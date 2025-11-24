using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class IntersectionPoint
    {
        public Coordinate Coord;

        public Dictionary<LineKey, DirectedLine> IntersectingLineList = new Dictionary<LineKey, DirectedLine>();

        public IntersectionPoint(Coordinate coord)
        {
            this.Coord = coord;
        }
    }
}
