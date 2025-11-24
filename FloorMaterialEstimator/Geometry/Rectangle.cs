using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geometry
{
    public class Rectangle
    {
   
        public double Angle { get; set; } = 0;

        public Coordinate Offset { get; set; } = new Coordinate(0, 0);

        public Coordinate UpperLeft { get; set; }

        public Coordinate LowerRght { get; set; }

        public Coordinate UpperRght => new Coordinate(LowerRght.X, UpperLeft.Y);

        public Coordinate LowerLeft => new Coordinate(UpperLeft.X, LowerRght.Y);

        public double Height => (UpperLeft.Y - LowerRght.Y);

        public double Width => (LowerRght.X - UpperLeft.X);

        // Not entirely accurate because it doesn't account for rotation, but this is only used for debugging at this point.
        public double MinX => this.Offset.X;

        // Not entirely accurate because it doesn't account for rotation, but this is only used for debugging at this point.
        public double MinY => this.Offset.Y;

        public Coordinate Center
        {
            get
            {
                double x = (UpperLeft.X + LowerRght.X) * 0.5;
                double y = (UpperLeft.Y + LowerRght.Y) * 0.5;

                return new Coordinate(x, y);

            }
        }
        public Rectangle() { }

        public Rectangle(Coordinate upperLeft, Coordinate lowerRght)
        {
            UpperLeft = upperLeft;
            LowerRght = lowerRght;
        }

        public Rectangle(Coordinate upperLeft, Coordinate lowerRght, double atan)
        {
            UpperLeft = upperLeft;
            LowerRght = lowerRght;

            this.Angle = atan;
        }


        public static explicit operator DirectedPolygon(Rectangle rectangle)
        {
            List<DirectedLine> lineList = new List<DirectedLine>()
            {
                new DirectedLine(rectangle.UpperLeft, rectangle.UpperRght),
                new DirectedLine(rectangle.UpperRght, rectangle.LowerRght),
                new DirectedLine(rectangle.LowerRght, rectangle.LowerLeft),
                new DirectedLine(rectangle.LowerLeft, rectangle.UpperLeft)
            };

            DirectedPolygon directedPolygon = new DirectedPolygon(lineList);

            directedPolygon.Rotate(-rectangle.Angle);

            directedPolygon.Translate(rectangle.Offset);

            return directedPolygon;
        }

        public void Translate(Coordinate offset)
        {
            this.Offset = offset;
        }

        public void Rotate(double angle)
        {
            this.Angle = angle;
        }

        public double AreaInSqrInches(double scaleFactor) => Width * Height * scaleFactor * scaleFactor;

        public Rectangle Clone()
        {
            Rectangle clonedRectangle = new Rectangle(UpperLeft, LowerRght)
            {
                Angle = this.Angle,
                Offset = this.Offset
            };

            return clonedRectangle;
        }

    }
}
