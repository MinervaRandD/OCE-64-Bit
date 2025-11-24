
namespace TestDriverShapeDeleteTest
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Graphics;
    using TracerLib;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class VisioShapeDeleteTestForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public GraphicsWindow Window { get; set; }


        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public GraphicsPage Page { get; set; }

        public VisioShapeDeleteTestForm()
        {
            InitializeComponent();

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

            VsoWindow.ShowScrollBars = 1;

            Tracer.TraceGen = new Tracer((TraceLevel) 0xFF, @"C:\Temp\temp.txt");

            setSize();

            this.SizeChanged += TestDriver2BaseForm_SizeChanged;

            doTest();
        }

        private void doTest()
        {
            Visio.Shape visioShape1 = VsoPage.DrawRectangle(2, 2, 4, 4);

            Shape shape1 = new Shape(Window, Page, visioShape1, ShapeType.Rectangle);

            int id1 = visioShape1.ID;

            shape1.Delete();

            id1 = visioShape1.ID;

            Visio.Layer visioLayer1 = VsoPage.Layers.Add("Test layer");

            visioLayer1.Delete(1);
        }

        private void TestDriver2BaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        
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

    }
}
