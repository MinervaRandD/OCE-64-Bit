
namespace FloorMaterialEstimator.CanvasManager
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
        Shape[] shapeElements;

        double x;
        double y;
        double width;

        public Marker(double x, double y, double width)
        {
            this.x = x;
            this.y = y;
            this.width = width;
        }

        internal void Draw(Visio.Page visioPage)
        {
            shapeElements = new Shape[2];

            shapeElements[0] = new Shape(visioPage.DrawLine(x - width, y, x + width, y), ShapeType.Line);
            shapeElements[1] = new Shape(visioPage.DrawLine(x, y - width, x, y + width), ShapeType.Line);

            shapeElements[0].VisioShape.Data1 = "Marker-H";
            shapeElements[1].VisioShape.Data1 = "Marker-V";
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
