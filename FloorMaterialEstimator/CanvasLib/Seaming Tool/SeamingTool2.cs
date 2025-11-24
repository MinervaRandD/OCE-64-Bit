

namespace CanvasLib.SeamingTool
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Geometry;
    using Graphics;
    using Utilities;

    public class SeamingTool: IGraphicsShape
    {
        public string Guid { get; set; }

        public GraphicShape Shape {
            get;
            set;
        }


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

        public double X { get; set; }

        public double Y { get; set; }

        private const double length = 2.0;

        Coordinate Location
        {
            get;
            set;
        }

        double Angle
        {
            get;
            set;
        }

        double Height
        {
            get;
            set;
        }

        public bool IsVisible => GraphicsLayer.IsVisible();

        private bool _IsSelected = false;

        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }

            set
            {
                _IsSelected = value;

                if (_IsSelected)
                {
                    VisioInterop.SelectShape(this.Window, this.Shape);
                }

                else
                {
                    VisioInterop.DeselectShape(this.Window, this.Shape);
                }
            }
        }

        public ShapeType ShapeType => ShapeType.SeamingTool;

        /// <summary>
        /// This is the graphics layer that the seaming tool sits on.
        /// It is put on its own layer for easy display/hide.
        /// </summary>
        //public GraphicsLayer GraphicsLayer { get; set; } = null;

        //public Coordinate CurrentLocation { get; set; } = Coordinate.NullCoordinate;

        public SeamingTool(GraphicsWindow window, GraphicsPage page)
        {
            this.Window = window;

            this.Page = page;

            Initial_Draw();

            GraphicsLayer = new GraphicsLayerBase(window, page, "[SeamingTool]", GraphicsLayerType.SeamingTool, GraphicsLayerStyle.Static);
               
            GraphicsLayer.AddShape(Shape, 0);

            GraphicsLayer gl = (GraphicsLayer)GraphicsLayer;

            if (!page.GraphicsLayerDictContains(gl.Guid))
            {
                page.GraphicsLayerDict.Add(((GraphicsLayer)GraphicsLayer).Guid, (GraphicsLayer)GraphicsLayer);
            }

            GraphicsLayer.SetLayerVisibility(false);

            Hide();
        }

        /// <summary>
        /// Draws the seaming tool for the first time (puts in on the visio canvas
        /// </summary>

        public void Initial_Draw()
        {
            // As a default, we 'create' the seaming tool in the center of the current view
            // This is not strictly necessary as it is not shown immediately, but is defensive in nature.

            double canvasLeft = 0;
            double canvasUppr = 0;

            double canvasWdth = 0;
            double canvasHght = 0;

            VisioInterop.GetViewRect(Window, out canvasLeft, out canvasUppr, out canvasWdth, out canvasHght);

            // X and Y now represent the center of the current view.

            double X = canvasLeft + canvasWdth / 2.0;
            double Y = canvasUppr - canvasHght / 2.0;

            Location = new Coordinate(X, Y);

            Height = length;

            Angle = 0.0;

            Draw();

            CenterInView();

            Shape.VisioShape.CellChanged += VisioShape_CellChanged;
        }

        private void VisioShape_CellChanged(Microsoft.Office.Interop.Visio.Cell Cell)
        {
            string cellLocalName = Cell.LocalName;

            if (cellLocalName == "LocPinX")
            {
                Cell.FormulaU = "Width * 0.5";

                VisioInterop.SelectShape(Window, this.Shape);
            }

            else if (cellLocalName == "LocPinY")
            {
                Cell.FormulaU = "Height * 0.5";

                VisioInterop.SelectShape(Window, this.Shape);
            }

        }

        public bool SelectedFromMouseDown(int button, double x, double y)
        {
            if (!Page.MouseOverSeamingTool(x, y))
            {
                return false;
            }

            Window.DeselectAll();

            VisioInterop.SelectShape(Window, Shape);

            this.IsSelected = true;

            if (button == 1)
            {

            }
            return true;
        }

        private void Draw()
        {
            
            string Guid = GuidMaintenance.CreateGuid(this);

            Shape = Page.DrawRectangle(this, Location.X,Location.Y - Height / 2.0, Location.X, Location.Y + Height / 2.0, Guid);

            Shape.Height = Height;

            Shape.ShapeAngle = Angle;

            Shape.CenterPoint = Location;

            Shape.ShapeType = ShapeType.SeamingTool;

            VisioInterop.SetShapeWidth(Shape, 0);

            Shape.SetLineColor(Color.Red);

            Shape.SetFillColor(Color.Red);

            VisioInterop.LockShapeWidth(Shape);

            Shape.VisioShape.Data1 = "[SeamingTool]";

            Shape.VisioShape.Data2 = "Rectangle";

            Shape.ParentObject = this;

            this.Guid = Shape.Guid;

            this.Page.AddToPageShapeDict(this.Shape);
           // this.Page.GuidShapeDict.Add(this.Guid, Shape);
        }


        /// <summary>
        /// Centers the seaming tool in the current view
        /// </summary>
        public void CenterInView()
        {

            double canvasLeft = 0;
            double canvasUppr = 0;

            double canvasWdth = 0;
            double canvasHght = 0;

            VisioInterop.GetViewRect(Window, out canvasLeft, out canvasUppr, out canvasWdth, out canvasHght);

            double locX = canvasLeft + canvasWdth / 2.0;
            double locY = canvasUppr - canvasHght / 2.0;

            SetLocation(new Coordinate(locX, locY));

        }

        /// <summary>
        /// Hides the seaming tool
        /// </summary>
        public void Hide()
        {
            GraphicsLayer.Visibility = false;
            GraphicsLayer.SetLayerVisibility(false);
        }

        /// <summary>
        /// Shows the seaming tool.
        /// </summary>
        public void Show()
        {
            GraphicsLayer.Visibility = false;

            VisioInterop.SetLayerVisibility((GraphicsLayer) GraphicsLayer, true);

            VisioInterop.SelectShape(Window, Shape);

            Shape.BringToFront();
        }

        /// <summary>
        /// Expands the seaming tool to span a layout area bondary.
        /// </summary>
        /// <param name="layoutAreaBoundary">The layout area boundary.</param>
        public void ExpandToPolygonBoundary(GraphicsDirectedPolygon layoutAreaBoundary)
        {
            DirectedLine seamingToolBaseLine = this.ToLine();

            //DirectedLine extendedSeamingToolBaseLine = seamingToolBaseLine.ExtendLine(1000.0);

            Coordinate toolLocation = this.GetLocation();

            double theta = seamingToolBaseLine.Atan();

            Coordinate offset = seamingToolBaseLine.Center();

            seamingToolBaseLine.Translate(-offset);

            seamingToolBaseLine.Rotate(-theta);

            DirectedPolygon lab = ((DirectedPolygon)layoutAreaBoundary).Clone();

            lab.Translate(-offset);

            lab.Rotate(-theta);

            double minDistanceLeft = double.MaxValue;
            double minDistanceRght = double.MaxValue;

            DirectedLine xAxis = new DirectedLine(new Coordinate(-1000, 0), new Coordinate(1000, 0));

            foreach (DirectedLine boundaryLine in lab)
            {
                Coordinate intersectionPoint = boundaryLine.Intersect(xAxis);

                if (Coordinate.IsNullCoordinate(intersectionPoint))
                {
                    continue;
                }

                double x = intersectionPoint.X;

                if (x >= 0)
                {
                    if (x < minDistanceRght)
                    {
                        minDistanceRght = x;
                    }
                }

                else
                {
                    if (-x < minDistanceLeft)
                    {
                        minDistanceLeft = -x;
                    }
                }
            }

            if (minDistanceLeft == double.MaxValue || minDistanceRght == double.MaxValue)
            {
                return;
            }

            DirectedLine spanningLine = new DirectedLine(new Coordinate(-minDistanceLeft - 0.01, 0), new Coordinate(minDistanceRght + 0.01, 0));

            spanningLine.Rotate(theta);

            spanningLine.Translate(offset);


            this.SetLength(spanningLine.Length);

            this.SetLocation(spanningLine.Center());

        }

        public Coordinate GetLocation()
        {
            return VisioInterop.GetShapePinLocation(this.Shape);
        }

        /// <summary>
        /// Sets the location of the tool.
        /// </summary>
        /// <param name="center">The coordinate of the new location</param>
        public void SetLocation(Coordinate center)
        {
            Shape.CenterPoint = center;
        }

        public void SetLength(double length)
        {
            Shape.Height = length;
        }

        /// <summary>
        /// Gets the angle of orientation of the tool.
        /// </summary>
        /// <returns></returns>
        public double GetAngle()
        {
            return Shape.ShapeAngle;
        }

        public void SetAngle(double theta)
        {
            Shape.ShapeAngle = theta;
        }

        public DirectedLine ToLine()
        {
            double length = VisioInterop.Height(Shape);

            Coordinate location = GetLocation();

            double theta = GetAngle();

            double x1 = -length * 0.5;
            double y1 = 0;

            double x2 = length * 0.5;
            double y2 = 0;

            DirectedLine directedLine = new DirectedLine(new Coordinate(x1, y1), new Coordinate(x2, y2));

            directedLine.Rotate(theta + Math.PI * 0.5);

            directedLine.Translate(location);

            return directedLine;
        }

        //public bool IsSelected()
        //{
        //    return VisioInterop.ShapeIsSelected(Window, Shape);
        //}

        public void Select()
        {
            VisioInterop.SelectShape(Window, Shape);
        }

        public void RotateRight90Degrees()
        {
            double theta = GetAngle();

            theta -= Math.PI * 0.5;

            SetAngle(theta);
        }

        public void AlignVertical()
        {
            SetAngle(0);
        }

        public void AlignHorizontal()
        {
            SetAngle(Math.PI * 1.5);

        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
