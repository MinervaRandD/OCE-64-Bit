using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    using Geometry;

    public class DirectedLineGeometricSerializable
    {
        public CoordinateSerializable Coord1 { get; set; }

        public CoordinateSerializable Coord2 { get; set; }

        public bool IsSeamable { get; set; }

        public DirectedLineGeometricSerializable() { }

        public DirectedLineGeometricSerializable(DirectedLine directedLine)
        {
            Coord1 = new CoordinateSerializable(directedLine.Coord1);

            Coord2 = new CoordinateSerializable(directedLine.Coord2);

            IsSeamable = directedLine.IsSeamable;
        }

        public DirectedLine Deserialize()
        {
            Coordinate coord1 = Coord1.Deserialize();

            Coordinate coord2 = Coord2.Deserialize();

            DirectedLine directedLine = new DirectedLine(coord1, coord2);

            directedLine.IsSeamable = IsSeamable;

            return directedLine;
        }
    }
}
