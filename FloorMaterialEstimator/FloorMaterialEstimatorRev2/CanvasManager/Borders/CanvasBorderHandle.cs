

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Graphics;

    public class CanvasBorderHandle: GraphicsCircle
    {
        public CanvasDirectedPolygon CanvasDirectedPolygon;

        public Coordinate BaseCoord;
        public Coordinate LeftCoord;
        public Coordinate RghtCoord;

        public bool IsSelectable { get; set; }

        public bool IsSelected { get; set; }

        public CanvasBorderHandle(
            GraphicsWindow window, 
            CanvasPage page,
            CanvasDirectedPolygon CanvasDirectedPolygon,
            Coordinate center,
            Coordinate leftCoord,
            Coordinate rghtCoord,
            double radius): base(window, page, center, radius)
        {
            BaseCoord = center;
            LeftCoord = leftCoord;
            RghtCoord = rghtCoord;

            this.CanvasDirectedPolygon = CanvasDirectedPolygon;
        }

        public void Draw()
        {
            Shape = Page.DrawCircle(Guid, Center, Radius, Color.Red);

            VisioInterop.SetBaseFillColor(Shape, Color.Red);

            VisioInterop.SetFillOpacity(Shape, 0);
        }

        public void Remove()
        {
            VisioInterop.DeleteShape(this.Shape);
        }

        public void Activate()
        {
            this.IsSelectable = true;
            this.IsSelected = false;

            VisioInterop.SetBaseFillColor(Shape, Color.Green);
            VisioInterop.SetFillOpacity(Shape, 0.75);
        }

        public void Deactivate()
        {
            this.IsSelectable = false;
            this.IsSelected = false;

            VisioInterop.SetBaseFillColor(Shape, Color.Red);
            VisioInterop.SetFillOpacity(Shape, 0);
        }

        public void Select()
        {
            this.IsSelectable = false;
            this.IsSelected = true;

            VisioInterop.SetBaseFillColor(Shape, Color.Red);
            VisioInterop.SetFillOpacity(Shape, 0.5);
        }

        internal bool IsAdjacentTo(CanvasBorderHandle canvasFixedWidthHandle)
        {
            if (this.CanvasDirectedPolygon != canvasFixedWidthHandle.CanvasDirectedPolygon)
            {
                return false;
            }

            if (canvasFixedWidthHandle.LeftCoord == this.BaseCoord)
            {
                return true;
            }

            if (canvasFixedWidthHandle.RghtCoord == this.BaseCoord)
            {
                return true;
            }

            return false;
        }
    }
}
