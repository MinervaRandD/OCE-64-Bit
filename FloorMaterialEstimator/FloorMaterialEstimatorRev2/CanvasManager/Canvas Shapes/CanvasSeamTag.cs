

namespace FloorMaterialEstimator.CanvasManager
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
    using System.Runtime.CompilerServices;

    public class CanvasSeamTag: GraphicSeamTag
    {
        //private CanvasManager canvasManager;

        public CanvasSeamTag(
            GraphicsWindow window
            , GraphicsPage page
            , double x
            , double y
            , int seamAreaIndex
            , LayoutAreaType layoutAreaType)
        {
            this.Window = window;

            this.Page = page;

            //this.canvasManager = canvasManager;
            this.X = x;
            this.Y = y;
            this.SeamAreaIndex = seamAreaIndex;
            this.LayoutAreaType = layoutAreaType;

            string guid = GuidMaintenance.CreateGuid(this);

            this.Guid = guid;
        }

        public CanvasSeamTag(
            GraphicsWindow window
            , GraphicsPage page
            , SeamTagSerializable seamTag)
        {
            this.Window = window;

            this.Page = page;

            //this.canvasManager = canvasManager;
            this.X = seamTag.X;
            this.Y = seamTag.Y;
            this.SeamAreaIndex = seamTag.SeamAreaIndex;

            if (string.IsNullOrEmpty(seamTag.Guid))
            {
                this.Guid = GuidMaintenance.CreateGuid(this);
            }

            else
            {
                this.Guid = seamTag.Guid;
            }
        }

        internal void Draw()
        {
            if (Utilities.IsNotNull(Shape))
            {
                Debug.WriteLine("Attempt to redraw an already drawn canvas seam tag");
            }

            if (this.Guid is null)
            {
                this.Guid = GuidMaintenance.CreateGuid(this);
            }

            Shape = Page.DrawCircle(this, this.Guid, this.Center, 0.2, Color.White);

            Shape.SetShapeData("[Canvas Seam Tag]", "CircleTag");

            Shape.ShapeType = ShapeType.SeamTag | ShapeType.CircleTag;

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

            //Page.AddToPageShapeDict(this);

            //if (SeamAreaIndex < 0)
            //{
            //    canvasManager.CurrentPage.RmntEmbdLayer.Add(shape, 1);
            //}
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
            Shape?.BringToFront();
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

            Shape.ShowShapeOutline(!noLineMode);
        }

        public int SeamAreaIndex
        {
            get;
            set;
        } = 0;
      
        public static explicit operator GraphicShape(CanvasSeamTag canvasSeamTag)
        {
            return canvasSeamTag.Shape as GraphicShape;
        }
    }
}
