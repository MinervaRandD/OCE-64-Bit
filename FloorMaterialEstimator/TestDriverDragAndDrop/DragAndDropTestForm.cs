
namespace TestDriverDragAndDrop
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

    public partial class DragAndDropTestForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Visio.Application VsoApplication { get; set; }
        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public DragAndDropTestForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;
            VsoApplication = this.axDrawingControl.Window.Application;

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

            Page.DrawRectangle(2, 2, 8, 8);

            this.VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.VsoWindow.MouseUp += VsoWindow_MouseUp;
            this.VsoWindow.MouseMove += VsoWindow_MouseMove;
        }

        private void VsoWindow_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = false;
        }

        private void VsoWindow_MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = true;
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = false;
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
            for (short i = 1; i < 2000; i++)
            {
                try
                {
                    VsoApplication.DoCmd(i);

                    int j = 1;
                }

                catch { }
            }
            
        }
    }
}
