using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestDriverBorderManager
{
    using Visio = Microsoft.Office.Interop.Visio;
    using CanvasLib.Pan_And_Zoom;
    using Graphics;
    using Utilities;
    using System.Runtime.InteropServices;
    using CanvasLib.Borders;
    using FinishesLib;
    using TracerLib;

    public partial class TestDriverBorderManagerForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        private BorderManager borderManager;

        private GraphicsWindow window;

        private GraphicsPage page;

        private bool drawingShape = false;

        private Tracer tracer;

        public TestDriverBorderManagerForm()
        {
            InitializeComponent();

            this.lblDrawingShape.Text = "Drawing Shape: " + drawingShape;

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];


            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, this.VsoPage);

            tracer = new Tracer((TraceLevel)31, @"C:\Temp\testLog.txt", true);

            VisioInterop.Init(window, page);

            borderManager = new BorderManager(window, page);

            borderManager.Init(1, null, null);

            this.VsoWindow.MouseUp += VsoWindow_MouseUp;
            setSize();

            this.SizeChanged += TestDriverBorderManagerForm_SizeChanged;
        }

        private void VsoWindow_MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            borderManager.BorderDrawingModeClick(Button, x, y);

            this.lblDrawingShape.Text = "Drawing Shape: " + drawingShape;

            CancelDefault = true;
        }

        private void TestDriverBorderManagerForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.ClientRectangle.Width;
            int formSizeY = this.ClientRectangle.Height;

            int cntlSizeX = formSizeX - 48;
            int cntlSizeY = formSizeY - 64;

            int cntlLocnX = 18;
            int cntlLocnY = 12;

            axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);

            this.btnDeleteLast.Location = new Point(this.btnDeleteLast.Location.X, formSizeY - 32);

            this.btnDeleteAll.Location = new Point(this.btnDeleteAll.Location.X, formSizeY - 32);
        }

        private void btnDeleteLast_Click(object sender, EventArgs e)
        {
            borderManager.DeleteLastMarker();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            borderManager.Reset();
        }
    }
}
