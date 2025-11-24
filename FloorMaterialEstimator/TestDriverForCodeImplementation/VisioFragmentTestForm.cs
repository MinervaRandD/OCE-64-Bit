
namespace TestDriverForCodeImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Graphics;
    using Geometry;
    using MaterialsLayout;
    using TracerLib;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class VisioFragmentTestForm : Form
    {
        private Visio.Window vsoWindow { get; set; }

        private GraphicsWindow window { get; set; }

        private Visio.Document vsoDocument { get; set; }

        private Visio.Page vsoPage { get; set; }

        private GraphicsPage page { get; set; }

        public VisioFragmentTestForm()
        {
            InitializeComponent();

            Tracer.TraceGen = new Tracer(TraceLevel.Exception, @"C:\Temp\temp.txt");

            vsoWindow = this.axDrawingControl.Window;
            vsoDocument = this.axDrawingControl.Document;

            this.vsoDocument.PrintLandscape = true;
            this.vsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.vsoDocument.Pages;

            this.vsoPage = pages[1];

            window = new GraphicsWindow(vsoWindow);

            page = new GraphicsPage(window, vsoPage);

            vsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            vsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            vsoWindow.ShowGrid = 1;

            vsoWindow.ShowRulers = 1;

            vsoWindow.ShowScrollBars = 1;

            vsoWindow.ZoomBehavior = Visio.VisZoomBehavior.visZoomVisio;

            vsoWindow.Zoom = -1;

            //VsoWindow.ZoomLock = true;

            setSize();

            this.SizeChanged += TestDriver2BaseForm_SizeChanged;

            doLayoutAreaDivide();
        }

        private void doLayoutAreaDivide()
        {
            VisioGeometryEngine.Init(window, page);
            VisioInterop.Init(window, page);

            double[] polygonPoints = new double[]
            {
                2, 2, 12, 2, 12, 4, 6, 4, 6, 10, 12, 10, 12, 12, 2, 12, 2, 2
            };

            Visio.Shape shape = vsoPage.DrawPolyline(polygonPoints, (short)8);

            double[] directedPolygonPoints = new double[polygonPoints.Length - 2];

            Array.Copy(polygonPoints, directedPolygonPoints, polygonPoints.Length - 2);

            DirectedPolygon directedPolygon = new DirectedPolygon(directedPolygonPoints);

            GraphicsDirectedPolygon graphicsDirectedPolygon = new GraphicsDirectedPolygon(window, page, directedPolygon);

            GraphicsLayoutArea graphicsLayoutArea = new GraphicsLayoutArea(window, page, graphicsDirectedPolygon);

            graphicsLayoutArea.Shape = new Shape(window, page, shape, ShapeType.LayoutArea);


            Coordinate coord1 = new Coordinate(9, 2);
            Coordinate coord2 = new Coordinate(9, 12);

            List<Shape> dividedElementList = graphicsLayoutArea.Divide(new List<Coordinate>() { coord1, coord2 });

            foreach (Shape shape1 in dividedElementList)
            {
                VisioInterop.SetBaseFillColor(shape1, Color.Blue);
            }
        }

        private void doDivide()
        {
            VisioGeometryEngine.Init(window, page);

            double[] polygonPoints = new double[]
            {
                2, 2, 12, 2, 12, 4, 6, 4, 6, 10, 12, 10, 12, 12, 2, 12, 2, 2
            };

            Visio.Shape shapeRect = vsoPage.DrawPolyline(polygonPoints, (short)8);

            Shape shape = new Shape(window, page, shapeRect, ShapeType.LayoutArea);

            double[] shapeLinePoints = new double[]
            {
                9, 2, 9, 12
            };

            List<Shape> dividedShapeList = VisioGeometryEngine.Subdivide(shape, shapeLinePoints);
        }

        private void doFragment()
        {

            //Visio.Shape frstShape = VsoPage.DrawRectangle(0, 0, 2, 2);

            double[] polygonPoints = new double[]
            {
                2, 2, 12, 2, 12, 4, 6, 4, 6, 10, 12, 10, 12, 12, 2, 12, 2, 2
            };

            Visio.Shape shapeRect = vsoPage.DrawPolyline(polygonPoints, (short) 8);
            Visio.Shape shapeLine = vsoPage.DrawLine(6, 2, 6, 12);


            vsoWindow.DeselectAll();

            Visio.Selection selection = vsoPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeSingle, Visio.VisSelectMode.visSelModeSkipSuper, shapeRect);

            //Visio.Selection selection2 = shapeOuter.CreateSelection(Visio.VisSelectionTypes.visSelTypeAll, Visio.VisSelectMode.visSelModeSkipSuper, shapeOuter);


            selection.Select(shapeRect, 2);
            selection.Select(shapeLine, 2);

            selection.Fragment();

            Visio.Selection result = vsoWindow.Selection;

            //VisioInterop.SetBaseFillColor(new Shape(shapeOuter, ShapeType.Rectangle), Color.Blue);
            //VisioInterop.SetBaseFillColor(new Shape(shapeInner, ShapeType.Rectangle), Color.Red);

            int count = result.Count;

            Visio.Shape shape1 = result[1];

            Visio.Shape containingShape = shape1.ContainingShape;

            Visio.Shapes containedShapes = containingShape.Shapes;

            count = containedShapes.Count;
            vsoWindow.DeselectAll();

            Visio.Paths paths = shape1.Paths;

            foreach (Visio.Path path in paths)
            {
                Array xyArray;

                path.Points(0.01, out xyArray);
            }

            foreach (Visio.Shape shape in containedShapes)
            {

                foreach (Visio.Path path in shape.Paths)
                {
                    Array xyArray;

                    path.Points(0.01, out xyArray);
                }
            }
            
            int shapeCount = vsoPage.Shapes.Count;

            Array x = vsoPage.GetContainers(Visio.VisContainerNested.visContainerExcludeNested);

            Visio.Shapes shapes = vsoPage.Shapes;

 //           Visio.Shape vsoShape1 = VsoPage.Shapes(shapeCount - 2)
 //ActiveWindow.Select vsoShape1, visSelect
            //paths = shapeRect.Paths;

            //foreach (Visio.Path path in paths)
            //{
            //    Array xyArray;

            //    path.Points(0.01, out xyArray);
            //}

        }

        private void TestDriver2BaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;


            int cntlLocnX = 16;
            int cntlLocnY = 16;

            int cntlSizeX = formSizeX - 48;
            int cntlSizeY = formSizeY - 96;

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

    }
}
