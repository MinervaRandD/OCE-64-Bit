using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestDriverZoomControl
{
    using Visio = Microsoft.Office.Interop.Visio;
    using CanvasLib.Pan_And_Zoom;
    using Graphics;
    using Utilities;
    using System.Runtime.InteropServices;

    public partial class ZoomTestForm : Form, IMessageFilter
    {
        public Visio.Window VsoWindow { get; set; }

        private GraphicsWindow window;

        public Visio.Document VsoDocument { get; set; }

        private GraphicsPage page;

        public Visio.Page VsoPage { get; set; }

        private PanAndZoomController panAndZoomController;

        public ZoomTestForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            panAndZoomController = new PanAndZoomController(
                window, page, trbHorizontal, trbVertical, 20, 16, btnZoomIn, btnZoomOut, tlbZoomPct, tlsZoomToFit);

            panAndZoomController.lblCenter = this.lblCenter;
            panAndZoomController.lblIgnoreScrollEvent = this.lblIgnoreScrollEvent;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, VsoPage);

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            for (int i = 2; i < 8; i+= 2)
            {
                Visio.Shape visioShape = this.VsoPage.DrawOval(10-i, 8-i, 10+i, 8+i);
                VisioInterop.SetFillOpacity(new Shape(window, page, visioShape, ShapeType.Circle), 0);
            }

            VsoWindow.DeselectAll();

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            VsoWindow.ShowScrollBars = 0;

            VsoWindow.Windows.ItemFromID[1653].Visible = true;

            setSize();

            this.SizeChanged += ScrollTestForm_SizeChanged;

            //this.VsoWindow.ScrollLock = true;

            this.VsoWindow.ShowGuides = (short)1;

            this.VsoWindow.ViewFit = 1;

            //this.MouseWheel += ZoomTestForm_MouseWheel;    
            //this.VsoWindow.ZoomLock = true;

           
        }


        //private void ZoomTestForm_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    this.lblMouseScroll.Text = e.Delta.ToString();
        //}

        private void ScrollTestForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
            panAndZoomController.SetZoom();
        }

        private void setSize()
        {
            int formSizeX = this.ClientRectangle.Width;
            int formSizeY = this.ClientRectangle.Height;

            int cntlSizeX = formSizeX - 48;
            int cntlSizeY = formSizeY - 96;

            int cntlLocnX = 8;
            int cntlLocnY = 32;

            axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);

            trbHorizontal.Location = new Point(cntlLocnX + 4, cntlLocnY + cntlSizeY - 16);
            trbHorizontal.Size = new Size(cntlSizeX, 16);

            trbVertical.Location = new Point(cntlLocnX + cntlSizeX -8, cntlLocnY + 4);
            trbVertical.Size = new Size(16, cntlSizeY);

        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {

            bool rtrnCode = false;

            if (m.Msg == (int)WindowsMessage.WM_MOUSEWHEEL)
            {
                IntPtr ip = m.WParam;

                object x = (int)ip;

                //panAndZoomController.SetZoom(0.05 + panAndZoomController.CurrZoom);

                return true;
            }

            return rtrnCode;
        }

    }
}
