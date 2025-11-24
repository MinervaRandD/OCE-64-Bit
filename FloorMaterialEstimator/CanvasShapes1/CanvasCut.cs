
namespace CanvasShapes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;
    using Geometry;
    using MaterialsLayout;

    public class CanvasCut : GraphicsCut
    {
        public CanvasLayoutArea layoutArea;

        public CanvasCut(GraphicsWindow window, GraphicsPage page, CanvasLayoutArea layoutArea) : base(window, page)
        {
            this.layoutArea = layoutArea;
        }

        //public CanvasCut(GraphicsWindow window, GraphicsPage page, CanvasLayoutArea layoutArea, Cut cut, double cutAngle, string guid = null): base(window, page, cut)
        //{
        //    this.layoutArea = layoutArea;
        //}
    }
}
