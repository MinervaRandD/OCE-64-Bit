

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

    using Visio = Microsoft.Office.Interop.Visio;

    public class SeamingTool3
    {
        public string ShapeGuid { get; set; }

        public string Shape4Guid { get; set; }

        public string Shape5Guid { get; set; }

        public GraphicShape Shape
        {
            get;
            set;
        }

        private GraphicsLayer seamingToolLayer;

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

        public bool IsVisible => VisioInterop.GetLayerVisibility(seamingToolLayer);

        private bool selected = false;

        /// <summary>
        /// This is the graphics layer that the seaming tool sits on.
        /// It is put on its own layer for easy display/hide.
        /// </summary>
        //public GraphicsLayer GraphicsLayer { get; set; } = null;

        //public Coordinate CurrentLocation { get; set; } = Coordinate.NullCoordinate;

        // For testing...

        private bool setupUpFromTest = false;

        public SeamingTool3(GraphicsWindow window, GraphicsPage page, bool setupUpFromTest = false)
        {
            Window = window;

            Page = page;

            this.setupUpFromTest = setupUpFromTest;

            seamingToolLayer = new GraphicsLayer(null, Window, Page, "[SeamingTool]SeamingToolLayer", GraphicsLayerType.SeamingTool, GraphicsLayerStyle.Static);
            
            seamingToolLayer.Guid = GuidMaintenance.CreateGuid(seamingToolLayer);

            Initial_Draw();

            seamingToolLayer.AddShape(Shape, 0);

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
        }

        GraphicShape shape1 = null;
        GraphicShape shape2 = null;
        GraphicShape shape3 = null;
        GraphicShape shape4 = null;
        GraphicShape shape5 = null;
      

        private void Draw()
        {
            ShapeGuid = GuidMaintenance.CreateGuid(this);

            shape1 = Page.DrawLine(this, Location.X, Location.Y - Height / 2.0, Location.X, Location.Y + Height / 2.0, ShapeGuid);

            //shape1.Height = Height;

            //shape1.ShapeAngle = Angle;

            //shape1.CenterPoint = Location;

            shape1.ShapeType = ShapeType.SeamingTool;

            //VisioInterop.SetShapeWidth(Shape, 0);

            //shape1.SetLineColor(Color.Red);

            //shape1.SetFillColor(Color.Red);

            VisioInterop.SetLineBeginShape(shape1, 34, 3);
            VisioInterop.SetLineEndShape(shape1, 34, 3);

            double sInc = 0.0625;

            //shape2 = Page.DrawLine(Location.X - sInc, Location.Y - sInc, Location.X + sInc, Location.Y + sInc, "abc");
            //shape3 = Page.DrawLine(Location.X - sInc, Location.Y + sInc, Location.X + sInc, Location.Y - sInc, "def");

            //shape4 = Page.DrawCircle(new Coordinate(Location.X, Location.Y - Height / 2.0 - 0.1), 0.1, Color.Gray);
            //shape5 = Page.DrawCircle(new Coordinate(Location.X, Location.Y + Height / 2.0 + 0.1), 0.1, Color.Gray);

            VisioInterop.SetFillOpacity(shape4, 0.0);
            VisioInterop.SetShapeLineVisibility(shape4, false);

            VisioInterop.SetFillOpacity(shape5, 0.0);
            VisioInterop.SetShapeLineVisibility(shape5, false);

            //VisioInterop.SetResizeMode(shape1, 1);
            //VisioInterop.SetResizeMode(shape2, 1);
            //VisioInterop.SetResizeMode(shape3, 1);
            //VisioInterop.SetResizeMode(shape4, 1);
            //VisioInterop.SetResizeMode(shape5, 1);

            Shape = shape1; // VisioInterop.GroupShapes(new Shape[] { shape1, shape2, shape3, shape4, shape5});
            
            //VisioInterop.SetResizeMode(Shape, 1);

            Shape.VisioShape.Data1 = "[SeamingTool]";

            Shape.Guid = this.ShapeGuid;

            //Shape4Guid = shape4.Guid;

            //Shape5Guid = shape5.Guid;

            VisioInterop.SetBaseLineColor(shape1, Color.DarkGray);
            //VisioInterop.SetBaseLineColor(shape2, Color.DarkGray);
            //VisioInterop.SetBaseLineColor(shape3, Color.DarkGray);

            //VisioInterop.SetSelectLockStatus(Shape, true);

            //if (setupUpFromTest)
            //{
            //    this.Window.VisioWindow.MouseMove += VisioWindow_MouseMove;
            //}

            //Shape.VisioShape.CellChanged += VisioShape_CellChanged;

            //Shape.VisioShape.ShapeChanged += VisioShape_ShapeChanged;
            Shape.VisioShape.CellsSRC[
                (short) Visio.VisSectionIndices.visSectionObject
                , (short) Visio.VisRowIndices.visRowMisc
                , (short) Visio.VisCellIndices.visLOFlags].FormulaForceU = "4";
        }

        private void VisioShape_ShapeChanged(Visio.Shape Shape)
        {
            VisioInterop.DeselectShape(Window, this.Shape);
        }

        private void VisioShape_CellChanged(Visio.Cell Cell)
        {
            VisioInterop.DeselectShape(Window, Shape);
        }

        public bool VisioWindowMouseMove(int Button, int KeyButtonState, double x, double y)
        {
            if (KeyButtonState == 1)
            {
                Coordinate searchCoord = new Coordinate(x, y);

                Coordinate shapeCoord = VisioInterop.GetShapeUpperLeftLocation(this.Shape);

                Coordinate shape4Coord = VisioInterop.GetShapePinLocation(this.shape4) - new Coordinate(0, 0.05) + shapeCoord;

                if (Coordinate.H2Distance(searchCoord, shape4Coord) < 0.2)
                {
                    this.Location = new Coordinate(x, y);
                }

                Coordinate shape5Coord = VisioInterop.GetShapePinLocation(this.shape5) + shapeCoord;
                
                if (Coordinate.H2Distance(searchCoord, shape5Coord) < 0.1)
                {
                    selected = true;
                }


            }

            if (!VisioInterop.GetLayerVisibility(seamingToolLayer))
            {
                return false;
            }

            GraphicsSelection selection = VisioInterop.SpatialSearch(
                Window
                , Page
                , x
                , y
                , (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn
                | (short)Visio.VisSpatialRelationCodes.visSpatialContain
                | (short)Visio.VisSpatialRelationCodes.visSpatialOverlap
                | (short)Visio.VisSpatialRelationCodes.visSpatialTouching
                , 0.9, 2);

            foreach (GraphicShape shape in selection)
            {
                if (shape.Guid == this.ShapeGuid)
                {
                    VisioInterop.SetBaseLineColor(shape1, Color.Green);
                    VisioInterop.SetBaseLineColor(shape2, Color.Green);
                    VisioInterop.SetBaseLineColor(shape3, Color.Green);

                    //VisioInterop.SelectShape(Window, Shape);

                    VisioInterop.SetShapeLineVisibility(shape4, true);
                    VisioInterop.SetShapeLineVisibility(shape5, true);
                    
                    selected = true;

                    return true;
                }
            }

            if (selected)
            {
                VisioInterop.SetBaseLineColor(shape1, Color.DarkGray);
                VisioInterop.SetBaseLineColor(shape2, Color.DarkGray);
                VisioInterop.SetBaseLineColor(shape3, Color.DarkGray);

                //VisioInterop.DeselectShape(Window, Shape);

                VisioInterop.SetShapeLineVisibility(shape4, false);
                VisioInterop.SetShapeLineVisibility(shape5, false);

                selected = false;
            }

            return false;
        }

        private void VisioWindow_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            VisioWindowMouseMove(Button, KeyButtonState, x, y);

            CancelDefault = true;

            return;

            if (!VisioInterop.GetLayerVisibility(seamingToolLayer))
            {
                return;
            }

            GraphicsSelection selection = VisioInterop.SpatialSearch(
                Window
                , Page
                , x
                , y
                , (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn
                | (short)Visio.VisSpatialRelationCodes.visSpatialContain
                | (short)Visio.VisSpatialRelationCodes.visSpatialOverlap
                | (short)Visio.VisSpatialRelationCodes.visSpatialTouching

                , 0.9, 2);

            foreach (GraphicShape shape in selection)
            {
                if (shape.Guid == this.ShapeGuid)
                {
                    VisioInterop.SetBaseLineColor(shape1, Color.Green);
                    VisioInterop.SetBaseLineColor(shape2, Color.Green);
                    VisioInterop.SetBaseLineColor(shape3, Color.Green);


                    selected = true;

                    return;
                }
            }

            if (selected)
            {
                VisioInterop.SetBaseLineColor(shape1, Color.DarkGray);
                VisioInterop.SetBaseLineColor(shape2, Color.DarkGray);
                VisioInterop.SetBaseLineColor(shape3, Color.DarkGray);

                selected = false;
            }
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
            VisioInterop.SetLayerVisibility(seamingToolLayer, false);
        }

        /// <summary>
        /// Shows the seaming tool.
        /// </summary>
        public void Show()
        {
            VisioInterop.SetLayerVisibility(seamingToolLayer, true);

            Shape.BringToFront();
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
            VisioInterop.SelectShape(Window, Shape);
        }
    }
}
