
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

    public class Guide
    {
        Shape[] shapeElements;

        double x;
        double y;
        double pageWidth;
        double pageHeight;

        public Guide(double x, double y, double pageWidth, double pageHeight)
        {
            this.x = x;
            this.y = y;
            this.pageWidth = pageWidth;
            this.pageHeight = pageHeight;
        }

        internal void Draw(Visio.Page visioPage)
        {
            shapeElements = new Shape[4];

            shapeElements[0] = new Shape(visioPage.DrawLine(0, y, x - 0.125, y), ShapeType.Line);
            shapeElements[1] = new Shape(visioPage.DrawLine(x + 0.125, y, pageWidth, y), ShapeType.Line);
            shapeElements[2] = new Shape(visioPage.DrawLine(x, 0, x, y - 0.125), ShapeType.Line);
            shapeElements[3] = new Shape(visioPage.DrawLine(x, y + 0.125, x, pageHeight), ShapeType.Line);

            shapeElements[0].VisioShape.Data1 = "Guide-L";
            shapeElements[1].VisioShape.Data1 = "Guide-R";
            shapeElements[2].VisioShape.Data1 = "Guide-U";
            shapeElements[3].VisioShape.Data1 = "Guide-D";

            foreach (Shape shape in shapeElements)
            {
                VisioInterop.FormatGuideLine(shape);
            }
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
        }
    }
}
