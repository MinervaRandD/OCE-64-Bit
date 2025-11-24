
namespace CanvasLib.Tape_Measure
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    public class TapeMeasureMarker
    {
        GraphicShape[] shapeElements;

        double diagOffset;
        double x;
        double y;

        public const double sqrt2 = 1.414213562373095;

        double markerWidth;

        double markerLineWidth;

        public TapeMeasureMarker(
            double x
            , double y
            , double markerWidth
            , double markerLineWidth)
        {
            this.x = x;
            this.y = y;

            this.markerWidth = markerWidth;

            this.markerLineWidth = markerLineWidth; 

            diagOffset = this.markerWidth / sqrt2;
        }

        internal void Draw(GraphicsPage graphicsPage)
        {
            shapeElements = new GraphicShape[3];

            shapeElements[0] = graphicsPage.DrawCircle(this, new Coordinate(x, y), markerWidth, Color.Red);
            shapeElements[1] = graphicsPage.DrawLine(this, x - diagOffset, y - diagOffset, x + diagOffset, y + diagOffset, string.Empty);
            shapeElements[2] = graphicsPage.DrawLine(this, x - diagOffset, y + diagOffset, x + diagOffset, y - diagOffset, string.Empty);

            shapeElements[0].SetFillOpacity(0);

            shapeElements[0].SetLineWidth(markerLineWidth);
            for (int i = 1; i < 3; i++)
            {
                shapeElements[i].SetLineColor(Color.Red);
                shapeElements[i].SetLineWidth(markerLineWidth);
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
                GraphicShape shape = shapeElements[i];

                if (shape != null)
                {
                    VisioInterop.DeleteShape(shape);
                }
            }

            shapeElements = null;
        }
    }
}
