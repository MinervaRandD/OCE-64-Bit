using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class CoordinateSerializable
    {
        public double X { get; set; }

        public double Y { get; set; }
        
        public CoordinateSerializable() { }

        public CoordinateSerializable(Coordinate coordinate)
        {
            this.X = coordinate.X;
            this.Y = coordinate.Y;
        }

        public CoordinateSerializable(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Coordinate Deserialize()
        {
            return new Coordinate(X, Y);
        }
    }
}
