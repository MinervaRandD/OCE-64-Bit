
namespace TestDriverLayerVisibilityForLines
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
  
    using Visio = Microsoft.Office.Interop.Visio;

    public partial class TestDriverLayerVisibilityForLinesForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        List<GraphicsDirectedLine> directedLineList = new List<GraphicsDirectedLine>();

        public GraphicsPage Page;

        public GraphicsWindow Window { get; set; }


        private List<GraphicsDirectedPolygon> polygonList = new List<GraphicsDirectedPolygon>();

        private const double scale = 12;

        private Geometry.Rectangle baseRectangle;

        private GraphicsLayer graphicsLayer;
        public TestDriverLayerVisibilityForLinesForm()
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

            GraphicsWindow window = new GraphicsWindow(VsoWindow);
            GraphicsPage page = new GraphicsPage(window, VsoPage);

            DirectedLine directedLine = new DirectedLine(new Coordinate(4, 100), new Coordinate(200, 100));

            GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(window, page, directedLine, LineRole.SingleLine);

            graphicsDirectedLine.Draw(Color.Red, 4);

            graphicsLayer = new GraphicsLayer(window, page, "abc", GraphicsLayerType.Dynamic);

            graphicsLayer.AddShape(graphicsDirectedLine.Shape, 1);
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

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        private void btnMakeVisible_Click(object sender, EventArgs e)
        {
            graphicsLayer.SetLayerVisibility(true);
        }

        private void btnMakeInvisible_Click(object sender, EventArgs e)
        {
            graphicsLayer.SetLayerVisibility(false);
        }
    }
}
