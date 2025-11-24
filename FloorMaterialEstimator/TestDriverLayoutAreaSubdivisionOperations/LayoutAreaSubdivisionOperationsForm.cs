
namespace TestDriverLayoutAreaEmbeddedLayoutOperations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using MaterialsLayout.Subdivision;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class LayoutAreaGeometricOperationsForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public TestCaseGenerator testCaseGenerator;

        public List<Shape> shapes = new List<Shape>();

        private GraphicsPage page;

        private GraphicsWindow window;

        List<Color> backColorList = new List<Color>() { Color.LightBlue, Color.LightGreen, Color.LightSalmon, Color.Cyan, Color.Magenta, Color.Tomato };
        public LayoutAreaGeometricOperationsForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 22;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            VsoWindow.ShowScrollBars = 1;

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, VsoPage);

            this.testCaseGenerator = new TestCaseGenerator(VsoDocument, VsoWindow, VsoPage);

            setSize();

            this.SizeChanged += LayoutAreaGeometricOperationsForm_SizeChanged;

        }

        private void LayoutAreaGeometricOperationsForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void testCase11()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon1();

            testCaseBase(gla, sd1);
        }

        private void testCase12()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon2();

            testCaseBase(gla, sd1);
        }

        private void testCase13()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon3();

            testCaseBase(gla, sd1);
        }

        private void testCase14()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon4();

            testCaseBase(gla, sd1);
        }

        private void testCase15()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon5();

            testCaseBase(gla, sd1);
        }

        private void testCase16()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon6();

            testCaseBase(gla, sd1);
        }

        private void testCase17()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon7();

            testCaseBase(gla, sd1);
        }

        private void testCase18()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon8();

            testCaseBase(gla, sd1);
        }

        private void testCase21()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon1();

            testCaseBase(gla, sd1);
        }

        private void testCase22()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon2();

            testCaseBase(gla, sd1);
        }

        private void testCase23()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon3();

            testCaseBase(gla, sd1);
        }

        private void testCase24()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon4();

            testCaseBase(gla, sd1);
        }

        private void testCase25()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon5();

            testCaseBase(gla, sd1);
        }

        private void testCase26()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon6();

            testCaseBase(gla, sd1);
        }

        private void testCase27()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon7();

            testCaseBase(gla, sd1);
        }

        private void testCase28()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon8();

            testCaseBase(gla, sd1);
        }

        private void testCase39()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase3();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon9();

            testCaseBase(gla, sd1);
        }

        private void testCase410()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase4();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon10();

            testCaseBase(gla, sd1);
        }

        private void testCase411()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase4();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.SubdividePolygon11();

            testCaseBase(gla, sd1);
        }

        private void testCaseBase(GraphicsLayoutArea gla, GraphicsDirectedPolygon sd1)
        {

            GraphicsDirectedPolygon ea = gla.ExternalArea;

           // GraphicsDirectedPolygon ia = gla.InternalAreas[0];

            ea.Draw(Color.Black, Color.LightBlue);

            gla.InternalAreas.ForEach(i1 => i1.Draw(Color.Black, Color.White));

            List<Shape> internalShapeList = new List<Shape>();

            gla.InternalAreas.ForEach(i1 => internalShapeList.Add(i1.Shape));

            Shape glaShape = VisioGeometryEngine.Subtract(window, page, ea.Shape, internalShapeList);

            sd1.Draw(Color.Black, Color.LightCoral);

            Shape sd1Shape = sd1.Shape;

            shapes.Add(glaShape);
            shapes.Add(sd1Shape);
            shapes.Add(ea.Shape);
            shapes.AddRange(internalShapeList);

            glaShape.SetFillOpacity(.5);
            sd1.Shape.SetFillOpacity(.5);

            glaShape.SetLineWidth(2.5);
            glaShape.SetLineWidth(2.5);

            window?.DeselectAll();

            SubdivisionGenerator subdivisionGenerator = new SubdivisionGenerator(gla, sd1);

            List<LayoutArea> results = subdivisionGenerator.GenerateSubdivision();

            int i = 0;

            foreach (LayoutArea layoutArea in results)
            {
                GraphicsLayoutArea graphicsLayoutArea = new GraphicsLayoutArea(window, page, layoutArea);

                graphicsLayoutArea.Translate(new Coordinate(10, 0));

                graphicsLayoutArea.Draw(Color.Black, backColorList[i % backColorList.Count]);

                VisioInterop.SetFillOpacity(graphicsLayoutArea.Shape, 0.5);

                graphicsLayoutArea.Shape.SetShapeText((i + 1).ToString(), Color.Black, 36);

                shapes.Add(graphicsLayoutArea.Shape);

                shapes.Add(graphicsLayoutArea.ExternalArea.Shape);

                graphicsLayoutArea.InternalAreas.ForEach(s => shapes.Add(s.Shape));

                shapes.Add(graphicsLayoutArea.Shape);

                i++;
            }

            window?.DeselectAll();
        }

        private void clearShapes()
        {
            foreach (Shape shape in shapes)
            {
                if (shape is null)
                {
                    continue;
                }

                shape.Delete();
            }
        }

        private void setSize()
        {
            int formSizeX = this.ClientRectangle.Width;
            int formSizeY = this.ClientRectangle.Height;

            int pnlSizeX = this.pnlTestCases.Width;
            int pnlSizeY = formSizeY - 8;

            int pnlLocnX = 4;
            int pnlLocnY = 4;

            int cntlLocnX = pnlLocnX + pnlSizeX + 8;
            int cntlLocnY = 4;

            int cntlSizeX = formSizeX - cntlLocnX - 8;
            int cntlSizeY = formSizeY - cntlLocnY - 8;

            this.pnlTestCases.Location = new Point(pnlLocnX, pnlLocnY);
            this.pnlTestCases.Size = new Size(pnlSizeX, pnlSizeY);

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        private void btnTestCase11_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase11();
        }

        private void btnTestCase12_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase12();
        }

        private void btnTestCase13_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase13();
        }

        private void btnTestCase14_Click(object sender, EventArgs e)
        {

            clearShapes();

            testCase14();
        }

        private void btnTestCase15_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase15();
        }

        private void btnTestCase16_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase16();
        }

        private void btnTestCase17_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase17();
        }

        private void btnTestCase18_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase18();
        }

        private void btnTestCase21_Click(object sender, EventArgs e)
        {
            testCase21();
        }

        private void btnTestCase22_Click(object sender, EventArgs e)
        {
            testCase22();
        }

        private void btnTestCase23_Click(object sender, EventArgs e)
        {
            testCase23();
        }

        private void btnTestCase24_Click(object sender, EventArgs e)
        {
            testCase24();
        }

        private void btnTestCase25_Click(object sender, EventArgs e)
        {
            testCase25();
        }

        private void btnTestCase26_Click(object sender, EventArgs e)
        {
            testCase26();
        }

        private void btnTestCase27_Click(object sender, EventArgs e)
        {
            testCase27();
        }

        private void btnTestCase28_Click(object sender, EventArgs e)
        {
            testCase28();
        }

        private void btnTestCase39_Click(object sender, EventArgs e)
        {
            testCase39();
        }

        private void btnTestCase410_Click(object sender, EventArgs e)
        {
            testCase410();
        }

        private void btnTestCase411_Click(object sender, EventArgs e)
        {
            testCase411();
        }
    }
}
