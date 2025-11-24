
namespace FloorMaterialEstimator.CanvasManager
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    public class LineModeStartMarker
    {
        Shape circle1;
        Shape circle2;

        double x;
        double y;

        double radius;

        double radius1;
        double radius2;

        Color color;

        StartMarkerType startMarkerType;

        public string MarkerLineGuid = null;

        public LineModeStartMarker(double x, double y, double radius, Color color, string markerLineGuid, StartMarkerType startMarkerType = StartMarkerType.Single)
        {
            this.x = x;
            this.y = y;

            this.radius = radius;

            this.color = color;

            this.MarkerLineGuid = markerLineGuid;

            this.startMarkerType = startMarkerType;

            if (startMarkerType == StartMarkerType.Single)
            {
                radius1 = radius;
            }

            else
            {
                radius1 = radius * 0.70;
                radius2 = radius * 1.30;
            }
        }

        internal void Draw(CanvasPage canvasPage)
        {
            circle1 = canvasPage.DrawCircle(new Coordinate(x, y), radius1, Color.Black);

            circle1.VisioShape.Data1 = "Start Marker 1";

            VisioInterop.SetFillOpacity(circle1, 0);
            VisioInterop.SetBaseLineColor(circle1, color);
            VisioInterop.SetLineWidth(circle1, 2);

            if (startMarkerType != StartMarkerType.Double)
            {
                return;
            }

            circle2 = canvasPage.DrawCircle(new Coordinate(x, y), radius2, Color.Black);

            circle1.VisioShape.Data1 = "Start Marker 2";

            VisioInterop.SetFillOpacity(circle2, 0);
            VisioInterop.SetBaseLineColor(circle2, color);
            VisioInterop.SetLineWidth(circle2, 2);
        }

        internal void Delete()
        {
            if (!(circle1 is null))
            {
                circle1.Delete();
                circle1 = null;
            }

            if (!(circle2 is null))
            {
                circle2.Delete();
                circle2 = null;
            }

        }
    }

    public enum StartMarkerType
    {
        Unknown = 0,
        Single = 1,
        Double = 2
    }
}
