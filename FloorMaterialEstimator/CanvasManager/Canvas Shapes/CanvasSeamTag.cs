

namespace CanvasManager.CanvasShapes
{
    using System;
    using System.Drawing;
    using Geometry;
    using Graphics;
    using Utilities;
    using System.Diagnostics;
    using MaterialsLayout;
    using SettingsLib;
    using Globals;
    public class CanvasSeamTag: IGraphicsShape
    {
        //private CanvasManager canvasManager;

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public double X;
        public double Y;

        public int SeamAreaIndex;

        public LayoutAreaType LayoutAreaType;

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


        public Coordinate Coordinate { get; set; } = Coordinate.NullCoordinate;

        public ShapeType ShapeType => ShapeType.SeamTag;

        public string Guid { get; set; }

        //public CanvasSeamTag(
        //    GraphicsWindow window
        //    , GraphicsPage page
        //    , double x
        //    , double y
        //    , int seamAreaIndex
        //    , LayoutAreaType layoutAreaType)
        //{
        //    this.Window = window;

        //    this.Page = page;

        //    //this.canvasManager = canvasManager;
        //    this.X = x;
        //    this.Y = y;
        //    this.SeamAreaIndex = seamAreaIndex;
        //    this.LayoutAreaType = layoutAreaType;

        //    Coordinate = new Coordinate(x, y);

        //    string guid = GuidMaintenance.CreateGuid(this);

        //    this.Guid = guid;
        //}

        //public CanvasSeamTag(
        //    GraphicsWindow window
        //    , GraphicsPage page
        //    , CanvasSeamTag seamTag)
        //{
        //    this.Window = window;

        //    this.Page = page;

        //    //this.canvasManager = canvasManager;
        //    this.X = seamTag.X;
        //    this.Y = seamTag.Y;
        //    this.SeamAreaIndex = seamTag.SeamAreaIndex;

        //    if (string.IsNullOrEmpty(seamTag.Guid))
        //    {
        //        this.Guid = GuidMaintenance.CreateGuid(this);
        //    }

        //    else
        //    {
        //        this.Guid = seamTag.Guid;
        //    }

        //}

        public void Draw()
        {
            if (Utilities.IsNotNull(Shape))
            {
                Debug.WriteLine("Attempt to redraw an already drawn canvas seam tag");
            }

            //Shape = canvasManager.CurrentPage.DrawRectangle(X, Y, X + 0.6, Y + 0.6);

            Shape = Page.DrawRectangle(this, X, Y, X + 0.6, Y + 0.6);

            Shape.SetShapeData("Canvas Seam Tag", "TextBox");

            Page.AddToPageShapeDict(Shape);

            VisioInterop.LockShape(Shape);

            VisioInterop.DeselectShape(Window, Shape);

            string seamAreaIndexText = SeamAreaIndex.ToString();

            if (LayoutAreaType == LayoutAreaType.Normal)
            {

            }

            else if (LayoutAreaType == LayoutAreaType.OversGenerator)
            {
                seamAreaIndexText = 'O' + seamAreaIndexText;
            }

            else if (LayoutAreaType == LayoutAreaType.Remnant)
            {
                seamAreaIndexText = 'R' + seamAreaIndexText;
            }

            Shape.SetShapeText(seamAreaIndexText, GlobalSettings.AreaIndexFontColor, GlobalSettings.AreaIndexFontInPts);
            VisioInterop.SetFillOpacity(Shape, 0.0);
            Shape.ShowShapeOutline(false);
            Shape.BringToFront();

            //if (SeamAreaIndex < 0)
            //{
            //    canvasManager.CurrentPage.RmntEmbdLayer.Add(shape, 1);
            //}
        }

        public void Delete()
        {
            if (Shape is null)
            {
                return;
            }

            Page.RemoveFromPageShapeDict(Shape);

            Shape.Delete();

            Shape = null;
        }

        public void Redraw()
        {
            if (this.Shape is null)
            {
                Draw();
            }

        }

        public void BringToFront()
        {
            Shape?.BringToFront();
        }

        public bool IsDrawn()
        {
            return Shape != null;
        }

        public void UnderlineTag()
        {
              VisioInterop.SetTextStyle(Shape, 4);
        }

        public void SetNolineMode(bool noLineMode)
        {
            if (Shape is null)
            {
                return;
            }

            Shape.ShowShapeOutline(!noLineMode);
        }

        void IGraphicsShape.Delete()
        {
            throw new NotImplementedException();
        }
    }
}
