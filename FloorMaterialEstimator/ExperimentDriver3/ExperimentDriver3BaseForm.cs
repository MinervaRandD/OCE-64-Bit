

namespace ExperimentDriver4
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using AxMicrosoft.Office.Interop.VisOcx;

    using Graphics;
    using Geometry;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class ExperimentDriver3BaseForm : Form
    {

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Visio.Shape shape1;
        public Visio.Shape shape2;

        public Visio.Layer workLayer;
        public Visio.Layer mainLayer;

        public ExperimentDriver3BaseForm()
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

            workLayer = VsoPage.Layers.Add("WorkLayer");
            mainLayer = VsoPage.Layers.Add("MainLayer");

            shape1 = VsoPage.DrawPolyline(Poly1Coords, 1);
            shape2 = VsoPage.DrawPolyline(Poly2Coords, 2);

            GraphicShape shape = new GraphicShape(null, Window, Page, shape1, ShapeType.Polyline);

            VisioInterop.SetTakeoutAreaPattern(shape, Color.Maroon, "12");
        }

        private void resetCanvas()
        {
            if (shape1 != null)
            {
                shape1.Delete();
            }

            if (shape2 != null)
            {
                shape2.Delete();
            }

            shape1 = VsoPage.DrawPolyline(Poly1Coords, 1);
            shape2 = VsoPage.DrawPolyline(Poly2Coords, 1);
        }

        private double[] Poly1Coords = new double[]
        {
            2, 2,
            10, 2,
            10, 10,
            2, 10,
            2, 2
        };


        private double[] Poly2Coords = new double[]
        {
            4, 4,
            8, 4,
            8, 8,
            4, 8,
            4, 4
        };
    }
}
