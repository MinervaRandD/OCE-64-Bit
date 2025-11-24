

namespace TestDriver2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using AxMicrosoft.Office.Interop.VisOcx;

    using Geometry;
    using Graphics;
    using MaterialsLayout;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class VisioExportImportBaseForm : Form
    {

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public GraphicsPage Page { get; set; }

        public GraphicsWindow Window { get; set; }

        private GraphicsLayoutArea graphicsLayoutArea = null;
        

        public VisioExportImportBaseForm()
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

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            Page.DrawRectangle(1, 1, 10, 10);

            this.btnExport.Click += btnExport_Click;
            this.btnClear.Click += btnClear_Click;
            this.btnImport.Click += btnImport_Click;

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

            int panlLocnX = 8;
            int panlLocnY = 8;

            int panlSizeX = 160;
            int panlSizeY = formSizeY - 16;

            int cntlLocnX = panlLocnX + panlSizeX + 16;
            int cntlLocnY = 8;

            int cntlSizeX = formSizeX - cntlLocnX - 32;
            int cntlSizeY = panlSizeY;

            this.panel1.Location = new Point(panlLocnX, panlLocnY);
            this.panel1.Size = new Size(panlSizeX, panlSizeY);

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.VsoPage.Export(@"C:\Temp\test.bmp");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Visio.Shape shape in this.VsoPage.Shapes)
            {
                shape.Delete();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.VsoPage.Import(@"C:\Temp\test.vsdx");
        }


    }
}
