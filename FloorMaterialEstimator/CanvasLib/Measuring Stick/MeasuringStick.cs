

namespace CanvasLib.MeasuringStick
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geometry;
    using System.Drawing;
    using Graphics;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;

    public class MeasuringStick : IGraphicsShape
    {
        const double DEFAULT_INCHES_IN_SCREEN_INCHES = 144.0;
        const string DEFAULT_SHAPE_DATA1 = "[MeasuringStick]";
        public string Guid { get; set; }

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


        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        Visio.Document rulerStencil = null;
        public Visio.Document RulerStencil
        {
            get { return rulerStencil; }

            set
            {
                bool wasVisible = IsVisible;

                Destroy();

                rulerStencil = value;

                if (wasVisible) Show();
            }
        }



        public bool IsVisible
        {
            get;
            set;
        } = false;

        public ShapeType ShapeType => throw new NotImplementedException();

        private double height = 0;
        private double width = 0;
        private double positionX = -1;
        private double positionY = -1;
        private double angle = 0;

        public MeasuringStick(GraphicsWindow window, GraphicsPage page)
        {
            Window = window;
            Page = page;
            Guid = GuidMaintenance.CreateGuid(this);
        }

        void CalcCenterPosition(out double locX, out double locY)
        {
            double canvasLeft = 0;
            double canvasUppr = 0;
            double canvasWdth = 0;
            double canvasHght = 0;
            VisioInterop.GetViewRect(Window, out canvasLeft, out canvasUppr, out canvasWdth, out canvasHght);

            locX = canvasLeft + canvasWdth / 2.0;
            locY = canvasUppr - canvasHght / 2.0;
        }

        private void CreateShape()
        {
            double locX = 0, locY = 0;
            if (positionX != -1 && positionX != -1)
            {
                locX = positionX;
                locY = positionY;
            }
            else CalcCenterPosition(out locX, out locY);

            Visio.Shape measuringStick = this.Page.VisioPage.Drop(this.RulerStencil.Masters["Ruler"], locX, locY);

            if (angle != 0) measuringStick.CellsU["Angle"].ResultIU = angle;

            width = measuringStick.CellsU["Width"].ResultIU;
            height = measuringStick.CellsU["Height"].ResultIU;

            measuringStick.CellsU["LockWidth"].ResultIU = 1;
            measuringStick.CellsU["LockHeight"].ResultIU = 1;

            measuringStick.Data1 = "[MeasuringStick]";

            Shape = new GraphicShape(this, Window, Page, measuringStick, ShapeType.TextBox, this.Guid)
            {
                ShapeType = ShapeType.MeasuringStick
            };

            Shape.VisioShape.Data1 = DEFAULT_SHAPE_DATA1;

            GraphicsLayer = new GraphicsLayerBase(Window, Page, "[MeasuringStick]", GraphicsLayerType.MeasuringStick, GraphicsLayerStyle.Static);
                
                //this.Guid, Window, Page, "[MeasuringStick]", GraphicsLayerType.MeasuringStick, GraphicsLayerStyle.Static);
            
            GraphicsLayer.AddShape(Shape, 1);

            Shape.GraphicsLayer = GraphicsLayer;

            SetScale(measuringStick);

            Page.AddToPageShapeDict(Shape);
        }

        private double CalculateScale()
        {
            return Page.DrawingScaleInInches / DEFAULT_INCHES_IN_SCREEN_INCHES;
        }

        public bool SelectedFromMouseDown(int button, double x, double y)
        {
            if (button != 1)
            {
                return false;
            }

            if (!Page.MouseOverMeasuringStickTool(x, y))
            {
                return false;
            }

            Window.DeselectAll();

            VisioInterop.SelectShape(Window, Shape);

            return true;
        }

        private void SetScale(Visio.Shape measuringStick)
        {
            double scale = CalculateScale();

            measuringStick.CellsU["Width"].ResultIU = width / scale;
            measuringStick.CellsU["Height"].ResultIU = height / scale;
        }

        public void Destroy()
        {
            Hide();

            if (Shape != null && GraphicsLayer != null)
            {

                Page.RemoveFromPageShapeDict(Shape);

                positionX = Shape.VisioShape.CellsU["PinX"].ResultIU;
                positionY = Shape.VisioShape.CellsU["PinY"].ResultIU;
                angle = Shape.VisioShape.CellsU["Angle"].ResultIU;

                Page.RemoveFromGraphicsLayerDict(((GraphicsLayer)GraphicsLayer));
                ((GraphicsLayer)GraphicsLayer).RemoveAllShapes();

                ((GraphicsLayer)GraphicsLayer).Delete();
                GraphicsLayer = null;
                Shape.Delete();
                height = 0;
                width = 0;
                Shape = null;
            }
        }

        public void Show(int btn = 2)
        {
            if (!IsVisible && RulerStencil != null)
            {
                if (Shape == null) CreateShape();

                this.GraphicsLayer.SetLayerVisibility(true);

                VisioInterop.SelectShape(Window, Shape);
                IsVisible = true;
            }
        }

        public void Hide()
        {
            if (IsVisible)
            {
                VisioInterop.DeselectShape(Window, Shape);
                this.GraphicsLayer.SetLayerVisibility(false);
                IsVisible = false;
            }
        }

        public void UpdateScale()
        {
            if (this.Shape != null) SetScale(this.Shape.VisioShape);
        }

        public bool IsSelected()
        {
            Visio.Selection selection = Window.VisioWindow.Selection;

            if (selection is null)
            {
                return false;
            }

            if (this.Shape is null)
            {
                return false;
            }

            if (this.Shape.VisioShape is null)
            {
                return false;
            }

            foreach (var x in selection)
            {
                if (x == this.Shape.VisioShape)
                {
                    return true;
                }
            }

            return false;
        }

        public void Select()
        {
            VisioInterop.SelectShape(this.Window, this.Shape);
        }

        public static bool IsMeasuringStickShape(Visio.Shape shape)
        {
            return shape.Data1.Contains(DEFAULT_SHAPE_DATA1);
        }

        public void Delete()
        {

        }
    }
}
