
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

    public class Marker
    {
        GraphicShape[] shapeElements;

        double x;
        double y;
        double width;

        GraphicsWindow window;

        GraphicsPage page;

        public Marker(GraphicsWindow window, GraphicsPage page, double x, double y, double width)
        {
            this.window = window;
            this.page = page;

            this.x = x;
            this.y = y;
            this.width = width;
        }

        public void Draw(Visio.Page visioPage)
        {
            shapeElements = new GraphicShape[2];

            shapeElements[0] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(x - width, y, x + width, y), ShapeType.Line);
            shapeElements[1] = new GraphicShape(this, window, page, page.VisioPage.DrawLine(x, y - width, x, y + width), ShapeType.Line);

            shapeElements[0].VisioShape.Data1 = "Marker-H";
            shapeElements[1].VisioShape.Data1 = "Marker-V";
        }

        public void Delete()
        {
            if (shapeElements == null)
            {
                return;
            }

            for (int i = 0; i < shapeElements.Length; i++)
            {
                GraphicShape shape = shapeElements[i];

                if (shape != null)
                {
                    VisioInterop.DeleteShape(shape);
                }
            }

            shapeElements = null;
        }
    }
}
