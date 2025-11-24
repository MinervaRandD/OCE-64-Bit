
namespace CanvasLib.Markers_and_Guides
{
    using Graphics;
    using Geometry;

    using System.Drawing;
   
    public class AreaModeStartMarker
    {
        GraphicShape circle;
  
        double x;
        double y;

        double radius;

        Color color;

        public AreaModeStartMarker(double x, double y, double radius, Color color)
        {
            this.x = x;
            this.y = y;

            this.radius = radius;

            this.color = color;
        }

        public void Draw(GraphicsPage graphicsPage)
        {
            circle = graphicsPage.DrawCircle(this, new Coordinate(x, y), radius, Color.Black);

            circle.VisioShape.Data1 = "Area Mode Start Marker";

            circle.SetFillOpacity(0);
            circle.SetLineColor(color);
            circle.SetLineWidth(2);
        }

        public void Delete()
        {
            if (!(circle is null))
            {
                circle.Delete();
                circle = null;
            }

        }
    }
}
