
namespace CanvasLib
{
    using System;
    using Graphics;
    using Geometry;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    using System.Drawing;

    public class ScaleLineMarker
    {
        Shape[] shapeElements;

        double diagOffset;
        double x;
        double y;

        double width;

        Func<Coordinate, double, Color, Shape> drawCircle;
        Func< double, double, double, double, Shape> drawLine;

        public ScaleLineMarker(
            double x,
            double y,
            double width,
            Func<Coordinate, double, Color, Shape> drawCirle,
            Func<double, double, double, double, Shape> drawLine)
        {
            this.x = x;
            this.y = y;

            this.width = width;

            diagOffset = width / MathUtils.CSqrt2;

            this.drawCircle = drawCirle;
            this.drawLine = drawLine;
        }

        internal void Draw()
        {
            shapeElements = new Shape[3];

            shapeElements[0] = drawCircle(new Coordinate(x, y), width, Color.Red);
            shapeElements[1] = drawLine(x - diagOffset, y - diagOffset, x + diagOffset, y + diagOffset);
            shapeElements[2] = drawLine(x - diagOffset, y + diagOffset, x + diagOffset, y - diagOffset);

            VisioInterop.SetFillOpacity(shapeElements[0], 0);

            for (int i = 1; i < 3; i++)
            {
                VisioInterop.SetBaseLineColor(shapeElements[i], Color.Red);
            }     
        }

        internal void Delete()
        {
            if (shapeElements == null)
            {
                return;
            }

            for (int i = 0; i < shapeElements.Length; i++)
            {
                Shape shape = shapeElements[i];

                if (shape != null)
                {
                    VisioInterop.DeleteShape(shape);
                }
            }

            shapeElements = null;
        }
    }
}
