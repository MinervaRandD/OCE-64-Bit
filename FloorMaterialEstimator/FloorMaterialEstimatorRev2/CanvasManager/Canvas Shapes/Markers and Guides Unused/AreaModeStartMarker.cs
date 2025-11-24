
namespace FloorMateralEstimator.CanvasManager
{
    using Graphics;
    using Geometry;

    using System.Drawing;
    using FloorMaterialEstimator.CanvasManager;

    public class AreaModeStartMarker
    {
        Shape circle;
  
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

        internal void Draw(CanvasPage canvasPage)
        {
            circle = canvasPage.DrawCircle(new Coordinate(x, y), radius, Color.Black);

            circle.VisioShape.Data1 = "Area Mode Start Marker";

            VisioInterop.SetFillOpacity(circle, 0);
            VisioInterop.SetBaseLineColor(circle, color);
            VisioInterop.SetLineWidth(circle, 2);
        }

        internal void Delete()
        {
            if (!(circle is null))
            {
                circle.Delete();
                circle = null;
            }

        }
    }
}
