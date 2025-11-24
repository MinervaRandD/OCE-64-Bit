
namespace CanvasLib.Scale_Line
{
    using Graphics;
    using Geometry;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    using System.Drawing;

    public class ScaleLineMarker
    {
        GraphicShape shape;

        GraphicsWindow window;

        GraphicsPage page;

        double diagOffset;
        double x;
        double y;

        double circleWidth;
        double lineWidth;

        public ScaleLineMarker(
            GraphicsWindow window
            , GraphicsPage page
            , double x
            , double y
            , double circleWidth
            , double lineWidth)
        {
            this.window = window;

            this.page = page;

            this.x = x;
            this.y = y;

            this.circleWidth = circleWidth;
            this.lineWidth = lineWidth;

            diagOffset = this.circleWidth / MathUtils.C_Sqrt2;
        }

        internal void Draw()
        {
            GraphicShape[] shapeElements;

            shapeElements = new GraphicShape[3];

            shapeElements[0] = page.DrawCircle(this, new Coordinate(x, y), circleWidth, Color.Red);
            shapeElements[1] = page.DrawLine(this, x - diagOffset, y - diagOffset, x + diagOffset, y + diagOffset, string.Empty);
            shapeElements[2] = page.DrawLine(this, x - diagOffset, y + diagOffset, x + diagOffset, y - diagOffset, string.Empty);

            shapeElements[0].SetFillOpacity(0);
            shapeElements[0].SetLineWidth(lineWidth);

            for (int i = 1; i < 3; i++)
            {
                shapeElements[i].SetLineWidth(lineWidth);
                shapeElements[i].SetLineColor(Color.Red);
            }

            shape = VisioInterop.GroupShapes(window, shapeElements);

            string guid = GuidMaintenance.CreateGuid(shape);

            VisioInterop.SetShapeData(shape, "[ScaleLineMarker]", "Marker", guid);
        }

        internal void Delete()
        {
            if (shape is null)
            {
                return;
            }

            VisioInterop.DeleteShape(shape);

            shape = null;
        }
    }
}
