
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
    using MaterialsLayout.Embedded_Layout_Generation;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class LayoutAreaEmbeddedLayoutForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public TestCaseGenerator testCaseGenerator;

        public List<Shape> shapes = new List<Shape>();

        public List<GraphicsLayoutArea> layoutAreas = new List<GraphicsLayoutArea>();

        private GraphicsPage page;

        private GraphicsWindow window;

        List<Color> backColorList = new List<Color>() { Color.LightBlue, Color.LightGreen, Color.LightSalmon, Color.Cyan, Color.Magenta, Color.Tomato };
        public LayoutAreaEmbeddedLayoutForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 26;

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

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon eb1 = testCaseGenerator.EmbedPolygon1();

            testCaseBase(glal, eb1);
        }

        private void testCase12()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon eb2 = testCaseGenerator.EmbedPolygon2();

            testCaseBase(glal, eb2);
        }

        private void testCase13()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon eb3 = testCaseGenerator.EmbedPolygon3();

            testCaseBase(glal, eb3);
        }

        private void testCase21()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon eb1 = testCaseGenerator.EmbedPolygon1();

            testCaseBase(glal, eb1);
        }

        private void testCase22()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon eb2 = testCaseGenerator.EmbedPolygon2();

            testCaseBase(glal, eb2);
        }

        private void testCase23()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon eb3 = testCaseGenerator.EmbedPolygon3();

            testCaseBase(glal, eb3);
        }

        private void testCase31()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase3();

            GraphicsDirectedPolygon eb1 = testCaseGenerator.EmbedPolygon1();

            testCaseBase(glal, eb1);
        }

        private void testCase32()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase3();

            GraphicsDirectedPolygon eb2 = testCaseGenerator.EmbedPolygon2();

            testCaseBase(glal, eb2);
        }

        private void testCase33()
        {
            clearShapes();

            List<GraphicsLayoutArea> glal = testCaseGenerator.BaseCase3();

            GraphicsDirectedPolygon eb3 = testCaseGenerator.EmbedPolygon3();

            testCaseBase(glal, eb3);
        }
#if false
        private void testCase12()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon2();

            testCaseBase(gla, sd1);
        }

        private void testCase13()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon3();

            testCaseBase(gla, sd1);
        }

        private void testCase14()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon4();

            testCaseBase(gla, sd1);
        }

        private void testCase15()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon5();

            testCaseBase(gla, sd1);
        }

        private void testCase16()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon6();

            testCaseBase(gla, sd1);
        }

        private void testCase17()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon7();

            testCaseBase(gla, sd1);
        }

        private void testCase18()
        {
            GraphicsLayoutArea gla = testCaseGenerator.BaseCase1();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon8();

            testCaseBase(gla, sd1);
        }


        private void testCase22()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon2();

            testCaseBase(gla, sd1);
        }

        private void testCase23()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon3();

            testCaseBase(gla, sd1);
        }

        private void testCase24()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon4();

            testCaseBase(gla, sd1);
        }

        private void testCase25()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon5();

            testCaseBase(gla, sd1);
        }

        private void testCase26()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon6();

            testCaseBase(gla, sd1);
        }

        private void testCase27()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon7();

            testCaseBase(gla, sd1);
        }

        private void testCase28()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase2();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon8();

            testCaseBase(gla, sd1);
        }

        private void testCase39()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase3();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon9();

            testCaseBase(gla, sd1);
        }

        private void testCase410()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase4();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon10();

            testCaseBase(gla, sd1);
        }

        private void testCase411()
        {
            clearShapes();

            GraphicsLayoutArea gla = testCaseGenerator.BaseCase4();

            GraphicsDirectedPolygon sd1 = testCaseGenerator.EmbedPolygon11();

            testCaseBase(gla, sd1);
        }

#endif

        private void testCaseBase(List<GraphicsLayoutArea> glal, GraphicsDirectedPolygon eb)
        {
            foreach (GraphicsLayoutArea gla in glal)
            {
                GraphicsDirectedPolygon ea = gla.ExternalArea;

                // GraphicsDirectedPolygon ia = gla.InternalAreas[0];

                ea.Draw(Color.Black, Color.LightBlue);

                gla.InternalAreas.ForEach(i1 => i1.Draw(Color.Black, Color.White));

                List<Shape> internalShapeList = new List<Shape>();

                gla.InternalAreas.ForEach(i1 => internalShapeList.Add(i1.Shape));

                Shape glaShape = VisioGeometryEngine.Subtract(window, page, ea.Shape, internalShapeList);

                eb.Draw(Color.Black, Color.LightCoral);

                Shape sd1Shape = eb.Shape;

                shapes.Add(glaShape);
                shapes.Add(sd1Shape);
                shapes.Add(ea.Shape);
                shapes.AddRange(internalShapeList);

                layoutAreas.Add(gla);

                glaShape.SetFillOpacity(.5);
                eb.Shape.SetFillOpacity(.5);

                glaShape.SetLineWidth(2.5);
                glaShape.SetLineWidth(2.5);
            }

            window?.DeselectAll();

            EmbeddedLayoutGenerator embeddedLayoutGenerator = new EmbeddedLayoutGenerator(eb, glal.ConvertAll(l=>(LayoutArea)l));

            List<LayoutArea> results = embeddedLayoutGenerator.GenerateEmbeddedLayoutAreas();

            int i = 0;

            foreach (LayoutArea layoutArea in results)
            {
                GraphicsLayoutArea graphicsLayoutArea = new GraphicsLayoutArea(window, page, layoutArea);

                graphicsLayoutArea.Translate(new Coordinate(14, 0));

                graphicsLayoutArea.Draw(Color.Black, backColorList[i % backColorList.Count]);

                VisioInterop.SetFillOpacity(graphicsLayoutArea.Shape, 0.5);

                graphicsLayoutArea.Shape.SetShapeText((i + 1).ToString(), Color.Black, 36);

                layoutAreas.Add(graphicsLayoutArea);
                shapes.Add(graphicsLayoutArea.Shape);

                shapes.Add(graphicsLayoutArea.ExternalArea.Shape);

                graphicsLayoutArea.InternalAreas.ForEach(s => shapes.Add(s.Shape));

                i++;
            }

            window?.DeselectAll();

        }

        private void clearShapes()
        {
            foreach (GraphicsLayoutArea gla in layoutAreas)
            {
                gla.Delete();
            }

            foreach (Shape shape in shapes)
            {
                if (shape is null)
                {
                    continue;
                }

                shape.Delete();
            }

            layoutAreas.Clear();

            shapes.Clear();
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

            //testCase14();
        }

        private void btnTestCase15_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase15();
        }

        private void btnTestCase16_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase16();
        }

        private void btnTestCase17_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase17();
        }

        private void btnTestCase18_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase18();
        }

        private void btnTestCase21_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase21();
        }

        private void btnTestCase22_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase22();
        }

        private void btnTestCase23_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase23();
        }

        private void btnTestCase24_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase24();
        }

        private void btnTestCase25_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase25();
        }

        private void btnTestCase26_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase26();
        }

        private void btnTestCase27_Click(object sender, EventArgs e)
        {
            //testCase27();
        }

        private void btnTestCase28_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase28();
        }

        private void btnTestCase31_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase31();
        }

        private void btnTestCase32_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase32();
        }

        private void btnTestCase33_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase33();
        }

        private void btnTestCase39_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase39();
        }

        private void btnTestCase410_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase410();
        }

        private void btnTestCase411_Click(object sender, EventArgs e)
        {
            clearShapes();

            //testCase411();
        }

    }
}
