
namespace TestDriverScrollControl
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Visio = Microsoft.Office.Interop.Visio;

    using CanvasLib;

    using Utilities;

    using CanvasLib.Pan_And_Zoom;
    using Graphics;

    public partial class ScrollTestForm : Form, IMessageFilter
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public Visio.Page VsoPage { get; set; }

        private double zoom = 1.0;

        private double nominalZoom = 1.0;

        private double currZoom;
        private double baseZoom;

        private double canvWdthInches;
        private double canvHghtInches;

        private double drawWdth = 20;
        private double drawHght = 16;

        private PanAndZoomController panAndZoomController;

        public ScrollTestForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = drawHght;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = drawWdth;
            VsoWindow.Windows.ItemFromID[1653].Visible = false;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            VsoWindow.ShowScrollBars = 0;

            VsoWindow.ZoomBehavior = Visio.VisZoomBehavior.visZoomVisio;

            //VsoWindow.Zoom = -1;

            VsoWindow.ViewFit = 1;

            baseZoom = VsoWindow.Zoom;
            currZoom = baseZoom;

            VsoPage.DrawRectangle(2, 2, 18, 14);

            this.lblZoom.Text = currZoom.ToString("#,##0.0000");
            this.lblWidth.Text = canvWdthInches.ToString("0.00");
            this.lblHeight.Text = canvHghtInches.ToString("0.00");

            //VsoWindow.ZoomLock = true;

            this.panAndZoomController = new PanAndZoomController(
                Window, Page, tkbHScroll, tkbVScroll, drawWdth, drawHght, null, null, null, null, false);

            this.VsoWindow.ViewChanged += VsoWindow_ViewChanged;

            setSize();
            
            this.SizeChanged += TestDriver2BaseForm_SizeChanged;
        }

        private void VsoWindow_ViewChanged(Visio.Window Window)
        {
            currZoom = VsoWindow.Zoom;

            this.lblZoom.Text = currZoom.ToString("#,##0.0000");

            this.canvWdthInches = 31.0 * (baseZoom / currZoom);
            this.canvHghtInches = 20.0 * (baseZoom / currZoom);

            this.lblWidth.Text = canvWdthInches.ToString("0.00");
            this.lblHeight.Text = canvHghtInches.ToString("0.00");

            double viewLeft = 0;
            double viewUppr = 0;
            double viewWdth = 0;
            double viewHght = 0;

            double viewRght = 0;
            double viewLowr = 0;

            VsoWindow.GetViewRect(out viewLeft, out viewUppr, out viewWdth, out viewHght);

            viewRght = Math.Round(viewLeft + viewWdth, 1);
            viewLowr = Math.Round(viewUppr - viewHght);

            this.lblViewLeft.Text = viewLeft.ToString("0.0");
            this.lblViewRght.Text = viewRght.ToString("0.0");
            this.lblViewUppr.Text = viewUppr.ToString("0.0");
            this.lblViewLowr.Text = (viewUppr - viewHght).ToString("0.0");
        }

        private void TestDriver2BaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int panlSizeX = this.pnlInfoPanel.Width;
            int panlSizeY = this.pnlInfoPanel.Height;

            int panlLocnX = formSizeX - panlSizeX - 32;
            int panlLocnY = 16;

            int cntlSizeX = formSizeX - panlSizeX - 128;
            int cntlSizeY = formSizeY - 128;

            int cntlLocnX = 16;
            int cntlLocnY = 16;

            this.pnlInfoPanel.Location = new Point(panlLocnX, panlLocnY);
            this.pnlInfoPanel.Size = new Size(panlSizeX, panlSizeY);

            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);

            this.tkbHScroll.Location = new Point(cntlLocnX+6, cntlLocnY + cntlSizeY - 28);
            this.tkbHScroll.Size = new Size(cntlSizeX - 12, 45);

            this.tkbVScroll.Location = new Point(cntlLocnX + cntlSizeX + 4, cntlLocnY + 2);
            this.tkbVScroll.Size = new Size(45, cntlSizeY - 20);
        }


        public Rectangle GetScreen()
        {
            return Screen.FromControl(this).Bounds;
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
            if (m.Msg == (int)WindowsMessage.WM_HSCROLL)
            {

                return false;
            }

            return false;
        }

    }
}
