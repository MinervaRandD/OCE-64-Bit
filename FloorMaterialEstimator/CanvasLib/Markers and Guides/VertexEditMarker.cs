
namespace CanvasLib.Markers_and_Guides
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    using Utilities;
    using System;

    public class VertexEditMarker
    {
        GraphicShape circle;

        double x;
        double y;

        double radius = 0.075;

        Color color = Color.Red;

        public Coordinate coord;

        GraphicsWindow window;

        GraphicsPage page;

        public VertexEditMarker(GraphicsWindow window, GraphicsPage page)
        {
            this.window = window;
            this.page = page;
        }

        public void Draw(double x, double y)
        {
            this.x = x;
            this.y = y;

            circle = page.DrawCircle(this, new Coordinate(x, y), radius, color);

            circle.VisioShape.Data1 = "Vertex Edit Marker";

            circle.SetFillOpacity(0);
            circle.SetLineColor(color);
            circle.SetFillColor(color);
            circle.SetLineWidth(2);

            window.DeselectAll();
        }

        public void Undraw()
        {
            if (!(circle is null))
            {
                circle.Delete();
                circle = null;
            }
        }

        public void Delete()
        {
            if (!(circle is null))
            {
                circle.Delete();
                circle = null;
            }
        }

        public void MoveTo(double xNew, double yNew)
        {
            VisioInterop.SetShapePinLocation(this.circle, xNew, yNew);

           // VisioInterop.MoveShape(Window, circle, xNew - x, yNew - y);

            x = xNew;
            y = yNew;
        }
    }
}
