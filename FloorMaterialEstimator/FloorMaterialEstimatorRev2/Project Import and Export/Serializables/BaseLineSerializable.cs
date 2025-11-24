using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class BaseLineSerializable
    {
        public Coordinate Coord1 { get; set; }
        public Coordinate Coord2 { get; set; }

        public BaseLineSerializable() { }
        
        public BaseLineSerializable(DirectedLine line)
        {
            this.Coord1 = line.Coord1;
            this.Coord2 = line.Coord2;
        }

        public BaseLineSerializable(Coordinate coord1, Coordinate coord2)
        {
            Coord1 = coord1;
            Coord2 = coord2;
        }
    }
}
