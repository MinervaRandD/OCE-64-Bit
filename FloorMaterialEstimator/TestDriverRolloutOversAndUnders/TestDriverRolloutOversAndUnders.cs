
namespace TestDriverRolloutOversAndUnders
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
    using MaterialsLayout;
    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class TestSeamsAndCutsForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        List<GraphicsDirectedLine> directedLineList = new List<GraphicsDirectedLine>();

        public GraphicsPage Page;

        public GraphicsWindow Window { get; set; }

        private GraphicsLayoutArea graphicsLayoutArea = null;

        private List<GraphicsDirectedPolygon> polygonList = new List<GraphicsDirectedPolygon>();

        private GraphicsRollout baseRollout;

        private const double scale = 12;

        private Geometry.Rectangle baseRectangle;

        public TestSeamsAndCutsForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 12 * 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 12 * 20;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            Page.VisioPage.AutoSize = true;

            VisioInterop.SetDrawingScale(Page, "1 ft");


            Page.DrawingScaleInInches = 1;

            for (int i = 1; i <= TestLayoutAreas.TestCoordLists.Count; i++)
            {
                this.cmbTestNumber.Items.Add("Test " + i);
            }

            setupDataGridView(dgvCuts);
            setupDataGridView(dgvOverages);
            setupDataGridView(dgvUnderages);

            Coordinate coord1 = new Coordinate(scale * 0, scale * 4);
            Coordinate coord2 = new Coordinate(scale * 12, scale * 2);

            baseRectangle = new Geometry.Rectangle(coord1, coord2);

            baseRollout = new GraphicsRollout(Window, Page, baseRectangle);

            baseRollout.Draw(Color.Black, Color.FromArgb(32, 0, 0, 255));

            setSize();

            this.SizeChanged += TestDriver2BaseForm_SizeChanged;

            this.btnDrawShape.Click += BtnDrawShape_Click;
        }

        private void setupDataGridView(DataGridView dgv)
        {

            dgv.Columns.Add("Index", "Index");
            dgv.Columns.Add("Location", "Location");
            dgv.Columns.Add("Effective Dimensions", "Effective Dimensions");

            dgv.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgv.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            dgv.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 100;
            dgv.Columns[1].Width = 100;

            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.RowHeadersVisible = false;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            }
        }

        private void TestDriver2BaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int panl1LocnX = 8;
            int panl1LocnY = 8;

            int panl1SizeX = 180;
            int panl1SizeY = formSizeY - 16;

            int panl2SizeX = 300;
            int panl2SizeY = formSizeY - 16;

            int cntlLocnX = panl1LocnX + panl1SizeX + 16;
            int cntlLocnY = 8;

            int cntlSizeX = formSizeX - panl2SizeX - cntlLocnX - 32;
            int cntlSizeY = panl1SizeY;

            int panl2LocnX = cntlLocnX + cntlSizeX + 16;
            int panl2LocnY = panl1LocnY;

            this.pnlTestSetup.Location = new Point(panl1LocnX, panl1LocnY);
            this.pnlTestSetup.Size = new Size(panl1SizeX, panl1SizeY);

            this.pnlResults.Location = new Point(panl2LocnX, panl2LocnY);
            this.pnlResults.Size = new Size(panl2SizeX, panl2SizeY);

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        List<Shape> shapeList = new List<Shape>();

        private void BtnDrawShape_Click(object sender, EventArgs e)
        {

            int testNumber = getTestNumber();
            
            if (testNumber == 0)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            shapeList.ForEach(s => s.Delete());

            shapeList.Clear();

            if (graphicsLayoutArea != null)
            {
                graphicsLayoutArea.Delete();
            }

            graphicsLayoutArea = TestLayoutAreas.TestGraphicsLayoutArea(Window, Page, testNumber, scale);

            if (graphicsLayoutArea == null)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            Shape shape = graphicsLayoutArea.Draw(
                Color.Red, // Perimeter color
                Color.AntiqueWhite);

            shapeList.Add(graphicsLayoutArea.ExternalArea.Shape);

            VisioInterop.SetFillOpacity(shape, .25);
            VisioInterop.SetFillPattern(shape, "2");

            VsoWindow.DeselectAll();

        }

        //OversUndersForm oversUndersForm = null;

        private void btnDoCuts_Click(object sender, EventArgs e)
        {
            int testNumber = getTestNumber();

            if (testNumber == 0)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            if (graphicsLayoutArea is null)
            {
                graphicsLayoutArea = TestLayoutAreas.TestGraphicsLayoutArea(Window, Page, testNumber, 12);
            }

            foreach (GraphicsDirectedPolygon polygon in polygonList)
            {
                polygon.Delete();
            }

            
            baseRollout.Clear();

            baseRollout = new GraphicsRollout(Window, Page, baseRectangle);

            graphicsLayoutArea.GraphicsRolloutList.Add(baseRollout);

            baseRollout.GenerateCutsOveragesAndUndrages(0, graphicsLayoutArea, 12);


            List<GraphicsCut> cutList = baseRollout.GraphicsCutList;
            //List<GraphicsOverage> overageList = graphicsLayoutArea.GraphicsOverageList;
            //List<GraphicsUndrage> underageList = graphicsLayoutArea.GraphicsUndrageList;

            this.dgvCuts.Rows.Clear();
            this.dgvOverages.Rows.Clear();
            this.dgvUnderages.Rows.Clear();


            foreach (GraphicsCut graphicsCut in cutList)
            {
                string cutIndex = graphicsCut.GraphicsCutIndex.ToString();

                GraphicsDirectedPolygon rollout = new GraphicsDirectedPolygon(Window, Page, ((DirectedPolygon)graphicsCut.CutRectangle));

                Debug.Assert(rollout.Count == 4);

                Shape shape = rollout.Draw(Page, Color.Green, Color.FromArgb(0, 0, 255, 0));

                this.shapeList.Add(shape);

                shape.SetLineWidth(8);

                shape.SetFill("2");

                shape.BringToFront();

                polygonList.Add(rollout);

                string location = locationInFeet(rollout);

                string dimensions = string.Empty;

                double lnth;
                double wdth;

                double dim1 = rollout.MaxX - rollout.MinX;
                double dim2 = rollout.MaxY - rollout.MinY;

                dimensions = effectiveDimensionsInFeet(new Tuple<double, double>(dim1, dim2));

                dgvCuts.Rows.Add(new object[] { cutIndex, location, dimensions });
            }

            foreach (GraphicsOverage graphicsOverage in baseRollout.GraphicsOverageList)
            {
                string overageIndex = ((char)('a' + graphicsOverage.GraphicsOverageIndex.OverageIndex - 1)).ToString();

                string location = locationInFeet(graphicsOverage.BoundingRectangle);

                string dimensions = string.Empty;

                if (graphicsOverage.EffectiveDimensions != null)
                {
                    dimensions = effectiveDimensionsInFeet(graphicsOverage.EffectiveDimensions);
                }

                dgvOverages.Rows.Add(new object[] { overageIndex, location, dimensions });
            }

            dgvOverages.CurrentCell = null;


            foreach (GraphicsUndrage graphicsUndrage in baseRollout.GraphicsUndrageList)
            {
                string underageIndex = Utilities.IndexToUpperCaseString(graphicsUndrage.UndrageIndex);

                string location = locationInFeet(graphicsUndrage.BoundingRectangle);

                string dimensions = string.Empty;

                if (graphicsUndrage.EffectiveDimensions != null)
                {
                    dimensions = effectiveDimensionsInFeet(graphicsUndrage.EffectiveDimensions);
                }

                dgvUnderages.Rows.Add(new object[] { underageIndex, location, dimensions });

                if (ckbDrawUnderages.Checked)
                {
                    graphicsUndrage.Draw(Color.Black, Color.LightBlue);
                }
            }

            dgvUnderages.CurrentCell = null;

            //if (oversUndersForm is null)
            //{
            //    //oversUndersForm = new FloorMaterialEstimator.OversUndersForm.OversUndersForm();
            //    oversUndersForm.Show(this);
            //}
        
            //oversUndersForm.Update(new List<LayoutArea>() { graphicsLayoutArea }, Page.DrawingScaleInInches);


            VsoWindow.DeselectAll();
        }

        private string locationInFeet(Geometry.Rectangle boundingRectangle)
        {
            double inchesX = Math.Round(boundingRectangle.MinX);
            double inchesY = Math.Round(boundingRectangle.MinY);

            int feetX = (int)(Math.Floor(inchesX / 12.0));
            int feetY = (int)(Math.Floor(inchesY / 12.0));

            int inchX = (int)inchesX - 12 * feetX;
            int inchY = (int)inchesY - 12 * feetY;

            return "(" + feetX.ToString() + "' " + inchX.ToString().PadLeft(2) + "\", " + feetY.ToString() + "' " + inchY.ToString().PadLeft(2) + "\")";
        }

        private string locationInFeet(DirectedPolygon boundingPolygon)
        {
            double inchesX = Math.Round(boundingPolygon.MinX);
            double inchesY = Math.Round(boundingPolygon.MinY);

            int feetX = (int)(Math.Floor(inchesX / 12.0));
            int feetY = (int)(Math.Floor(inchesY / 12.0));

            int inchX = (int)inchesX - 12 * feetX;
            int inchY = (int)inchesY - 12 * feetY;

            return "(" + feetX.ToString() + "' " + inchX.ToString().PadLeft(2) + "\", " + feetY.ToString() + "' " + inchY.ToString().PadLeft(2) + "\")";
        }

        private string effectiveDimensionsInFeet(Tuple<double, double> effectiveDimensions)
        {
            double inchesX = Math.Round(effectiveDimensions.Item1);
            double inchesY = Math.Round(effectiveDimensions.Item2);

            int feetX = (int)(Math.Floor(inchesX / 12.0));
            int feetY = (int)(Math.Floor(inchesY / 12.0));

            int inchX = (int)inchesX - 12 * feetX;
            int inchY = (int)inchesY - 12 * feetY;

            return feetX.ToString() + "' " + inchX.ToString().PadLeft(2) + "\" x " + feetY.ToString() + "' " + inchY.ToString().PadLeft(2) + "\"";
        }

        private int getTestNumber()
        {
            return this.cmbTestNumber.SelectedIndex + 1;
        }
    }
}
