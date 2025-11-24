
namespace TestDriverVoronoiAlgo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Geometry;
    using Graphics;
    using VoronoiLib;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class VoronoiAlgoTestForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public VoronoiAlgoTestForm()
        {
            InitializeComponent();

            TestCases.SetupTestCases();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);


            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            setSize();

            this.SizeChanged += TestDriver2BaseForm_SizeChanged;
        }

        private void TestDriver2BaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int grbTestsLocX = 8;
            int grbTestsLocY = 16;

            int cntlLocnX = grbTestsLocX + grbTests.Width + 16;
            int cntlLocnY = 16;

            int grbTestsSizeX = 120;
            int grbTestsSizeY = formSizeY - grbTestsLocY - 64;

            int cntlSizeX = formSizeX - grbTestsSizeX - grbTestsLocX - 8;
            int cntlSizeY = formSizeY - grbTestsLocY - 32;

            this.grbTests.Size = new Size(grbTestsSizeX, grbTestsSizeY);
            this.grbTests.Location = new Point(grbTestsLocX, grbTestsLocY);

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            runTest(TestCases.TestCase1DirectedLines);
        }
        private void btnTest2_Click(object sender, EventArgs e)
        {
            runTest(TestCases.TestCase2DirectedLines);
        }
        private void btnTest3_Click(object sender, EventArgs e)
        {
            runTest(TestCases.TestCase3DirectedLines);
        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            runTest(TestCases.TestCase4DirectedLines);
        }
        private void runTest(List<DirectedLine> directedLineList)
        {
            deleteLines(DrawnLines);
            deleteCircles(DrawnCircles);

            drawLineList(directedLineList, Color.Blue);

            VoronoiRunner voronoiRunner = new VoronoiRunner(directedLineList, 11);

            Coordinate result = voronoiRunner.RunVoroniAlgo();

            List<Coordinate> voronoiInternalCoords = new List<Coordinate>(voronoiRunner.VoronoiInternalCoords);

            List<DirectedLine> voronoiEdgeList = voronoiRunner.VoronoiEdgeList;

            drawCoordList(voronoiInternalCoords, Color.Green);
            drawLineList(voronoiEdgeList, Color.Red);
            drawBestCoord(result, Color.Green);
        }

       
        private void deleteLines(List<GraphicsDirectedLine> drawnLines)
        {
            foreach (GraphicsDirectedLine line in drawnLines)
            {
                line.Delete();
            }
        }

        private void deleteCircles(List<GraphicsCircle> drawnCircles)
        {
            foreach (GraphicsCircle circle in drawnCircles)
            {
                circle.Delete();
            }
        }

        List<GraphicsDirectedLine> DrawnLines = new List<GraphicsDirectedLine>();
        List<GraphicsCircle> DrawnCircles = new List<GraphicsCircle>();

        private void drawLineList(List<DirectedLine> lineList, Color color)
        {
            foreach (DirectedLine line in lineList)
            {
                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(Window, Page, line, LineRole.Unknown);

                graphicsDirectedLine.Draw(color, 2);

                DrawnLines.Add(graphicsDirectedLine);
            }

            Window?.DeselectAll();
        }

        private void drawCoordList(List<Coordinate> coordList, Color color)
        {
            foreach (Coordinate coord in coordList)
            {
                GraphicsCircle circle = new GraphicsCircle(Window, Page, coord, 0.25);

                circle.Draw(color, 2);

                DrawnCircles.Add(circle);
            }

            Window?.DeselectAll();
        }

        private void drawBestCoord(Coordinate coord, Color color)
        {
            GraphicsCircle circle = new GraphicsCircle(Window, Page, coord, 0.25);
            
            circle.Draw(color, 2);

            circle.Shape.SetFillColor(color);

            Window?.DeselectAll();

            DrawnCircles.Add(circle);
        }

    }
}
