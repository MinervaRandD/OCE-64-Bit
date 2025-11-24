
namespace ExperimentDriver1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;

    using Visio = Microsoft.Office.Interop.Visio;

    using ClipperLib;
    
    public partial class ExperimentDriver1 : Form
    {

        private List<List<IntPoint>> subjects = new List<List<IntPoint>>();
        private List<List<IntPoint>> clips = new List<List<IntPoint>>();
        private List<List<IntPoint>> solution = new List<List<IntPoint>>();

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Page Page;

        private GraphicsLayoutArea graphicsLayoutArea = null;

        public ExperimentDriver1()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            Page = new Page(VsoPage);
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            Visio.Shape outerShape = Page.VisioPage.DrawPolyline(new double[] { 2, 2, 10, 2, 10, 10, 2, 10, 2, 2 }, 1);
            Visio.Shape innerShape = Page.VisioPage.DrawPolyline(new double[] { 4, 4, 8, 4, 8, 8, 4, 8, 4, 4 }, 1);

            VsoWindow.Select(innerShape, (short)Visio.VisSelectArgs.visSubSelect);
            
            Visio.Selection s = VsoWindow.Selection;
            
            s.Subtract();

            VsoWindow.DeselectAll();

            List<List<Tuple<string, string>>> ShapeInfoList = new List<List<Tuple<string, string>>>();

            foreach (Visio.Shape shape in VsoPage.Shapes)
            {
                List<Tuple<string, string>> shapeInfoElem = GetShapeInfo(shape);
                ShapeInfoList.Add(shapeInfoElem);
            }
        }

        private List<Tuple<string, string>> GetShapeInfo(Visio.Shape visioShape)
        {
            List<Tuple<string, string>> returnList = new List<Tuple<string, string>>();

            short iSections = visioShape.GeometryCount;

            for (short iCurrGeometrySectionIndex = 0; iCurrGeometrySectionIndex < iSections; iCurrGeometrySectionIndex++)
            {
                int iTemp = ((int)Visio.VisSectionIndices.visSectionFirstComponent) + iCurrGeometrySectionIndex;

                short iCurrentGeometrySection = (short)iTemp;

                int iRows = visioShape.RowCount[iCurrentGeometrySection];

                for (short iCurrentRow = 2; iCurrentRow < iRows; iCurrentRow++)
                {
                    short iCells = visioShape.RowsCellCount[iCurrentGeometrySection, iCurrentRow];

                    for (short iCurrentCell = 0; iCurrentCell < iCells; iCurrentCell++)
                    {
                        string name = visioShape.CellsSRC[iCurrentGeometrySection, iCurrentRow, iCurrentCell].LocalName;

                        string formula = visioShape.CellsSRC[iCurrentGeometrySection, iCurrentRow, iCurrentCell].Formula;

                        returnList.Add(new Tuple<string, string>(name, formula));
                    }
                }
            }

            return returnList;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            generateTest(TestPolygons.Case2DirectedPolygon);
        }


        private List<IntPoint> subject3 = new List<IntPoint>()
        {
            new IntPoint(2.0, 2.0),
            new IntPoint(10.0, 2.0),
            new IntPoint(10.0, 10.0),
            new IntPoint(2.0, 10.0)
        };

        private List<IntPoint> clip3 = new List<IntPoint>()
        {
            new IntPoint(4.0, 4.0),
            new IntPoint(8.0, 4.0),
            new IntPoint(8.0, 8.0),
            new IntPoint(4.0, 8.0)
        };

        private void btnTest3_Click(object sender, EventArgs e)
        {
            subjects.Clear();
            clips.Clear();
            solution.Clear();

            subjects.Add(subject3);
            clips.Add(clip3);

            ClipperLib.Clipper clipper = new Clipper();

            clipper.AddPaths(subjects, PolyType.ptSubject, true);
            clipper.AddPaths(clips, PolyType.ptClip, true);


            bool succeeded = clipper.Execute(ClipType.ctXor, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);
        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            generateTest(TestPolygons.Case4DirectedPolygon);
        }

        private void generateTest(DirectedPolygon directedPolygon)
        {
            if (this.graphicsLayoutArea != null)
            {
                this.graphicsLayoutArea.Delete();
            }

           // graphicsLayoutArea = new GraphicsLayoutArea(Page, directedPolygon);

            //graphicsLayoutArea.GenerateHorizontalSeamList(12, 1);

            //graphicsLayoutArea.GenerateHorizontalCutList(12, 1);

            ////graphicsLayoutArea.GenerateHorizontalOverageList(1);

            graphicsLayoutArea.Draw(
                Color.FromArgb(255, 0, 0),
                Color.FromArgb(0, 255, 0),
                Color.FromArgb(0, 0, 255), // Cut pen color
                Color.LightBlue
                );

            Visio.Shape visioShape = graphicsLayoutArea.Shape.VisioShape;

            short iSections = visioShape.GeometryCount;

            for (short iCurrGeometrySectionIndex = 0; iCurrGeometrySectionIndex < iSections; iCurrGeometrySectionIndex++)
            {
                int iTemp = ((int) Visio.VisSectionIndices.visSectionFirstComponent) + iCurrGeometrySectionIndex;

                short iCurrentGeometrySection = (short)iTemp;

                int iRows = visioShape.RowCount[iCurrentGeometrySection];

                for (short iCurrentRow = 2; iCurrentRow < iRows; iCurrentRow++)
                {
                    short iCells = visioShape.RowsCellCount[iCurrentGeometrySection, iCurrentRow];

                    int s = (int)iCells - 1;

                    for (short iCurrentCell = (short) s; iCurrentCell < iCells; iCurrentCell++)
                    {
                        string name = visioShape.CellsSRC[iCurrentGeometrySection, iCurrentRow, iCurrentCell].LocalName;

                        string formula = visioShape.CellsSRC[iCurrentGeometrySection, iCurrentRow, iCurrentCell].Formula;
                    }
                }
            }
            string formula1 = visioShape.CellsSRC[10, 2, 2].Formula;

            string sss = visioShape.Cells["Geometry1.A1"].Formula;

            short s1 = visioShape.CellExists["Geometry1.A2",1];
            short s2 = visioShape.CellExists["Geometry1.A3", 1];

            VsoWindow.DeselectAll();
        }

    }
}
