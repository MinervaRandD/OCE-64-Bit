
namespace CanvasLib.Borders
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    public class BorderGenerationMarker
    {
        Shape[] shapeElements;

        double x;
        double y;

        double width;

        public BorderGenerationMarker(double x, double y, double width)
        {
            this.x = x;
            this.y = y;

            this.width = width;
        }

        internal void Draw(GraphicsPage graphicsPage)
        {
            shapeElements = new Shape[2];

            shapeElements[0] = DrawOuterSquare(graphicsPage);

            shapeElements[1] = DrawInnerCircle(graphicsPage);
        }

        private Shape DrawOuterSquare(GraphicsPage graphicsPage)
        {
            double x1 = x - 0.5 * width;
            double x2 = x + 0.5 * width;

            double y1 = y - 0.5 * width;
            double y2 = y + 0.5 * width;

            return graphicsPage.DrawRectangle(x1, y1, x2, y2);
        }

        private Shape DrawInnerCircle(GraphicsPage graphicsPage)
        {
            double x1 = x - 0.25 * width;
            double x2 = x + 0.25 * width;

            double y1 = y - 0.25 * width;
            double y2 = y + 0.25 * width;

            return graphicsPage.DrawCircle(new Coordinate(x, y), width / 4.0, Color.Black);
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
