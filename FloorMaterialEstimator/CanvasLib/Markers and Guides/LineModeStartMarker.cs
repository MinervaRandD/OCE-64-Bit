
namespace CanvasLib.Markers_and_Guides
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    using Utilities;

    public class LineModeStartMarker
    {
        GraphicShape circle1;
        GraphicShape circle2;

        double x;
        double y;

        double radius;

        double radius1;
        double radius2;

        Color color;

        StartMarkerType startMarkerType;

        public string MarkerLineGuid = null;

        public Coordinate coord;

        public LineModeStartMarker(double x, double y, double radius, Color color, StartMarkerType startMarkerType = StartMarkerType.Single)
        {
            this.x = x;
            this.y = y;

            coord = new Coordinate(x, y);

            this.radius = radius;

            this.color = color;

            this.MarkerLineGuid = GuidMaintenance.CreateGuid(this);

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

        public void Draw(GraphicsPage graphicsPage)
        {
            circle1 = graphicsPage.DrawCircle(this, new Coordinate(x, y), radius1, Color.Black);

            circle1.VisioShape.Data1 = "Start Marker 1";

            circle1.SetFillOpacity(0);
            circle1.SetLineColor(color);
            circle1.SetLineWidth(2);

            if (startMarkerType != StartMarkerType.Double)
            {
                return;
            }

            circle2 = graphicsPage.DrawCircle(this, new Coordinate(x, y), radius2, Color.Black);

            circle1.VisioShape.Data1 = "Start Marker 2";

            circle2.SetFillOpacity(0);
            circle2.SetLineColor(color);
            circle2.SetLineWidth(2);
        }

        public void Undraw()
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

        public void Delete()
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
