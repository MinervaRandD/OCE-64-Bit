using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class RectangleSerializable
    {
        public CoordinateSerializable UpperLeft { get; set; }

        public CoordinateSerializable LowerRght { get; set; }

        public CoordinateSerializable Offset { get; set; }

        public double Angle { get; set; }

        public RectangleSerializable() { }

        public RectangleSerializable(Rectangle rectangle)
        {
            UpperLeft = new CoordinateSerializable(rectangle.UpperLeft);

            LowerRght = new CoordinateSerializable(rectangle.LowerRght);

            Offset = new CoordinateSerializable(rectangle.Offset);

            Angle = rectangle.Angle;
        }

        public Rectangle Deserialize()
        {
            Rectangle rectangle = new Rectangle(this.UpperLeft.Deserialize(), this.LowerRght.Deserialize());

            rectangle.Offset = this.Offset.Deserialize();

            rectangle.Angle = this.Angle;

            return rectangle;
        }

    }
}
