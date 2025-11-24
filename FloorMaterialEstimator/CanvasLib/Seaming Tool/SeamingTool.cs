

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

    public class SeamingToolOld
    {
        public string Guid { get; set; }

        public GraphicShape Shape { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        private const double length = 2.0;

        private double width;

        private double height;

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
        public GraphicsLayerBase GraphicsLayerBase { get; set; } = null;

        //public Coordinate CurrentLocation { get; set; } = Coordinate.NullCoordinate;

        //public SeamingToolOld(GraphicsWindow Window, GraphicsPage Page)
        //{
        //    Window = Window;

        //    Page = Page;

        //    width = 2.0 * 0.01;

        //    height = length;

        //    if (Shape is null)
        //    {
        //        Draw(/*.1, 2.0*/);
        //    }

        //    GraphicsLayer = new GraphicsLayerBase(Shape.Guid, Window, Page, "[SeamingTool]SeamingToolLayer", GraphicsLayerType.Static);

        //    GraphicsLayer.AddShape(Shape, 1);

        //    Hide();
        //}

        //public void Delete()
        //{
        //    GraphicsLayer.RemoveAllShapes();

        //    Page.RemoveFromGraphicsLayerDict(GraphicsLayer);

        //    GraphicsLayer.Delete();

        //    GraphicsLayer = null;
        //}

        /// <summary>
        /// Draws the seaming tool for the first time (puts in on the visio canvas
        /// </summary>

        public void Draw(/*double x, double y*/)
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

            double x1 = X - 0.005;

            double x2 = X + 0.005;

            double y1 = Y - 0.5 * length;

            double y2 = Y + 0.5 * length;

            string Guid = GuidMaintenance.CreateGuid(this);

            Shape = Page.DrawRectangle(this, x1, y1, x2, y2, Guid);

            Shape.VisioShape.Data1 = "[SeamingTool]";

            this.Guid = Shape.Guid;

            Shape.SetLineColor(Color.Red);

            Shape.SetFillColor( Color.Red);

            VisioInterop.LockShapeWidth(Shape);


            IsVisible = false;
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

            VisioInterop.DeselectShape(Window, Shape);

            this.GraphicsLayerBase.SetLayerVisibility(false);

        }

        /// <summary>
        /// Shows the seaming tool.
        /// </summary>
        public void Show(int btn = 2)
        {
            this.GraphicsLayerBase.SetLayerVisibility(true);

            if (FirstTimeDisplay)
            {

                // The first time the seaming tool is displayed for a project it is centered in the current view.
                // This is to guarantee that it will be visible, since the user may zoom or pan before first showing.

                CenterInView();

                //FirstTimeDisplay = false;
            }

            VisioInterop.SelectShape(Window, Shape);
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

#if false
        /// <summary>
        /// Expands the seaming tool to span a layout area bondary.
        /// </summary>
        /// <param name="layoutAreaBoundary">The layout area boundary.</param>
        public void ExpandToPolygonBoundary(GraphicsDirectedPolygon layoutAreaBoundary)
        {
            DirectedLine seamingToolBaseLine = this.ToLine();

            DirectedLine extendedSeamingToolBaseLine = seamingToolBaseLine.ExtendLine(1000.0);

            Coordinate toolLocation = this.GetLocation();

            double toolCenterX = toolLocation.X;
            double toolCenterY = toolLocation.Y;

            PrimaryOrientation primaryOrientation = MathUtils.LinePrimaryOrientation(seamingToolBaseLine.Coord1.X, seamingToolBaseLine.Coord1.Y, seamingToolBaseLine.Coord2.X, seamingToolBaseLine.Coord2.Y);

            DirectedLine spanningLine = null;

            if (primaryOrientation == PrimaryOrientation.Horizontal)
            {
                spanningLine = getExtendedLineHorizontal(toolLocation, extendedSeamingToolBaseLine, layoutAreaBoundary);
            }

            else
            {
                spanningLine = getExtendedLineVertical(toolLocation, extendedSeamingToolBaseLine, layoutAreaBoundary);
            }

            if (spanningLine is null)
            {
                return;
            }

            spanningLine.ExtendLine(0.01);

            this.SetLength(spanningLine.Length);

            this.SetLocation(spanningLine.Center());
        }

        /// <summary>
        /// Expands the seaming tool to span a layout area bondary when the seaming tool is oriented primarily horizontal.
        /// </summary>
        /// <param name="layoutAreaBoundary">The layout area boundary.</param>
        private DirectedLine getExtendedLineHorizontal(Coordinate toolLocation, DirectedLine toolExtendedLine, GraphicsDirectedPolygon layoutAreaBoundary)
        {
            Coordinate closestLeft = Coordinate.NullCoordinate;
            Coordinate closestRght = Coordinate.NullCoordinate;

            double minDistanceLeft = double.MaxValue;
            double minDistanceRght = double.MaxValue;

            double toolCenterX = toolLocation.X;
            double toolCenterY = toolLocation.Y;

            foreach (GraphicsDirectedLine polygonBoundaryLine in layoutAreaBoundary)
            {
                DirectedLine boundaryLine = (DirectedLine)polygonBoundaryLine;

                Coordinate intersectionPoint = boundaryLine.Intersect(toolExtendedLine);

                if (Coordinate.IsNullCoordinate(intersectionPoint))
                {
                    continue;
                }

                double intersectionX = intersectionPoint.X;
                double intersectionY = intersectionPoint.Y;

                double distance = MathUtils.H2Distance(toolCenterX, toolCenterY, intersectionX, intersectionY);

                if (intersectionX <= toolCenterX)
                {
                    if (distance < minDistanceLeft)
                    {
                        minDistanceLeft = distance;
                        closestLeft = intersectionPoint;
                    }
                }

                else
                {
                    if (distance < minDistanceRght)
                    {
                        minDistanceRght = distance;
                        closestRght = intersectionPoint;
                    }
                }
            }

            if (Coordinate.IsNullCoordinate(closestLeft) || Coordinate.IsNullCoordinate(closestRght))
            {
                return null;
            }

            return new DirectedLine(closestLeft, closestRght);
        }

        /// <summary>
        /// Expands the seaming tool to span a layout area bondary when the seaming tool is oriented primarily vertical.
        /// </summary>
        /// <param name="layoutAreaBoundary">The layout area boundary.</param>
        private DirectedLine getExtendedLineVertical(Coordinate toolLocation, DirectedLine toolExtendedLine, GraphicsDirectedPolygon layoutAreaBoundary)
        {
            Coordinate closestLowr = Coordinate.NullCoordinate;
            Coordinate closestUppr = Coordinate.NullCoordinate;

            double minDistanceLowr = double.MaxValue;
            double minDistanceUppr = double.MaxValue;

            double toolCenterX = toolLocation.X;
            double toolCenterY = toolLocation.Y;

            foreach (GraphicsDirectedLine polygonBoundaryLine in layoutAreaBoundary)
            {
                DirectedLine boundaryLine = (DirectedLine)polygonBoundaryLine;

                Coordinate intersectionPoint = boundaryLine.Intersect(toolExtendedLine);

                if (Coordinate.IsNullCoordinate(intersectionPoint))
                {
                    continue;
                }

                double intersectionX = intersectionPoint.X;
                double intersectionY = intersectionPoint.Y;

                double distance = MathUtils.H2Distance(toolCenterX, toolCenterY, intersectionX, intersectionY);

                if (intersectionY <= toolCenterY)
                {
                    if (distance < minDistanceLowr)
                    {
                        minDistanceLowr = distance;
                        closestLowr = intersectionPoint;
                    }
                }

                else
                {
                    if (distance < minDistanceUppr)
                    {
                        minDistanceUppr = distance;
                        closestUppr = intersectionPoint;
                    }
                }
            }

            if (Coordinate.IsNullCoordinate(closestLowr) || Coordinate.IsNullCoordinate(closestUppr))
            {
                return null;
            }

            return new DirectedLine(closestLowr, closestUppr);
        }
#endif

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

            if (IsSelected)
            {
                return;
            }

            IsSelected = true;
        }
    }
}
