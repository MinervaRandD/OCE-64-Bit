
namespace CanvasLib.Markers_and_Guides
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;
    using Geometry;

    using Visio = Microsoft.Office.Interop.Visio;

    public class PlaceMarker: IGraphicsShape
    {
        List<GraphicShape> lineList = new List<GraphicShape>();

        double x;
        double y;
        double width;

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public GraphicShape Shape { get; set; }


        public GraphicsLayerBase GraphicsLayer
        {
            get
            {
                return Shape.SingleGraphicsLayer;
            }

            set
            {
                if (Shape is null)
                {
                    throw new NotImplementedException();
                }

                Shape.AddToLayerSet(value);

            }
        }


        public ShapeType ShapeType => ShapeType.PlaceMarker;

        public string Guid => Shape.Guid;

        public PlaceMarker(GraphicsWindow window, GraphicsPage page, double x, double y, double width)
        {
            this.Window = window;
            this.Page = page;

            this.x = x;
            this.y = y;
            this.width = width;
        }

        public void Draw()
        {
            lineList.Clear();

            lineList.Add(Page.DrawCircle(this, new Coordinate(x, y), width, Color.FromArgb(0, 255, 255, 255)));

            lineList.Add(Page.DrawLine(this, "Marker-H", x - width * 0.5, y, x + width * 0.5, y));
            lineList.Add(Page.DrawLine(this, "Marker-V", x, y - width * 0.5, x, y + width * 0.5));

            VisioInterop.SetFillOpacity(lineList[0], 0);
            VisioInterop.SetBaseLineOpacity(lineList[0], 0);

            VisioInterop.SetBaseLineColor(lineList[1], Color.Red);
            VisioInterop.SetBaseLineColor(lineList[2], Color.Red);

            VisioInterop.SetLineWidth(lineList[1], 2);
            VisioInterop.SetLineWidth(lineList[2], 2);

            Shape = Page.GroupShapes(Window, Page, lineList);

            VisioInterop.SetShapeData(Shape, "PlaceMarker", "Composite Shape", Shape.Guid);

            
        }

        public void Delete()
        {
            lineList.Clear();

            VisioInterop.DeleteShape(Shape);

            Shape = null;
        }
    }
}
