using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasLib.Markers_and_Guides
{
    using System.Drawing;
    using Graphics;

    public class ExtendedCrosshairs: IGraphicsShape
    {
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


        public ShapeType ShapeType => ShapeType.ExtendedCrosshairs;

        public string Guid => Shape is null ? string.Empty : Shape.Guid;

        public GraphicShape HorzLine;

        public GraphicShape VertLine;

        public ExtendedCrosshairs(GraphicsWindow window, GraphicsPage page)
        {
            Window = window;

            Page = page;

            HorzLine = Page.DrawLine(this, "[ExtendedCrosshairs]:Horiziontal Line", 0, 0, 0, 100);
            VertLine = Page.DrawLine(this, "[ExtendedCrosshairs]:Vertical Line", 0, 0, 100, 0);

            HorzLine.ShapeType = ShapeType.ExtendedCrosshairs;
            VertLine.ShapeType = ShapeType.ExtendedCrosshairs;

            VisioInterop.SetBaseLineColor(HorzLine, Color.DarkGreen);
            VisioInterop.SetBaseLineColor(VertLine, Color.DarkGreen);
        }

        public void SetCenter(double x, double y)
        {
            HorzLine.VisioShape.SetBegin(0, y);
            HorzLine.VisioShape.SetEnd(Page.PageWidth, y);

            VertLine.VisioShape.SetBegin(x, 0);
            VertLine.VisioShape.SetEnd(x, Page.PageHeight);
        }

        public void Delete()
        {
            Page.RemoveFromPageShapeDict(HorzLine);
            Page.RemoveFromPageShapeDict(VertLine);

            HorzLine.Delete();
            VertLine.Delete();

            HorzLine = null;
            VertLine = null;
        }
    }
}
