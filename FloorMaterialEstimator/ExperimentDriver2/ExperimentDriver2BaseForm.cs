

namespace ExperimentDriver4
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
    
    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using Graphics;

    public partial class ExperimentDriver2BaseForm : Form
    {

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Visio.Shape shape1;
        public Visio.Shape shape2;

        public Visio.Layer workLayer;
        public Visio.Layer mainLayer;

        public ExperimentDriver2BaseForm()
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

            //workLayer = VsoPage.Layers.Add("WorkLayer");
            //mainLayer = VsoPage.Layers.Add("MainLayer");

            shape1 = VsoPage.DrawPolyline(Poly1Coords, 1);
            shape2 = VsoPage.DrawPolyline(Poly2Coords, 2);

            GraphicsWindow window = new GraphicsWindow(VsoWindow);
            GraphicsPage page = new GraphicsPage(window, VsoPage);

            GraphicsLayer workLayer = new GraphicsLayer(window, page, "[Worklayer]", GraphicsLayerType.WorkLayer, GraphicsLayerStyle.Static);
           
            GraphicsLayer mainLayer = new GraphicsLayer(window, page, "[MainLayer]", GraphicsLayerType.MainLayer, GraphicsLayerStyle.Static);

            VisioInterop.SetLayerVisibility(workLayer, false);
            VisioInterop.SetLayerVisibility(mainLayer, false);
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            resetCanvas();

            VsoWindow.Select(shape1, (short) Visio.VisSelectArgs.visSelectAll);
            VsoWindow.Select(shape2, (short)Visio.VisSelectArgs.visSelectAll);

            VsoWindow.Selection.Subtract();

            shape1 = null;
            shape2 = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetCanvas();

            VsoWindow.Select(shape2, (short)Visio.VisSelectArgs.visSelectAll);
            VsoWindow.Select(shape1, (short)Visio.VisSelectArgs.visSelectAll);

            workLayer.Add(shape1,1);
            workLayer.Add(shape2,1);

            VsoWindow.Selection = VsoPage.CreateSelection(Visio.VisSelectionTypes.visSelTypeAll);
            VsoWindow.Selection.Fragment();

            foreach (Visio.Shape shape in VsoPage.Shapes)
            {
                int count = shape.GeometryCount;

                if (count <= 1)
                {
                    shape.Delete();
                }
            }
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            resetCanvas();

            VsoWindow.Select(shape2, (short)Visio.VisSelectArgs.visSelectAll);
            VsoWindow.Select(shape1, (short)Visio.VisSelectArgs.visSelectAll);

            VsoWindow.Selection.Union();

            shape1 = null;
            shape2 = null;


        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            
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
