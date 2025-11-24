

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

    public class SeamingTool_rev2
    {
        public string Guid { get; set; }

        public GraphicShape Shape {
            get;
            set;
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

        public bool IsVisible
        {
            get;
            set;
        } = false;

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
        /// <summary>
        /// Flag to determine whether the seaming tool has yet
        /// been displayed for the current new/blank/existing project.
        /// It is set up to be false whenever a new/blank/existing project
        /// is loaded. The flag is used to center the seaming tool on first display
        /// </summary>
        public bool FirstTimeDisplay { get; set; } = true;

        /// <summary>
        /// This is the graphics layer that the seaming tool sits on.
        /// It is put on its own layer for easy display/hide.
        /// </summary>
        //public GraphicsLayer GraphicsLayer { get; set; } = null;

        //public Coordinate CurrentLocation { get; set; } = Coordinate.NullCoordinate;

        public SeamingTool_rev2(GraphicsWindow window, GraphicsPage page)
        {
            Window = window;

            Page = page;

            //if (Shape is null)
            //{
            //    Initial_Draw();
            //}

            //GraphicsLayer = new GraphicsLayer(Shape.Guid, Window, Page, "[SeamingTool]SeamingToolLayer", GraphicsLayerType.Static);

            //GraphicsLayer.Add(Shape, 0);

            //Hide();
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

        private void Draw()
        {
            if (FirstTimeDisplay)
            {
                FirstTimeDisplay = false;

                Initial_Draw();

                return;
            }

            string Guid = GuidMaintenance.CreateGuid(this);

            Shape = Page.DrawRectangle(this, Location.X,Location.Y - Height / 2.0, Location.X, Location.Y + Height / 2.0, Guid);

            Shape.Height = Height;

            Shape.ShapeAngle = Angle;

            Shape.CenterPoint = Location;

            VisioInterop.SetShapeWidth(Shape, 0);

            Shape.SetLineColor(Color.Red);

            Shape.SetFillColor(Color.Red);

            VisioInterop.LockShapeWidth(Shape);

            Shape.VisioShape.Data1 = "[SeamingTool]";

            this.Guid = Shape.Guid;

            CenterInView();

            //IsVisible = true;
        }

        private void VisioShape_CellChanged(Microsoft.Office.Interop.Visio.Cell Cell)
        {
            if (Utilities.IsNotNull(Shape))
            {
                Location = Shape.CenterPoint;

                Angle = Shape.ShapeAngle;

                Height = Shape.Height;

            }


            VisioInterop.ResetLocPinXY(Shape);
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

            if (!this.IsVisible)
            {
                return;
            }

            // Save current location and other information. However, depending on cycle the info might
            // get wiped out in the call to Delete Shape.

            if (Utilities.IsNotNull(Shape))
            {
                Location = Shape.CenterPoint;
                Angle = Shape.ShapeAngle;
                Height = Shape.Height;
            }

            VisioInterop.SetShapeLineVisibility(Shape, false);

            VisioInterop.DeleteShape(Shape);

            this.IsVisible = false;

            Shape = null;
        }

        /// <summary>
        /// Shows the seaming tool.
        /// </summary>
        public void Show(int btn = 2)
        {
            if (Shape is null)
            {
                Draw();
            }

            VisioInterop.SelectShape(Window, Shape);

            Shape.BringToFront();
            // VisioInterop.SelectShape(Window, Shape);

            this.IsVisible = true;
        }

        /// <summary>
        /// Expands the seaming tool to span a layout area bondary.
        /// </summary>
        /// <param name="layoutAreaBoundary">The layout area boundary.</param>
        public void ExpandToPolygonBoundary(GraphicsDirectedPolygon layoutAreaBoundary)
        {
            DirectedLine seamingToolBaseLine = this.ToLine();

            DirectedLine extendedSeamingToolBaseLine = seamingToolBaseLine.ExtendLine(1000.0);

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
            return Shape.CenterPoint;
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
            if (!IsVisible)
            {
                return;
            }
            VisioInterop.ResetLocPinXY(Shape);
            if (IsSelected)
            {

                return;
            }

            IsSelected = true;
        }
    }
}
