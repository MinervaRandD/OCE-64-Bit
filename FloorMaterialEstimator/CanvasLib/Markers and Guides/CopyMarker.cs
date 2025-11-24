
using System;
using System.Linq;

namespace CanvasLib.Markers_and_Guides
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    using Color = System.Drawing.Color;

    public class CopyMarker: IGraphicsShape
    {
        GraphicShape[] shapeElements;

        double diagOffset;
        double x;
        double y;

        double width;

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public GraphicShape Shape { get; set; }


        private GraphicsLayerBase _graphicsLayerBase = null;

        public GraphicsLayerBase GraphicsLayer
        {
            get
            {
                // The following works only on the assumption that the copy marker is on only one layer.

                if (Shape is null)
                {
                    return null;
                }

                if (Shape.LayerSet.Count > 1)
                {
                    throw new NotImplementedException();
                }

                if (Shape.LayerSet.Count == 0)
                {
                    return null;
                }

                return Shape.LayerSet.First();
            }

            set
            {
                if (value is null)
                {
                    return;
                }

                if (Shape is null)
                {
                    return;
                }

                Shape.AddToLayerSet(value);

            }
        }

        public ShapeType ShapeType { get; set; }

        public string Guid { get;  set; }
       
        public CopyMarker(GraphicsWindow window, GraphicsPage page, double x, double y, double width)
        {
            this.Window = window;
            this.Page = page;

            this.x = x;
            this.y = y;

            this.width = width;

        }

        public GraphicShape Draw(Color color)
        {
            shapeElements = new GraphicShape[3];

            shapeElements[0] = Page.DrawCircle(this, new Coordinate(x, y), width, color);
            shapeElements[1] = Page.DrawLine(this, x - width / 2.0, y, x + width/2.0, y, string.Empty);
            shapeElements[2] = Page.DrawLine(this, x, y - width / 2.0, x, y + width / 2.0, string.Empty);

            shapeElements[0].SetFillOpacity(0);

            for (int i = 1; i < 3; i++)
            {
                shapeElements[i].SetLineColor(color);
            }

            Shape = VisioInterop.GroupShapes(Window, shapeElements);

            Guid = Utilities.GuidMaintenance.CreateGuid(Shape);


            Window?.DeselectAll();

            return Shape;
        }

        public Coordinate Location()
        {
            return new Coordinate(x, y);
        }

        public void Delete()
        {
            if (Shape is null)
            {
                return;
            }

            if (GraphicsLayer != null)
            {
                GraphicsLayer.RemoveShapeFromLayer(this);
            }

            VisioInterop.DeleteShape(Shape);

            shapeElements = null;
        }
    }
}
