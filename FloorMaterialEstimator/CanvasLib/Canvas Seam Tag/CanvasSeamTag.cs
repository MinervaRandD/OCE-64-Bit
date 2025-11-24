

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Drawing;
    using Geometry;
    using Graphics;
    using Utilities;
    using System.Diagnostics;

    public class CanvasSeamTag
    {
        private CanvasManager canvasManager;
        public double X;
        public double Y;
        public int SeamAreaIndex;

        public Shape Shape;

        public Coordinate Coordinate { get; set; } = Coordinate.NullCoordinate;

        public CanvasSeamTag(CanvasManager canvasManager, double x, double y, int seamAreaIndex)
        {
            this.canvasManager = canvasManager;
            this.X = x;
            this.Y = y;
            this.SeamAreaIndex = seamAreaIndex;

            Coordinate = new Coordinate(x, y);
        }

        public CanvasSeamTag(CanvasManager canvasManager, SeamTagSerializable seamTag)
        {
            this.canvasManager = canvasManager;
            this.X = seamTag.X;
            this.Y = seamTag.Y;
            this.SeamAreaIndex = seamTag.SeamAreaIndex;
        }

        internal void Draw()
        {
            if (Utilities.IsNotNull(Shape))
            {
                Debug.WriteLine("Attempt to redraw an already drawn canvas seam tag");
            }

            Shape = canvasManager.CurrentPage.DrawRectangle(X, Y, X + 0.6, Y + 0.6);

            Shape.VisioShape.Data1 = "[CanvasSeamTag]";

            Shape.VisioShape.Data2 = "TextBox";
            Shape.VisioShape.Data1 = "Canvas Seam Tag";

            VisioInterop.DeselectShape(canvasManager.Window, Shape);

            string seamAreaIndexText = string.Empty;

            if (SeamAreaIndex >= 0)
            {
                seamAreaIndexText = SeamAreaIndex.ToString();
            }

            else
            {
                seamAreaIndexText = 'R' + (-SeamAreaIndex).ToString();
                //
            }

            VisioInterop.SetShapeText(Shape, seamAreaIndexText, Color.Red, 20);
            VisioInterop.SetFillOpacity(Shape, 0.0);
            VisioInterop.SetNolineMode(Shape);
            VisioInterop.BringToFront(Shape);

            //if (SeamAreaIndex < 0)
            //{
            //    canvasManager.CurrentPage.RmntEmbdLayer.Add(shape, 1);
            //}
        }

        internal void Delete()
        {
            if (Shape is null)
            {
                return;
            }

            Shape.Delete();

            Shape = null;
        }

        internal void Undraw()
        {
            if (Shape is null)
            {
                return;
            }

            Shape.Delete();

            Shape = null;
        }

        internal void Redraw()
        {
            if (this.Shape is null)
            {
                Draw();
            }

        }

        internal void BringToFront()
        {
            VisioInterop.BringToFront(Shape);
        }

        public bool IsDrawn()
        {
            return Shape != null;
        }

        internal void UnderlineTag()
        {
            VisioInterop.SetTextStyle(Shape, 4);
        }

        internal void SetNolineMode(bool noLineMode)
        {
            if (Shape is null)
            {
                return;
            }

            VisioInterop.SetNolineMode(Shape, noLineMode);
        }
    }
}
