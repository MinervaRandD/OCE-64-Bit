
namespace TestDriverVisioSubtractOperation
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Graphics;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class VisioSubtractTestForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public GraphicsWindow Window { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }
        
        public GraphicsPage Page { get; set; }

        public VisioSubtractTestForm()
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

            VsoWindow.ZoomBehavior = Visio.VisZoomBehavior.visZoomVisio;

            VsoWindow.Zoom = -1;

            //VsoWindow.ZoomLock = true;

            setSize();

            this.SizeChanged += TestDriver2BaseForm_SizeChanged;

            doSubtract();
        }

        private void doSubtract()
        {

            Visio.Shape frstShape = VsoPage.DrawRectangle(0, 0, 2, 2);

            Visio.Shape shapeOuter = VsoPage.DrawRectangle(2, 2, 12, 12);
            Visio.Shape shapeInner = VsoPage.DrawRectangle(4, 4, 10, 10);


            VsoWindow.DeselectAll();

            Visio.Selection selection = VsoPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeSingle, Visio.VisSelectMode.visSelModeSkipSuper, shapeOuter);

            //Visio.Selection selection2 = shapeOuter.CreateSelection(Visio.VisSelectionTypes.visSelTypeAll, Visio.VisSelectMode.visSelModeSkipSuper, shapeOuter);


            selection.Select(shapeOuter, 2);
            selection.Select(shapeInner, 2);

            selection.Subtract();

            Visio.Selection result = VsoWindow.Selection;

            //VisioInterop.SetBaseFillColor(new Shape(shapeOuter, ShapeType.Rectangle), Color.Blue);
            //VisioInterop.SetBaseFillColor(new Shape(shapeInner, ShapeType.Rectangle), Color.Red);

            Visio.Shape shape = result[1];

            //VsoWindow.DeselectAll();

            string shapeName = shape.NameID;

            VisioInterop.SetBaseFillColor(new Shape(Window, Page, shape, ShapeType.Rectangle), Color.Blue);

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
