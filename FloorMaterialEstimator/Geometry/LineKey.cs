using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class LineKey: Tuple<Coordinate, Coordinate>
    {
        public LineKey(Coordinate coord1, Coordinate coord2) : base(coord1, coord2) { }
    }
}
