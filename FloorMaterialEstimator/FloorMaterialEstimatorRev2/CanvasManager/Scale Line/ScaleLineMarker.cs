
namespace CanvasLib.Scale_Line
{
    using Graphics;
    using Geometry;
    using Utilities;

    using FloorMaterialEstimator.CanvasManager;

    using Visio = Microsoft.Office.Interop.Visio;

    using System.Drawing;

    public class ScaleLineMarker
    {
        Shape[] shapeElements;

        double diagOffset;
        double x;
        double y;

        double width;

        public ScaleLineMarker(double x, double y, double width)
        {
            this.x = x;
            this.y = y;

            this.width = width;

            diagOffset = width / MathUtils.CSqrt2;
        }

        internal void Draw(CanvasPage canvasPage)
        {
            shapeElements = new Shape[3];

            shapeElements[0] = canvasPage.DrawCircle(new Coordinate(x, y), width, Color.Red);
            shapeElements[1] = canvasPage.DrawLine(x - diagOffset, y - diagOffset, x + diagOffset, y + diagOffset);
            shapeElements[2] = canvasPage.DrawLine(x - diagOffset, y + diagOffset, x + diagOffset, y - diagOffset);

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
