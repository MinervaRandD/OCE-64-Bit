
namespace FloorMaterialEstimator.CanvasManager
{
    using Geometry;
    using Graphics;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CanvasBorderDirectionHandle: GraphicsCircle
    {
        CanvasBorderDirectionSelector canvasDirectionSelector;

        public char direction;

        public CanvasBorderDirectionHandle(GraphicsWindow window, GraphicsPage page, CanvasBorderDirectionSelector canvasDirectionSelector, char direction, Coordinate center, double radius): base(window, page, center, radius)
        {
            this.canvasDirectionSelector = canvasDirectionSelector;

            this.direction = direction;
        }

        public void Draw()
        {
            Shape = Page.DrawCircle(Guid, Center, Radius, Color.Green);

            VisioInterop.SetBaseFillColor(Shape, Color.Green);

            VisioInterop.SetFillOpacity(Shape, .75);
        }

        public void Remove()
        {
            VisioInterop.DeleteShape(this.Shape);
        }

        internal void SetSelected()
        {
            VisioInterop.SetBaseFillColor(Shape, Color.Red);
        }

        internal void SetUnselected()
        {
            VisioInterop.SetBaseFillColor(Shape, Color.Green);
        }
    }
}
