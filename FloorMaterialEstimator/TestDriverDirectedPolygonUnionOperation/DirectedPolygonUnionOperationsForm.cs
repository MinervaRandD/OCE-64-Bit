
namespace TestDriverDirectedPolygonUnionOperations
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using MaterialsLayout.Subdivision;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class DirectedPolygonUnionOperationsForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public TestCaseGenerator testCaseGenerator;

        public List<Shape> shapes = new List<Shape>();

        private GraphicsPage page;

        private GraphicsWindow window;

        List<Color> backColorList = new List<Color>() { Color.LightBlue, Color.LightGreen, Color.LightSalmon, Color.Cyan, Color.Magenta, Color.Tomato };
        public DirectedPolygonUnionOperationsForm()
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

        }

        private void testCase1()
        {
            clearShapes();

            List<GraphicsDirectedPolygon> gdpList = testCaseGenerator.TestCase1();

            for (int i = 0; i < gdpList.Count; i++)
            {
                gdpList[i].Draw(Color.Black, backColorList[i % backColorList.Count]);
                gdpList[i].Shape.SetFillOpacity(0.5);
                shapes.Add(gdpList[i].Shape);
            }

            List<GraphicsDirectedPolygon> results = GraphicsDirectedPolygon.Union(window, page, gdpList);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Translate(new Coordinate(10, 0));

                results[i].Draw(Color.Black, backColorList[(i + gdpList.Count) % backColorList.Count]);
                results[i].Shape.SetFillOpacity(0.5);
                shapes.Add(results[i].Shape);
                results[i].Shape.SetLineWidth(5);
            }

            VsoWindow.DeselectAll();
        }


        private void testCase2()
        {
            clearShapes();

            List<GraphicsDirectedPolygon> gdpList = testCaseGenerator.TestCase2();

            for (int i = 0; i < gdpList.Count; i++)
            {
                gdpList[i].Draw(Color.Black, backColorList[i % backColorList.Count]);
                gdpList[i].Shape.SetFillOpacity(0.5);
                shapes.Add(gdpList[i].Shape);
            }

            List<GraphicsDirectedPolygon> results = GraphicsDirectedPolygon.Union(window, page, gdpList);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Translate(new Coordinate(10, 0));

                results[i].Draw(Color.Black, backColorList[(i + gdpList.Count) % backColorList.Count]);
                results[i].Shape.SetFillOpacity(0.5);
                shapes.Add(results[i].Shape);
                results[i].Shape.SetLineWidth(5);
            }

            VsoWindow.DeselectAll();
        }

        private void testCase3()
        {
            clearShapes();

            List<GraphicsDirectedPolygon> gdpList = testCaseGenerator.TestCase3();

            for (int i = 0; i < gdpList.Count; i++)
            {
                gdpList[i].Draw(Color.Black, backColorList[i % backColorList.Count]);
                gdpList[i].Shape.SetFillOpacity(0.5);
                shapes.Add(gdpList[i].Shape);
            }

            List<GraphicsDirectedPolygon> results = GraphicsDirectedPolygon.Union(window, page, gdpList);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Translate(new Coordinate(10, 0));

                results[i].Draw(Color.Black, backColorList[(i + gdpList.Count) % backColorList.Count]);
                results[i].Shape.SetFillOpacity(0.5);
                shapes.Add(results[i].Shape);
                results[i].Shape.SetLineWidth(5);
            }

            VsoWindow.DeselectAll();
        }

        private void testCase4()
        {
            clearShapes();

            List<GraphicsDirectedPolygon> gdpList = testCaseGenerator.TestCase4();

            for (int i = 0; i < gdpList.Count; i++)
            {
                gdpList[i].Draw(Color.Black, backColorList[i % backColorList.Count]);
                gdpList[i].Shape.SetFillOpacity(0.5);
                shapes.Add(gdpList[i].Shape);
            }

            List<GraphicsDirectedPolygon> results = GraphicsDirectedPolygon.Union(window, page, gdpList);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Translate(new Coordinate(10, 0));

                results[i].Draw(Color.Black, backColorList[(i + gdpList.Count) % backColorList.Count]);
                results[i].Shape.SetFillOpacity(0.5);
                shapes.Add(results[i].Shape);
                results[i].Shape.SetLineWidth(5);
            }

            VsoWindow.DeselectAll();
        }

        private void testCase5()
        {
            clearShapes();

            List<GraphicsDirectedPolygon> gdpList = testCaseGenerator.TestCase5();

            for (int i = 0; i < gdpList.Count; i++)
            {
                gdpList[i].Draw(Color.Black, backColorList[i % backColorList.Count]);
                gdpList[i].Shape.SetFillOpacity(0.5);
                shapes.Add(gdpList[i].Shape);
            }

            List<GraphicsDirectedPolygon> results = GraphicsDirectedPolygon.Union(window, page, gdpList);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Translate(new Coordinate(10, 0));

                results[i].Draw(Color.Black, backColorList[(i + gdpList.Count) % backColorList.Count]);
                results[i].Shape.SetFillOpacity(0.5);
                shapes.Add(results[i].Shape);
                results[i].Shape.SetLineWidth(5);
            }

            VsoWindow.DeselectAll();
        }

        private void testCase6()
        {
            clearShapes();

            List<GraphicsDirectedPolygon> gdpList = testCaseGenerator.TestCase6();

            for (int i = 0; i < gdpList.Count; i++)
            {
                gdpList[i].Draw(Color.Black, backColorList[i % backColorList.Count]);
                gdpList[i].Shape.SetFillOpacity(0.5);
                shapes.Add(gdpList[i].Shape);
            }

            List<GraphicsDirectedPolygon> results = GraphicsDirectedPolygon.Union(window, page, gdpList);

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Translate(new Coordinate(10, 0));

                results[i].Draw(Color.Black, backColorList[(i + gdpList.Count) % backColorList.Count]);
                results[i].Shape.SetFillOpacity(0.5);
                shapes.Add(results[i].Shape);
                results[i].Shape.SetLineWidth(5);
            }

            VsoWindow.DeselectAll();
        }

        private void clearShapes()
        {
            foreach (Shape shape in shapes)
            {
                shape.Delete();
            }
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

        private void btnTestCase1_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase1();
        }

        private void btnTestCase2_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase2();
        }

        private void btnTestCase3_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase3();
        }

        private void btnTestCase4_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase4();
        }

        private void btnTestCase5_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase5();
        }

        private void btnTestCase6_Click(object sender, EventArgs e)
        {
            clearShapes();

            testCase6();
        }
    }
}
