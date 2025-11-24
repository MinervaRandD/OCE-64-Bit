
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
            VisioInterop.SetDrawingScale(Page, "1 ft");

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            Page.VisioPage.AutoSize = false;

    

            Page.DrawingScaleInInches = 1;

            for (int i = 1; i <= 7; i++)
            {
                this.cmbTestNumber.Items.Add("Test " + i);
            }

            setupDataGridView(dgvCuts);
            setupDataGridView(dgvOverages);
            setupDataGridView(dgvUnderages);

           

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

        private void BtnDrawShape_Click(object sender, EventArgs e)
        {
            GraphicsLayoutArea prevGraphicsLayoutArea = graphicsLayoutArea;

            int testNumber = getTestNumber();
            
            if (testNumber == 0)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            graphicsLayoutArea = TestLayoutAreas.TestGraphicsLayoutArea(Window, Page, testNumber, 12);

            if (graphicsLayoutArea == null)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            if (prevGraphicsLayoutArea != null)
            {
                prevGraphicsLayoutArea.Delete();

                foreach (GraphicsDirectedPolygon polygon in polygonList)
                {
                    polygon.Delete();
                }
            }

            directedLineList.ForEach(l => l.Delete());
            directedLineList.Clear();

            graphicsLayoutArea.Draw(
                Color.Red, // Perimeter color
                Color.Yellow, // Area fill color
                Color.FromArgb(255, 0, 100, 0), // Seam color
                Color.FromArgb(255, 0, 0, 100), // Cut pen color
                Color.FromArgb(100, 100, 100, 255), // Cut fill color
                Color.Green, // Overage pen color
                Color.LightGreen // Overage fill color
                );

            int lineNumber = 1;

            foreach (GraphicsDirectedLine line in graphicsLayoutArea.ExternalArea)
            {
                line.Draw(Color.Red, 3, 1);

                line.Shape.SetShapeText(lineNumber.ToString(), Color.Black, 12 * 48);

                directedLineList.Add(line);

                lineNumber++;
            }

            
            VsoWindow.DeselectAll();

            double area1 = 0.0; // VisioInterop.GetShapeArea(graphicsLayoutArea.ExternalArea.Shape);
            double area2 = graphicsLayoutArea.ExternalArea.AreaInSqrInches() / 144.0;
        }

        //OversUndersForm oversUndersForm = null;

        private void btnDoCuts_Click(object sender, EventArgs e)
        {
            GraphicsLayoutArea prevGraphicsLayoutArea = graphicsLayoutArea;

            int testNumber = getTestNumber();

            if (testNumber == 0)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            int lineNumber = getLineNumber();

            if (lineNumber == 0)
            {
                MessageBox.Show("Set a valid line number to proceed.");
                return;
            }

            int rollWidth = getRollWidth();

            if (rollWidth == 0)
            {
                MessageBox.Show("Set a valid roll width to proceed.");
                return;
            }

            graphicsLayoutArea = TestLayoutAreas.TestGraphicsLayoutArea(Window, Page, testNumber, 12);

            if (graphicsLayoutArea == null)
            {
                MessageBox.Show("Select a valid test to proceed.");
                return;
            }

            foreach (GraphicsDirectedPolygon polygon in polygonList)
            {
                polygon.Delete();
            }

            graphicsLayoutArea.GenerateSeamsAndRollouts(
                new GraphicsDirectedLine(
                    Window
                    , Page
                    , graphicsLayoutArea.ExternalArea[lineNumber-1]
                    , LineRole.Seam),
                0, rollWidth, rollWidth, 3.0, 12.0);


            //graphicsLayoutArea.Draw(
            //    Color.Green, // Perimeter color
            //    Color.White, // Area fill color
            //    Color.FromArgb(255, 0, 100, 0), // Seam color
            //    Color.FromArgb(255, 0, 0, 100), // Cut pen color
            //    Color.FromArgb(100, 100, 100, 255), // Cut fill color
            //    Color.Green, // Overage pen color
            //    Color.LightGreen // Overage fill color
            //    );


            List<GraphicsCut> cutList = graphicsLayoutArea.GraphicsCutList;
            List<GraphicsOverage> overageList = graphicsLayoutArea.GraphicsOverageList;
            List<GraphicsUndrage> underageList = graphicsLayoutArea.GraphicsUndrageList;

            this.dgvCuts.Rows.Clear();
            this.dgvOverages.Rows.Clear();
            this.dgvUnderages.Rows.Clear();

#if false
            foreach (GraphicsCut cut in cutList)
            {
                string cutIndex = cut.CutIndex.ToString();

               // GraphicsDirectedPolygon rollout = cut.Rollout;

                Debug.Assert(rollout.Count == 4);

                rollout.Draw(Page, Color.Blue, Color.FromArgb(0, 255, 255, 255));

                polygonList.Add(rollout);

                string location = locationInFeet(rollout);

                string dimensions = string.Empty;

                double lnth;
                double wdth;

                double dim1 = rollout.MaxX - rollout.MinX;
                double dim2 = rollout.MaxY - rollout.MinY;

                if (dim1 > dim2)
                {
                    lnth = dim1;
                    wdth = dim2;
                }

                else
                {
                    lnth = dim2;
                    wdth = dim1;
                }

                dimensions = effectiveDimensionsInFeet(new Tuple<double, double>(lnth, wdth));

                dgvCuts.Rows.Add(new object[] { cutIndex, location, dimensions });
            }

            foreach (GraphicsOverage overage in overageList)
            {
                string overageIndex = ((char)('a' + overage.OverageIndex - 1)).ToString();

                string location = locationInFeet(overage.BoundingPolygon);

                string dimensions = string.Empty;

                if (overage.EffectiveDimensions != null)
                {
                    dimensions = effectiveDimensionsInFeet(overage.EffectiveDimensions);
                }

                dgvOverages.Rows.Add(new object[] { overageIndex, location, dimensions });
            }

            dgvOverages.CurrentCell = null;

            foreach (GraphicsUndrage undrage in underageList)
            {
                string underageIndex = ((char) ('A' + undrage.UnderageIndex-1)).ToString();

                string location = locationInFeet(undrage.BoundingPolygon);

                string dimensions = string.Empty;

                if (undrage.EffectiveDimensions != null)
                {
                    dimensions = effectiveDimensionsInFeet(undrage.EffectiveDimensions);
                }

                dgvUnderages.Rows.Add(new object[] { underageIndex, location, dimensions });

                if (ckbDrawUnderages.Checked)
                {
                    undrage.Draw(Page);
                }
            }

            dgvUnderages.CurrentCell = null;

            if (oversUndersForm is null)
            {
                oversUndersForm = new OversUndersForm();
                oversUndersForm.Show(this);
            }

           
            oversUndersForm.Update(graphicsLayoutArea.GraphicsCutList, rollWidth);
#endif
            VsoWindow.DeselectAll();
        }

        private string locationInFeet(DirectedPolygon boundingPolygon)
        {

            double inchesX = Math.Round(boundingPolygon.MinX);
            double inchesY = Math.Round(boundingPolygon.MinY);

            int feetX = (int)(Math.Floor(inchesX / 12.0));
            int feetY = (int)(Math.Floor(inchesY / 12.0));

            int inchX = (int)inchesX - 12 * feetX;
            int inchY = (int)inchesY - 12 * feetY;

            return "(" + feetX.ToString() + "' " + inchX.ToString().PadLeft(2) + "\"," + feetY.ToString() + "' " + inchY.ToString().PadLeft(2) + "\")";
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
            switch (this.cmbTestNumber.SelectedItem.ToString())
            {
                case "Test 1": return 1;
                case "Test 2": return 2;
                case "Test 3": return 3;
                case "Test 4": return 4;
                case "Test 5": return 5;
                case "Test 6": return 6;
                case "Test 7": return 7;
                default: return 0;
            }
        }

        private int getLineNumber()
        {
            int lineNumber = 0;

            if (!int.TryParse(this.txbBaseLine.Text.Trim(), out lineNumber))
            {
                return 0;
            }

            return lineNumber;
        }

        private int getRollWidth()
        {
            int rollWidth = 0;

            if (!int.TryParse(this.txbRollWidth.Text.Trim(), out rollWidth))
            {
                return 0;
            }

            return rollWidth;
        }
    }
}
