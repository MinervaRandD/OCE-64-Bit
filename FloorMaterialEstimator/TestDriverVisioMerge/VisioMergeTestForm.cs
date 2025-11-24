
namespace TestDriverVisioMerge
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Graphics;
    using Geometry;
    using MaterialsLayout;
    using Utilities;
    using TracerLib;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class VisioMergeTestForm : Form
    {
        private Visio.Window vsoWindow { get; set; }

        private GraphicsWindow window { get; set; }

        private Visio.Document vsoDocument { get; set; }

        private Visio.Page vsoPage { get; set; }

        private GraphicsPage page { get; set; }

        public VisioMergeTestForm()
        {
            InitializeComponent();

            Tracer.TraceGen = new Tracer(TraceLevel.Exception, @"C:\Temp\temp.txt");

            vsoWindow = this.axDrawingControl.Window;
            vsoDocument = this.axDrawingControl.Document;

            this.vsoDocument.PrintLandscape = true;
            this.vsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.vsoDocument.Pages;

            this.vsoPage = pages[1];

            window = new GraphicsWindow(vsoWindow);

            page = new GraphicsPage(window, vsoPage);

            vsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            vsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            vsoWindow.ShowGrid = 1;

            vsoWindow.ShowRulers = 1;

            vsoWindow.ShowScrollBars = 1;

            vsoWindow.ZoomBehavior = Visio.VisZoomBehavior.visZoomVisio;

            vsoWindow.Zoom = -1;

            //VsoWindow.ZoomLock = true;

            setSize();

            this.SizeChanged += TestDriver2BaseForm_SizeChanged;

            doMerge();
        }

        private void doMerge()
        {
           // VisioGeometryEngine.Init(window, page);
            VisioInterop.Init(window, page);

            double[] polygonPoints1 = new double[]
            {
                2, 2, 6, 2, 6, 6, 2, 6, 2, 2
            };

            double[] polygonPoints2 = new double[]
            {
                6, 2, 10, 2, 10, 10, 6, 10,  6, 2
            };

            double[] polygonPoints3 = new double[]
            {
                12, 2, 16, 2, 16, 6, 12, 6,  12, 2
            };

            Visio.Shape shape1 = vsoPage.DrawPolyline(polygonPoints1, (short)8);

            Visio.Shape shape2 = vsoPage.DrawPolyline(polygonPoints2, (short)8);

            Visio.Shape shape3 = vsoPage.DrawPolyline(polygonPoints3, (short)8);

            shape1.Data3 = GuidMaintenance.CreateGuid(shape1);

            shape2.Data3 = GuidMaintenance.CreateGuid(shape2);

            shape3.Data3 = GuidMaintenance.CreateGuid(shape3);

            Shape Shape1 = new Shape(window, page, shape1, ShapeType.Polygon);

            Shape Shape2 = new Shape(window, page, shape2, ShapeType.Polygon);

            Shape mergedShape = VisioGeometryEngine.Merge(window, page, new List<Shape>() { Shape1, Shape2 });

            VisioInterop.SetBaseFillColor(mergedShape, Color.BlueViolet); 
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
